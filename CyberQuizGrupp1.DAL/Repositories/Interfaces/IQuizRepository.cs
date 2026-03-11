using CyberQuizGrupp1.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        //hämtar all quiz-data för en specifik subkategori
        //används när ett quiz startas för att ladda frågor och svar kopplade till subkategorin
        Task<SubCategoryModel?> GetQuizDataBySubCategoryIdAsync(int subCategoryId);

        //sparar ett genomfört quizförsök i databasen
        //innehåller information som användare, resultat och tidsstämpel
        Task AddQuizAttemptAsync(QuizAttemptModel quizAttempt);

        //hämtar ett quiz-försök baserat på attemptid
        //används när man ska hämta info om ett pågående quiz
        Task<QuizAttemptModel?> GetQuizAttemptByIdAsync(Guid attemptId);

        //hämtar en fråga med alla dess svarsalternativ
        //används när man ska validera ett svar
        Task<QuestionModel?> GetQuestionWithAnswerOptionsAsync(int questionId);

        //sparar ett användarsvar i databasen
        //innehåller info om vilket svar användaren valde och om det var rätt
        Task AddUserAnswerAsync(UserAnswerModel userAnswer);

        //hämtar alla användarsvar för ett specifikt quiz-försök
        //används när man ska räkna ut slutresultatet
        Task<List<UserAnswerModel>> GetUserAnswersByAttemptIdAsync(Guid attemptId);

        //markerar ett quiz-försök som avslutat genom att sätta finishedAt
        //används när användaren slutför quizet
        Task MarkQuizAttemptAsFinishedAsync(Guid attemptId);

        //hämtar alla felaktiga användarsvar för en specifik användare och subkategori
        //används i coaching-flödet för att analysera vilka frågor användaren ofta svarar fel på
        Task<List<UserAnswerModel>> GetIncorrectUserAnswersByUserAndSubCategoryAsync(string userId, int subCategoryId);

    }
}
