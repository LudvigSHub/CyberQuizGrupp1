using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CyberQuizGrupp1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        //injicera quizservice för business logik
        private readonly IQuizService _quizService;

        //tar emot IQuizService via dependency injection och sparar den i _quizService
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        //GET: api/quiz/start?subCategoryId
        //startar ett nytt quiz för en specifik subkategori och användare
        [HttpGet("start/{subCategoryId}")]
        public async Task<ActionResult<StartQuizDTO>> StartQuiz(int subCategoryId, [FromQuery] string userId)
        {
            //snabb validering av input
            if (subCategoryId <= 0)
                return BadRequest("subcategoryid must be greater than 0");

            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("userid cannot be empty");

            //kalla bll för att starta quizet
            var quiz = await _quizService.StartQuizAsync(subCategoryId, userId);

            //om subkategorin inte finns returnera 404
            if (quiz == null)
                return NotFound($"subcategory with id {subCategoryId} not found");

            //returnera quiz-data med 200 ok
            return Ok(quiz);
        }

        //POST: api/quiz/answer
        //skickar in ett svar på en quiz-fråga och får feedback om det var rätt eller fel
        [HttpPost("answer")]
        public async Task<ActionResult<AnswerFeedbackDTO>> SubmitAnswer([FromBody] SubmitAnswerDTO dto)
        {
            //kalla bll för att hantera svaret
            var result = await _quizService.SubmitAnswerAsync(dto);

            //om svaret inte kunde sparas returnera 400
            if (result == null)
                return BadRequest("could not submit answer");

            //returnera feedback med 200 ok
            return Ok(result);
        }

        //POST: api/quiz/finish
        //avslutar ett quiz och returnerar resultatet
        [HttpPost("finish")]
        public async Task<ActionResult<QuizResultDTO>> FinishQuiz([FromBody] FinishQuizDTO dto)
        {
            //kalla bll för att avsluta quizet
            var result = await _quizService.FinishQuizAsync(dto);

            //om quizet inte kunde avslutas returnera 400
            if (result == null)
                return BadRequest("could not finish quiz");

            //returnera quiz-resultat med 200 ok
            return Ok(result);
        }
    }
}