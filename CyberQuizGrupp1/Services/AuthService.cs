using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.UI.Services
{

    //service som innehåller allt med authentication, login, register etc.
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //check endpoints ("api/auth/register") with api
        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto); //skickar dton i JSON format till api endpointen och väntar på ett response
            return response.IsSuccessStatusCode; //returnerar response status koden, true om det är 200-299, annars false/error, tex 400, 500 etc.
        }
    }
}
