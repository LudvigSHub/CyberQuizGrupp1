using CyberQuizGrupp1.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Data
{
    //static klass för att seeda en testanvändare i databasen vid app-start
    public static class IdentitySeeder
    {
        //skapar en testanvändare om den inte redan finns
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            //försök hitta användare med användarnamnet "user"
            var user = await userManager.FindByNameAsync("user");

            //om användaren inte finns, skapa en ny
            if (user == null)
            {
                //skapa ny applicationuser med användarnamn och email
                user = new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@cyberquiz.com"
                };

                //skapa användaren med lösenord "Password1234!" i databasen
                await userManager.CreateAsync(user, "Password1234!");
            }
        }
    }
}