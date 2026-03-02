using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.UI.Services
{
    public class CategoryService
    {

        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDTO>>("api/categories") ?? [];
        }
    }
}
