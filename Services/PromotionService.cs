using System.Net.Http.Json;
using AlumniConnect.Front.Models;

namespace AlumniConnect.Front.Services
{
    public class PromotionService
    {
        private readonly HttpClient _httpClient;

        public PromotionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PromotionModel>> GetAllPromotionsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PromotionModel>>("/api/promotions");
                return response ?? new List<PromotionModel>();
            }
            catch (Exception ex)
            {
                // Log l'erreur si nécessaire
                Console.WriteLine($"Erreur lors du chargement des promotions: {ex.Message}");
                return new List<PromotionModel>();
            }
        }

        public async Task<PromotionModel?> GetPromotionByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PromotionModel>($"/api/promotions/{id}");
            }
            catch
            {
                return null;
            }
        }
    }
}
