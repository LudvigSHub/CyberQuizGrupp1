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

        //string i usedId som parameter, eftersom identity sparar användarens id som string
        public async Task<List<SubCategoryDTO>> GetSubCategoriesAsync(int categoryId, string userId)
        {
            var url = $"api/subcategories/{categoryId}?userId={userId}";
            return await _httpClient.GetFromJsonAsync<List<SubCategoryDTO>>(url) ?? [];

            //kör det till vänster om ?? (url) ifall det inte är null, om det är null körs []; istället vilket innebär att en ny tom lista skapas för att undvika NullReferenceException
            //annars skulle appen krasha om man loopar genom det i tex en foreach 

            //tidigare:
            //return await _httpClient.GetFromJsonAsync<List<SubCategoryDTO>>($"api/categories/{categoryId}/subcategories?userId={userId}") ?? [];  

        }
    }
}
