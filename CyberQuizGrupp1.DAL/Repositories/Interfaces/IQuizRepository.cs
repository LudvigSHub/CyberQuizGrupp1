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
    }
}
