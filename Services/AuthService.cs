using AlumniConnect.Front.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Text;

namespace AlumniConnect.Front.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "authToken";
        private const string UserKey = "currentUser";
        private UserInfo? _currentUser;
        public UserInfo? CurrentUser => _currentUser;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/register", dto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<string>
                    {
                        Success = true,
                        Data = content,
                        Message = "Inscription réussie. Vérifiez votre email pour le code OTP."
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return await HandleErrorResponse<string>(response, errorContent);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<string>> ConfirmEmailAsync(ConfirmEmailDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/confirm-email", dto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<string>
                    {
                        Success = true,
                        Data = content,
                        Message = "Email confirmé avec succès !"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return await HandleErrorResponse<string>(response, errorContent);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<string>> ResendOtpAsync(ResendOtpDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/resend-otp", dto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<string>
                    {
                        Success = true,
                        Data = content,
                        Message = "Un nouveau code OTP a été envoyé à votre email."
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return await HandleErrorResponse<string>(response, errorContent);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", dto);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (loginResponse != null)
                    {
                        await StoreAuthDataAsync(loginResponse.Token, loginResponse.User);
                        SetAuthorizationHeader(loginResponse.Token);
                        _currentUser = loginResponse.User;

                        return new ApiResponse<LoginResponse>
                        {
                            Success = true,
                            Data = loginResponse,
                            Message = "Connexion réussie !"
                        };
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return await HandleErrorResponse<LoginResponse>(response, errorContent);
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoginResponse>
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<string>> RequestResetPasswordOtpAsync(ResendOtpDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/request-reset-password-otp", dto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<string>
                    {
                        Success = true,
                        Data = content,
                        Message = "Un code OTP de réinitialisation a été envoyé à votre email."
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return await HandleErrorResponse<string>(response, errorContent);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/reset-password", dto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<string>
                    {
                        Success = true,
                        Data = content,
                        Message = "Mot de passe réinitialisé avec succès."
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return await HandleErrorResponse<string>(response, errorContent);
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur",
                    Errors = new[] { ex.Message }
                };
            }
        }

        private Task<ApiResponse<T>> HandleErrorResponse<T>(HttpResponseMessage response, string errorContent)
        {
            try
            {
                // Tenter de désérialiser la réponse JSON d'erreur
                var errorResponse = JsonSerializer.Deserialize<dynamic>(errorContent);
                return Task.FromResult(new ApiResponse<T>
                {
                    Success = false,
                    Message = errorContent,
                    Errors = new[] { errorContent }
                });
            }
            catch
            {
                return Task.FromResult(new ApiResponse<T>
                {
                    Success = false,
                    Message = errorContent,
                    Errors = new[] { errorContent }
                });
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _currentUser = null;
        }

        private async Task StoreAuthDataAsync(string token, UserInfo user)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserKey, JsonSerializer.Serialize(user));
        }

        private void SetAuthorizationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }

        public async Task<UserInfo?> GetCurrentUserAsync()
        {
            var userJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", UserKey);
            return string.IsNullOrEmpty(userJson) ? null : JsonSerializer.Deserialize<UserInfo>(userJson);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task InitializeAsync()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                SetAuthorizationHeader(token);
                _currentUser = await GetCurrentUserAsync();
            }
        }
    }
}
