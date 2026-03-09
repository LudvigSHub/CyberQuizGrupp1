using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CyberQuizGrupp1.UI.Services
{
    // 1.skapa http client service
    // 2.lägg in i program.cs
    // 3.anävnd i pages/components

    //felmeddelande som skrivs i service är det man ser i tex devtools.
    //service hanterar tekninska fel / http-fel / api-fel. i pagen skriver man ut det felmeddelande som anävndaren ska se
    public class QuizService
    {
        private readonly HttpClient _httpClient;

        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //SÅHÄR SER ROUTES/ENDPOINTS I APIET UT:

        //[ApiController]
        //[Route("api/[controller]")]  // = "api/quiz" (Controllern heter QuizController så när man bara sätter [controller] skrivs namnet före Controller ut)
        //public class QuizController : ControllerBase
        //{
        //    [HttpPost("answer")]  // = "api/quiz/answer"
        //    public async Task<ActionResult<AnswerFeedbackDTO>> SubmitAnswer(SubmitAnswerDTO dto)
        //    {
        //        // tar emot dto här
        //    }
        //}


        //GET från api (GETfromjsonasync) hämta quizet i denna metod, skicka med id för subkategorin, får tillbaka tillhörande quiz som är av typen StartQuizDTO
        public async Task<StartQuizDTO> GetQuizAsync(int subCategoryId, string userId)
        {
            var url = $"api/quiz/start/{subCategoryId}?userId={userId}";
            return await _httpClient.GetFromJsonAsync<StartQuizDTO>(url);
        }

        //POST till api (POSTasjasonasync) skicka svaret (vill skicka questionId (frågan svaren tillhör) och answerId (id:t på svaret man valde), men de är ju i SubmittedAnswerDTO så skicka den)
        //jag får tillbaka en AnswerFeedBackDTO
        //lägg till ? efter vad som ska returneras eftersom vi då lovar att returnera antingen ett objekt elelr null (returnerar null längre enr i metoden). utan ? MÅSET vi alltid returnera ett objekt, aldirg null. returnerar vi null utan  ? får vi kompileringsfel
        public async Task<AnswerFeedbackDTO?> SubmitAnswerAsync(SubmitAnswerDTO dto)
        {
            //skicka repsonset till api:et
            //om det inte lyckas - printa felmeddelande
            //annars returnera 


            //response är det jag skickar till api:et (när man gör en post postar man repsonset)
            //PostAsJsonAsync tar emot två parametrar. 1 - endpointed i apiet/url:en "api/quiz/answer" (dit vi skickar) och 2 - det vi skcikar (dton). POSTen heter answer i apiet
            var response = await _httpClient.PostAsJsonAsync("api/quiz/answer",dto);

            if(!response.IsSuccessStatusCode)
            {
                //hanterar fel, felmeddelande som skrivs i service (med console.writeline) är det man ser i tex devtools. service hanterar tekninska fel / http-fel / api-fel. i pagen skriver man ut det felmeddelande som anävndaren ska se
                //hämtar statuskoden och tillhörande felmeddelande och printar båda i consolen så man vet vad som är fel 
                Console.WriteLine($"{response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return null;
            }

            //response.Content hämtar json texten av http-svaret (response). ReadFromJsonAsync<AnswerFeedbackDTO>() omvandlar response.content från json till ett AnswerFeedBackDTO objekt
            //det som returneras är ett AnswerFeedBackDTO objekt med värdena. detta värdet hämtar vi sedan upp för att visa i pagen: var result = await QuizService.SubmitAnswerAsync(dto); result är nu ett AnswerFeedbackDTO-objekt
            return await response.Content.ReadFromJsonAsync<AnswerFeedbackDTO>();
        }

        //POST 
        public async Task<QuizResultDTO?> FinishedQuizAsync(FinishQuizDTO dto)
        {
            //skicka endpointed (finish är posten i apiet) + dto som parametrar
            var response = await _httpClient.PostAsJsonAsync("api/quiz/finish", dto);
            
            if (!response.IsSuccessStatusCode)
            {
                //hämtar statuskoden och tillhörande felmeddelande och printar båda i consolen så man vet vad som är fel 
                Console.WriteLine($"{response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return null;
            }

            //får tillbaka ett objekt av QuizResultDTO fyllt med infon vi behöver displaya till användaren i pagen: var result = await QuizService.FinishedQuizAsync(dto); result är nu ett QuizResultDTO-objekt
            return await response.Content.ReadFromJsonAsync<QuizResultDTO>();
        }
    }
}
