using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.Identity.Client;

namespace CyberQuizGrupp1.UI.Services
{
    public class CoachingService
    {
        private readonly HttpClient _httpClient;

        public CoachingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //två olika sätt att hantera fel på (med tillhörande metoder i AiCoaching.razor:
        public async Task<List<CoachingItemDTO>?> GetFailedSubCategoriesAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/coaching/failed?userId={userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<CoachingItemDTO>>();
        }
        public async Task<CoachingResponseDTO?> GetCoachingAsync(int subCategoryId, string userId)
        {
            return await _httpClient.GetFromJsonAsync<CoachingResponseDTO>(
                $"api/coaching/{subCategoryId}?userId={userId}");
        }
        //----------------------------------------------------------------------

        public async Task<UserProgressDTO?> GetUserProgressAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/coaching/progress?userId={userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserProgressDTO>();
        }
    }
}
