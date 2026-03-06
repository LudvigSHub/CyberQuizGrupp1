using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.UI.Services
{
    // 1.skapa http client service
    // 2.lägg in i program.cs
    //3. anävnd i pages/components
    public class QuizService
    {
        private readonly HttpClient _httpClient;
        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //hämta quizet, 
        public async Task<StartQuizDTO> GetQuizAsync(int subCategoryId)
        {
            return await _httpClient.GetFromJsonAsync<StartQuizDTO>($"api/quiz/start/{subCategoryId}");
        }
    }
}
