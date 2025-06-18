using System;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using AlumniConnect.Front.Models;
using AlumniConnect.Front.Services;

namespace AlumniConnect.Front.Services
{
    public class AuthService
    {
        
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly IJSRuntime _jsRuntime;
        private const string AdminTokenKey = "admin_jwt_token";

        public AuthService(AuthenticationStateProvider authStateProvider, IJSRuntime jsRuntime)
        {
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
            _jsRuntime = jsRuntime;
        }

        public bool IsAdmin => !string.IsNullOrEmpty(GetTokenAsync().Result); 

        public async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", AdminTokenKey);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            await Task.Delay(1000);

            if (email == "admin@monprojet.com" && password == "admin123")
            {
                var claimsPayload = new 
                { 
                    name = email, 
                    email = email, 
                    role = "Admin",
                    exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
                };
                var jsonPayload = JsonSerializer.Serialize(claimsPayload);
                var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload));

                var simulatedToken = $"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.{base64Payload}.dummy_signature";
                
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AdminTokenKey, simulatedToken);
                
                _authStateProvider.MarkUserAsAuthenticated(email, "Admin"); 
                return true;
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AdminTokenKey);
                _authStateProvider.MarkUserAsLoggedOut();
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AdminTokenKey);
            _authStateProvider.MarkUserAsLoggedOut();
        }

        public async Task InitializeAuthenticationState()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                // Token is present; could validate and decode if needed
            }
            else
            {
                _authStateProvider.MarkUserAsLoggedOut();
            }
        }
    }
}
