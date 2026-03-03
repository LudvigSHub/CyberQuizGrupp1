using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.UI.Services
{
    //service endast för allt som har med subcategories att göra, för att separera så mycket som möjligt och göra det lättare att ändra/felsöka
    public class SubCategoryService
    {
        private readonly HttpClient _httpClient;

        public SubCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SubCategoryDTO>> GetSubCategoriesAsync(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<SubCategoryDTO>>($"api/subcategories/{categoryId}") ?? []; //abbas behöver göra en subcategory controller, kolla så endpointsen stämmer 

        }
    }
}
