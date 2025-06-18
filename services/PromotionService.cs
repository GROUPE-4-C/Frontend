using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AlumniConnect.Front.Models;
using AlumniConnect.Front.Services;

using System;


namespace AlumniConnect.Front.Services
{
    public class PromotionService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public PromotionService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            Console.WriteLine("PromotionService initialisé.");
        }

        private async Task AddAuthorizationHeaderAsync(HttpRequestMessage request)
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("PromotionService: Token d'autorisation ajouté à la requête.");
            }
            else
            {
                Console.WriteLine("PromotionService: Pas de token d'autorisation disponible. La requête sera envoyée sans.");
            }
        }

        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            Console.WriteLine("PromotionService: Tentative de récupération des promotions...");
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/promotions");
                await AddAuthorizationHeaderAsync(request);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var promotions = await response.Content.ReadFromJsonAsync<List<Promotion>>();
                Console.WriteLine($"PromotionService: {promotions?.Count ?? 0} promotions récupérées.");
                return promotions ?? new List<Promotion>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"PromotionService: Erreur HTTP lors de la récupération des promotions: {ex.StatusCode} - {ex.Message}");
                if (ex.StatusCode != null)
                {
                    Console.WriteLine($"Réponse d'erreur: {await ex.ReadResponseContentAsync()}");
                }
                return new List<Promotion>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PromotionService: Erreur inattendue lors de la récupération des promotions: {ex.Message}");
                return new List<Promotion>();
            }
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            Console.WriteLine($"PromotionService: Tentative d'ajout de la promotion '{promotion.Nom}'...");
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/promotions");
                await AddAuthorizationHeaderAsync(request);
                request.Content = JsonContent.Create(promotion);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"PromotionService: Promotion '{promotion.Nom}' ajoutée avec succès.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"PromotionService: Échec HTTP de l'ajout de la promotion '{promotion.Nom}': {ex.StatusCode} - {ex.Message}");
                if (ex.StatusCode != null)
                {
                    Console.WriteLine($"Réponse d'erreur: {await ex.ReadResponseContentAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PromotionService: Échec inattendu de l'ajout de la promotion '{promotion.Nom}': {ex.Message}");
            }
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            Console.WriteLine($"PromotionService: Tentative de mise à jour de la promotion '{promotion.Nom}' (ID: {promotion.Id})...");
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"/api/promotions/{promotion.Id}");
                await AddAuthorizationHeaderAsync(request);
                request.Content = JsonContent.Create(promotion);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"PromotionService: Promotion '{promotion.Nom}' (ID: {promotion.Id}) mise à jour avec succès.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"PromotionService: Échec HTTP de la mise à jour de la promotion '{promotion.Nom}': {ex.StatusCode} - {ex.Message}");
                if (ex.StatusCode != null)
                {
                    Console.WriteLine($"Réponse d'erreur: {await ex.ReadResponseContentAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PromotionService: Échec inattendu de la mise à jour de la promotion '{promotion.Nom}': {ex.Message}");
            }
        }

        public async Task DeletePromotionAsync(int id)
        {
            Console.WriteLine($"PromotionService: Tentative de suppression de la promotion (ID: {id})...");
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/promotions/{id}");
                await AddAuthorizationHeaderAsync(request);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"PromotionService: Promotion (ID: {id}) supprimée avec succès.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"PromotionService: Échec HTTP de la suppression de la promotion (ID: {id}): {ex.StatusCode} - {ex.Message}");
                if (ex.StatusCode != null)
                {
                    Console.WriteLine($"Réponse d'erreur: {await ex.ReadResponseContentAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PromotionService: Échec inattendu de la suppression de la promotion (ID: {id}): {ex.Message}");
            }
        }
    }

    public static class HttpRequestExceptionExtensions
    {
        public static async Task<string> ReadResponseContentAsync(this HttpRequestException ex)
        {
            if (ex.Data.Contains("HttpResponseMessage") && ex.Data["HttpResponseMessage"] is HttpResponseMessage response)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "N/A";
        }
    }
}