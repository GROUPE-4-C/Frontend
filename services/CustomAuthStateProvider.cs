using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using AlumniConnect.Front.Services; // Ceci est l'espace de noms de ce fichier lui-même, il n'est pas toujours nécessaire mais ne fait pas de mal

namespace AlumniConnect.Front.Services
{
    // C'EST ICI LA CORRECTION PRINCIPALE : AJOUTER ": AuthenticationStateProvider"
    public class CustomAuthStateProvider : AuthenticationStateProvider 
    {
        private readonly IJSRuntime _jsRuntime;
        private const string AdminTokenKey = "admin_jwt_token";

        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", AdminTokenKey);

            ClaimsIdentity identity;
            if (!string.IsNullOrEmpty(token))
            {
                var claims = ParseClaimsFromJwt(token);

                var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (!string.IsNullOrEmpty(emailClaim) && !string.IsNullOrEmpty(roleClaim))
                {
                    identity = new ClaimsIdentity(claims, "JwtAuth");
                }
                else
                {
                    identity = new ClaimsIdentity();
                }
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string email, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousIdentity = new ClaimsIdentity();
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousPrincipal)));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1]; 

            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                foreach (var kvp in keyValuePairs)
                {
                    if (kvp.Value is JsonElement element)
                    {
                        switch (kvp.Key)
                        {
                            case "name":
                            case "unique_name":
                                claims.Add(new Claim(ClaimTypes.Name, element.GetString() ?? string.Empty));
                                break;
                            case "email":
                                claims.Add(new Claim(ClaimTypes.Email, element.GetString() ?? string.Empty));
                                break;
                            case "role":
                                if (element.ValueKind == JsonValueKind.Array)
                                {
                                    claims.AddRange(element.EnumerateArray().Select(r => new Claim(ClaimTypes.Role, r.GetString() ?? string.Empty)));
                                }
                                else
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, element.GetString() ?? string.Empty));
                                }
                                break;
                            case "sub":
                                claims.Add(new Claim(ClaimTypes.NameIdentifier, element.GetString() ?? string.Empty));
                                break;
                            default:
                                claims.Add(new Claim(kvp.Key, element.ToString() ?? string.Empty));
                                break;
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty));
                    }
                }
            }
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}