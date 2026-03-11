using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.BLL.Interfaces
{
    public interface IAiCoachClient
    {
        //alla klasser som ärver av IAiCoachClient måste innehålla denna metod som tar emot en prompt som parameter och returnerar en string med coachingtext: 
        //vi gör detta för att lättare kunna ändra mellan olika AI, om vi vill ändra (tex från ollama till openai) så skapar vi bara en ny implementation som ärver av detta interface (tex public class OllamaCoachClient : IAiCoachClient eller public class OpenAiCoachClient : IAiCoachClient och anropar ai apiet där i
        //vi vill INTE hårdkoda servicen till en specifik AI inne i servicen (tex såhär: var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", ...); 
        Task<string> GetCoachingTextAsync(string prompt);
    }
}
