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

        //hämtar ett quiz-försök baserat på attemptid
        public async Task<QuizAttemptModel?> GetQuizAttemptByIdAsync(Guid attemptId)
        {
            //hämtar första quiz-försöket med matchande id eller null om det inte finns
            return await _context.QuizAttempts
                .FirstOrDefaultAsync(a => a.Id == attemptId);
        }

        //hämtar en fråga med alla dess svarsalternativ
        public async Task<QuestionModel?> GetQuestionWithAnswerOptionsAsync(int questionId)
        {
            //hämtar frågan och inkluderar alla svarsalternativ (eager loading)
            return await _context.Questions
                .Include(q => q.AnswerOptions) //inkludera alla svarsalternativ för frågan
                .FirstOrDefaultAsync(q => q.Id == questionId); //hämta första frågan med matchande id eller null om den inte finns
        }

        //sparar ett användarsvar i databasen
        public async Task AddUserAnswerAsync(UserAnswerModel userAnswer)
        {
            //lägger till användarsvaret i dbset (märks som "added" i change tracker)
            await _context.UserAnswers.AddAsync(userAnswer);
            //sparar ändringarna till databasen (kör insert-kommandot)
            await _context.SaveChangesAsync();
        }

        //hämtar alla användarsvar för ett specifikt quiz-försök
        public async Task<List<UserAnswerModel>> GetUserAnswersByAttemptIdAsync(Guid attemptId)
        {
            //hämtar alla användarsvar som tillhör det specifika försöket
            return await _context.UserAnswers
                .Where(ua => ua.AttemptId == attemptId) //filtrera på attemptid
                .ToListAsync(); //konvertera till lista
        }

        //markerar ett quiz-försök som avslutat genom att sätta finishedat
        public async Task MarkQuizAttemptAsFinishedAsync(Guid attemptId)
        {
            //hämta quiz-försöket från databasen
            var attempt = await _context.QuizAttempts.FirstOrDefaultAsync(a => a.Id == attemptId);

            //om försöket finns, sätt finishedat till nuvarande tid
            if (attempt != null)
            {
                attempt.FinishedAt = DateTime.UtcNow; //använd utc för konsekvent tidszon
                await _context.SaveChangesAsync(); //spara ändringen
            }
        }

        //hämtar alla användarsvar för en specifik användare och subkategori
        //inkluderar både rätta och felaktiga svar så att bll kan göra den slutgiltiga analysen
        public async Task<List<UserAnswerModel>> GetUserAnswersByUserAndSubCategoryAsync(string userId, int subCategoryId)
        {
            //hämtar alla användarsvar genom att filtrera på userid och subkategoryid
            //inkluderar quiz-försök, fråga med svarsalternativ och det valda svaret för fullständig analys
            return await _context.UserAnswers
                .Include(ua => ua.Attempt) //inkludera quiz-försöket för att kunna filtrera på subkategori
                .Include(ua => ua.Question) //inkludera frågan för användarsvaret
                    .ThenInclude(q => q.AnswerOptions) //inkludera alla svarsalternativ för frågan
                .Include(ua => ua.SelectedAnswerOption) //inkludera det svar användaren valde
                .Where(ua => ua.UserId == userId && ua.Attempt.SubCategoryId == subCategoryId) //filtrera på användare och subkategori
                .ToListAsync(); //konvertera till lista
        }
    }
}