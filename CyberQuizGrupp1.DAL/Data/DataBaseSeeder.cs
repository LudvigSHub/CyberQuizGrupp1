using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Text;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new CategoryModel { Name = "Nätverkssäkerhet" },
                new CategoryModel { Name = "Kryptografi & Säker kommunikation" },
                new CategoryModel { Name = "Identitet, Åtkomst & Autentisering (IAM)" }
            );

            await context.SaveChangesAsync();
        }
    }
}