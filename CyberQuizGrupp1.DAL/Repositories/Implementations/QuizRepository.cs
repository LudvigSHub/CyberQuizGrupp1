using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Repositories.Implementations
{
    public class QuizRepository : IQuizRepository
    {
        //injicerad databas-kontext för att prata med databasen
        private readonly AppDbContext _context;

        //konstruktor som tar emot appdbcontext via dependency injection
        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        //hämtar all quiz-data för en specifik subkategori med frågor och svarsalternativ
        public async Task<SubCategoryModel?> GetQuizDataBySubCategoryIdAsync(int subCategoryId)
        {
            //hämtar subkategorin med id och inkluderar alla frågor och deras svarsalternativ (eager loading)
            //detta gör att vi får all data i ett anrop istället för flera separata anrop
            return await _context.SubCategories
                .Include(sc => sc.Questions) //inkludera alla frågor för subkategorin
                    .ThenInclude(q => q.AnswerOptions) //inkludera alla svarsalternativ för varje fråga
                .FirstOrDefaultAsync(sc => sc.Id == subCategoryId); //hämta första subkategorin med matchande id eller null om den inte finns
        }

        //sparar ett quiz-försök i databasen
        public async Task AddQuizAttemptAsync(QuizAttemptModel quizAttempt)
        {
            //lägger till quiz-försöket i dbset (märks som "added" i change tracker)
            await _context.QuizAttempts.AddAsync(quizAttempt);
            //sparar ändringarna till databasen (kör insert-kommandot)
            await _context.SaveChangesAsync();
        }
    }
}