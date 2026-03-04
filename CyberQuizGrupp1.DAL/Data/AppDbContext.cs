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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // -------------------------
            // UNIQUE INDEXES (förhindra dubletter)
            // -------------------------
            // Category: unik Name

            modelBuilder.Entity<CategoryModel>()
                .HasIndex(x => x.Name)
                .IsUnique();
            // SubCategory: unik (CategoryId, Name)
            modelBuilder.Entity<SubCategoryModel>()
                .HasIndex(x => new { x.CategoryId, x.Name })
                .IsUnique();
            // Question: unik (SubCategoryId, Text)
            modelBuilder.Entity<QuestionModel>()
                .HasIndex(x => new { x.SubCategoryId, x.Text })
                .IsUnique();
            // AnswerOption: unik (QuestionId, Text)
            modelBuilder.Entity<AnswerOptionModel>()
                .HasIndex(x => new { x.QuestionId, x.Text })
                .IsUnique();
            // -------------------------
            // OPTIONAL: RELATIONSHIPS (om ni har navigation properties)
            // Kommentera in om ni har t.ex:
            // SubCategoryModel.Category, CategoryModel.SubCategories
            // QuestionModel.SubCategory, SubCategoryModel.Questions
            // AnswerOptionModel.Question, QuestionModel.AnswerOptions
            // -------------------------
            // modelBuilder.Entity<SubCategoryModel>()
            //     .HasOne(sc => sc.Category)
            //     .WithMany(c => c.SubCategories)
            //     .HasForeignKey(sc => sc.CategoryId)
            //     .OnDelete(DeleteBehavior.Restrict);
            // modelBuilder.Entity<QuestionModel>()
            //     .HasOne(q => q.SubCategory)
            //     .WithMany(sc => sc.Questions)
            //     .HasForeignKey(q => q.SubCategoryId)
            //     .OnDelete(DeleteBehavior.Restrict);
            // modelBuilder.Entity<AnswerOptionModel>()
            //     .HasOne(a => a.Question)
            //     .WithMany(q => q.AnswerOptions)
            //     .HasForeignKey(a => a.QuestionId)
            //     .OnDelete(DeleteBehavior.Cascade);
            // -------------------------
            // OPTIONAL: MANUELLA ID:N (endast om ni vill äga Id och INTE använda IDENTITY)
            // OBS: kräver i praktiken clean reset/ny migration om DB redan skapats med IDENTITY.
            // -------------------------
            // modelBuilder.Entity<CategoryModel>().Property(x => x.Id).ValueGeneratedNever();
            // modelBuilder.Entity<SubCategoryModel>().Property(x => x.Id).ValueGeneratedNever();
            // modelBuilder.Entity<QuestionModel>().Property(x => x.Id).ValueGeneratedNever();
            // modelBuilder.Entity<AnswerOptionModel>().Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
