using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CyberQuizGrupp1.UI.Services
{
    //service för allt som har med categories att göra, för att separera så mycket som möjligt och göra det lättare att ändra/felsöka
    public class CategoryService
    {

        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDTO>>($"api/categories/{userId}") ?? [];
        }
    }
}
