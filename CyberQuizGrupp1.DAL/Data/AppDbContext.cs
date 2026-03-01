using CyberQuizGrupp1.DAL.Identity;
using CyberQuizGrupp1.SHARED.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //tabeller skall vara här
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SubCategoryModel> SubCategories { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerOptionModel> AnswerOptions { get; set; }
        public DbSet<UserResultModel> UserResults { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Här kan ni konfigurera relationer senare
        }
    }
}
