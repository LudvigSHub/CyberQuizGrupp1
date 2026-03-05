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
            

            
        
            base.OnModelCreating(modelBuilder);

            // -------------------------
            // UNIQUE INDEXES (förhindra dubletter)
            // -------------------------
            modelBuilder.Entity<CategoryModel>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<SubCategoryModel>()
                .HasIndex(x => new { x.CategoryId, x.Name })
                .IsUnique();

            modelBuilder.Entity<QuestionModel>()
                .HasIndex(x => new { x.SubCategoryId, x.Text })
                .IsUnique();

            modelBuilder.Entity<AnswerOptionModel>()
                .HasIndex(x => new { x.QuestionId, x.Text })
                .IsUnique();

            // -------------------------
            // SEED DATA (fasta Id:n) via HasData
            // -------------------------

            // Categories
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "Nätverkssäkerhet" },
                new CategoryModel { Id = 2, Name = "Kryptografi & Säker kommunikation" },
                new CategoryModel { Id = 3, Name = "Identitet, Åtkomst & Autentisering (IAM)" }
            );

            // SubCategories
            modelBuilder.Entity<SubCategoryModel>().HasData(
                // Category 1
                new SubCategoryModel { Id = 1, CategoryId = 1, Name = "Phishing & Social Engineering", IsLocked = false },
                new SubCategoryModel { Id = 2, CategoryId = 1, Name = "Brandväggar & Segmentering", IsLocked = true },
                new SubCategoryModel { Id = 3, CategoryId = 1, Name = "Wi-Fi & Trådlös säkerhet", IsLocked = true },

                // Category 2
                new SubCategoryModel { Id = 4, CategoryId = 2, Name = "Hashning & Integritet", IsLocked = false },
                new SubCategoryModel { Id = 5, CategoryId = 2, Name = "Symmetrisk & Asymmetrisk kryptering", IsLocked = true },
                new SubCategoryModel { Id = 6, CategoryId = 2, Name = "TLS/HTTPS & Certifikat", IsLocked = true },

                // Category 3
                new SubCategoryModel { Id = 7, CategoryId = 3, Name = "Lösenord & MFA", IsLocked = false },
                new SubCategoryModel { Id = 8, CategoryId = 3, Name = "RBAC/ABAC & Principer", IsLocked = true },
                new SubCategoryModel { Id = 9, CategoryId = 3, Name = "Sessions, Tokens & SSO", IsLocked = true }
            );

            // Questions (Q1–Q36)
            modelBuilder.Entity<QuestionModel>().HasData(
                // SubCategory 1 (Q1–Q4)
                new QuestionModel { Id = 1, SubCategoryId = 1, Text = "Vad är phishing?" },
                new QuestionModel { Id = 2, SubCategoryId = 1, Text = "Vilket är ett vanligt tecken på ett phishing-mejl?" },
                new QuestionModel { Id = 3, SubCategoryId = 1, Text = "Vad är social engineering?" },
                new QuestionModel { Id = 4, SubCategoryId = 1, Text = "Vad bör du göra om du klickat på en misstänkt länk på jobbet?" },

                // SubCategory 2 (Q5–Q8)
                new QuestionModel { Id = 5, SubCategoryId = 2, Text = "Vad är huvudsyftet med en brandvägg?" },
                new QuestionModel { Id = 6, SubCategoryId = 2, Text = "Vad innebär nätverkssegmentering?" },
                new QuestionModel { Id = 7, SubCategoryId = 2, Text = "Vilken regel är mest riskfylld i en brandvägg?" },
                new QuestionModel { Id = 8, SubCategoryId = 2, Text = "Vilken princip bör gälla för öppna portar ut mot internet?" },

                // SubCategory 3 (Q9–Q12)
                new QuestionModel { Id = 9, SubCategoryId = 3, Text = "Vilket är säkrast för ett hemnät: WPA3, WPA2 eller öppet Wi-Fi?" },
                new QuestionModel { Id = 10, SubCategoryId = 3, Text = "Varför är WPS ofta en säkerhetsrisk?" },
                new QuestionModel { Id = 11, SubCategoryId = 3, Text = "Vad är en 'evil twin'-attack?" },
                new QuestionModel { Id = 12, SubCategoryId = 3, Text = "Vilken åtgärd är bäst för gästnätverk?" },

                // SubCategory 4 (Q13–Q16)
                new QuestionModel { Id = 13, SubCategoryId = 4, Text = "Vad används en hashfunktion främst till?" },
                new QuestionModel { Id = 14, SubCategoryId = 4, Text = "Vilken egenskap är viktig för en säker hashfunktion?" },
                new QuestionModel { Id = 15, SubCategoryId = 4, Text = "Varför används 'salt' vid lagring av lösenord?" },
                new QuestionModel { Id = 16, SubCategoryId = 4, Text = "Varför räcker inte en hash som 'kryptering'?" },

                // SubCategory 5 (Q17–Q20)
                new QuestionModel { Id = 17, SubCategoryId = 5, Text = "Vad är skillnaden mellan symmetrisk och asymmetrisk kryptering?" },
                new QuestionModel { Id = 18, SubCategoryId = 5, Text = "Vilket är ett exempel på symmetrisk kryptering?" },
                new QuestionModel { Id = 19, SubCategoryId = 5, Text = "Varför används ofta hybridkryptografi?" },
                new QuestionModel { Id = 20, SubCategoryId = 5, Text = "Vad används en publik nyckel till?" },

                // SubCategory 6 (Q21–Q24)
                new QuestionModel { Id = 21, SubCategoryId = 6, Text = "Vad skyddar TLS (HTTPS) främst mot?" },
                new QuestionModel { Id = 22, SubCategoryId = 6, Text = "Vad är ett digitalt certifikat?" },
                new QuestionModel { Id = 23, SubCategoryId = 6, Text = "Vad betyder 'chain of trust' för certifikat?" },
                new QuestionModel { Id = 24, SubCategoryId = 6, Text = "Vilket är ett varningstecken i webbläsaren kopplat till TLS?" },

                // SubCategory 7 (Q25–Q28)
                new QuestionModel { Id = 25, SubCategoryId = 7, Text = "Varför är MFA säkrare än endast lösenord?" },
                new QuestionModel { Id = 26, SubCategoryId = 7, Text = "Vilket lösenord är starkast?" },
                new QuestionModel { Id = 27, SubCategoryId = 7, Text = "Vad är credential stuffing?" },
                new QuestionModel { Id = 28, SubCategoryId = 7, Text = "Vilken rekommendation är bäst för lösenordshantering?" },

                // SubCategory 8 (Q29–Q32)
                new QuestionModel { Id = 29, SubCategoryId = 8, Text = "Vad betyder RBAC?" },
                new QuestionModel { Id = 30, SubCategoryId = 8, Text = "Vad innebär principen om minsta privilegium (least privilege)?" },
                new QuestionModel { Id = 31, SubCategoryId = 8, Text = "Vad är skillnaden mellan RBAC och ABAC?" },
                new QuestionModel { Id = 32, SubCategoryId = 8, Text = "Varför är administratörskonton särskilt känsliga?" },

                // SubCategory 9 (Q33–Q36)
                new QuestionModel { Id = 33, SubCategoryId = 9, Text = "Vad är en session i webbsammanhang?" },
                new QuestionModel { Id = 34, SubCategoryId = 9, Text = "Vad är skillnaden mellan en cookie och en token?" },
                new QuestionModel { Id = 35, SubCategoryId = 9, Text = "Vad står SSO för?" },
                new QuestionModel { Id = 36, SubCategoryId = 9, Text = "Vad är en risk med för lång session-livslängd?" }
            );

            // AnswerOptions (A1–A108) – 3 per fråga
            modelBuilder.Entity<AnswerOptionModel>().HasData(
                // Q1
                new AnswerOptionModel { Id = 1, QuestionId = 1, Text = "Ett försök att lura dig att lämna ut känslig information via falska meddelanden", IsCorrect = true },
                new AnswerOptionModel { Id = 2, QuestionId = 1, Text = "En metod för att kryptera filer på hårddisken", IsCorrect = false },
                new AnswerOptionModel { Id = 3, QuestionId = 1, Text = "Ett verktyg för att blockera nätverkstrafik", IsCorrect = false },

                // Q2
                new AnswerOptionModel { Id = 4, QuestionId = 2, Text = "Avsändaradressen/domänen är misstänkt eller felstavad", IsCorrect = true },
                new AnswerOptionModel { Id = 5, QuestionId = 2, Text = "Mejlet kommer alltid från en känd kontakt", IsCorrect = false },
                new AnswerOptionModel { Id = 6, QuestionId = 2, Text = "Mejlet saknar alltid bilagor", IsCorrect = false },

                // Q3
                new AnswerOptionModel { Id = 7, QuestionId = 3, Text = "Manipulation av människor för att få dem att göra något som gynnar angriparen", IsCorrect = true },
                new AnswerOptionModel { Id = 8, QuestionId = 3, Text = "En portskanning av ett nätverk", IsCorrect = false },
                new AnswerOptionModel { Id = 9, QuestionId = 3, Text = "En automatiserad lösenordsattack mot en server", IsCorrect = false },

                // Q4
                new AnswerOptionModel { Id = 10, QuestionId = 4, Text = "Rapportera enligt rutin (IT/Helpdesk/SOC) och följ incidentprocessen", IsCorrect = true },
                new AnswerOptionModel { Id = 11, QuestionId = 4, Text = "Skicka länken vidare så andra kan varna sig", IsCorrect = false },
                new AnswerOptionModel { Id = 12, QuestionId = 4, Text = "Ignorera det och hoppas att inget händer", IsCorrect = false },

                // Q5
                new AnswerOptionModel { Id = 13, QuestionId = 5, Text = "Att filtrera och kontrollera nätverkstrafik enligt regler", IsCorrect = true },
                new AnswerOptionModel { Id = 14, QuestionId = 5, Text = "Att skapa nya lösenord åt användare", IsCorrect = false },
                new AnswerOptionModel { Id = 15, QuestionId = 5, Text = "Att öka Wi-Fi-hastigheten", IsCorrect = false },

                // Q6
                new AnswerOptionModel { Id = 16, QuestionId = 6, Text = "Att dela upp nätet i separata zoner för att begränsa spridning vid intrång", IsCorrect = true },
                new AnswerOptionModel { Id = 17, QuestionId = 6, Text = "Att slå ihop alla nät till ett för enklare drift", IsCorrect = false },
                new AnswerOptionModel { Id = 18, QuestionId = 6, Text = "Att kryptera all intern trafik automatiskt", IsCorrect = false },

                // Q7
                new AnswerOptionModel { Id = 19, QuestionId = 7, Text = "Att tillåta all inkommande trafik från internet (0.0.0.0/0) till interna system", IsCorrect = true },
                new AnswerOptionModel { Id = 20, QuestionId = 7, Text = "Att blockera en specifik port som inte används", IsCorrect = false },
                new AnswerOptionModel { Id = 21, QuestionId = 7, Text = "Att logga nekad trafik för felsökning", IsCorrect = false },

                // Q8
                new AnswerOptionModel { Id = 22, QuestionId = 8, Text = "Exponera bara det som behövs (minimera attackytan) och stäng resten", IsCorrect = true },
                new AnswerOptionModel { Id = 23, QuestionId = 8, Text = "Öppna alla standardportar så tjänster fungerar direkt", IsCorrect = false },
                new AnswerOptionModel { Id = 24, QuestionId = 8, Text = "Öppna portar temporärt utan loggning", IsCorrect = false },

                // Q9
                new AnswerOptionModel { Id = 25, QuestionId = 9, Text = "WPA3 är säkrast (därefter WPA2); öppet Wi-Fi är minst säkert", IsCorrect = true },
                new AnswerOptionModel { Id = 26, QuestionId = 9, Text = "Öppet Wi-Fi är säkrast eftersom det inte har lösenord", IsCorrect = false },
                new AnswerOptionModel { Id = 27, QuestionId = 9, Text = "WPA2 är alltid osäkert och ska aldrig användas", IsCorrect = false },

                // Q10
                new AnswerOptionModel { Id = 28, QuestionId = 10, Text = "WPS kan vara sårbart (t.ex. PIN-baserade attacker) och gör intrång enklare", IsCorrect = true },
                new AnswerOptionModel { Id = 29, QuestionId = 10, Text = "WPS gör nätet snabbare men mindre stabilt", IsCorrect = false },
                new AnswerOptionModel { Id = 30, QuestionId = 10, Text = "WPS krävs för att kunna använda WPA3", IsCorrect = false },

                // Q11
                new AnswerOptionModel { Id = 31, QuestionId = 11, Text = "Ett falskt Wi-Fi-nät som imiterar ett legitimt för att få dig att ansluta", IsCorrect = true },
                new AnswerOptionModel { Id = 32, QuestionId = 11, Text = "En attack där man gissar routerns admin-lösenord via Bluetooth", IsCorrect = false },
                new AnswerOptionModel { Id = 33, QuestionId = 11, Text = "En teknik för att öka räckvidden på en router", IsCorrect = false },

                // Q12
                new AnswerOptionModel { Id = 34, QuestionId = 12, Text = "Skapa ett separat gästnät (separat VLAN/isolering) med begränsad åtkomst", IsCorrect = true },
                new AnswerOptionModel { Id = 35, QuestionId = 12, Text = "Låt gäster använda samma SSID och lösenord som interna nätet", IsCorrect = false },
                new AnswerOptionModel { Id = 36, QuestionId = 12, Text = "Stäng av lösenord helt för att undvika support", IsCorrect = false },

                // Q13
                new AnswerOptionModel { Id = 37, QuestionId = 13, Text = "Att skapa ett fingeravtryck (digest) för integritet/identifiering", IsCorrect = true },
                new AnswerOptionModel { Id = 38, QuestionId = 13, Text = "Att göra data oläsbar och sedan läsbar igen med nyckel", IsCorrect = false },
                new AnswerOptionModel { Id = 39, QuestionId = 13, Text = "Att komprimera data för att spara utrymme", IsCorrect = false },

                // Q14
                new AnswerOptionModel { Id = 40, QuestionId = 14, Text = "Motstånd mot kollisioner (svårt att hitta två olika inputs med samma hash)", IsCorrect = true },
                new AnswerOptionModel { Id = 41, QuestionId = 14, Text = "Att den alltid producerar olika längd på output", IsCorrect = false },
                new AnswerOptionModel { Id = 42, QuestionId = 14, Text = "Att den går att reversera snabbt", IsCorrect = false },

                // Q15
                new AnswerOptionModel { Id = 43, QuestionId = 15, Text = "För att göra rainbow tables och förberäknade attacker mycket svårare", IsCorrect = true },
                new AnswerOptionModel { Id = 44, QuestionId = 15, Text = "För att kryptera lösenordet så det går att läsa tillbaka", IsCorrect = false },
                new AnswerOptionModel { Id = 45, QuestionId = 15, Text = "För att minska längden på lösenordet i databasen", IsCorrect = false },

                // Q16
                new AnswerOptionModel { Id = 46, QuestionId = 16, Text = "Hashning är envägs; kryptering är tvåvägs med nyckel", IsCorrect = true },
                new AnswerOptionModel { Id = 47, QuestionId = 16, Text = "Hashning ger alltid längre output än kryptering", IsCorrect = false },
                new AnswerOptionModel { Id = 48, QuestionId = 16, Text = "Hashning kräver alltid en privat nyckel", IsCorrect = false },

                // Q17
                new AnswerOptionModel { Id = 49, QuestionId = 17, Text = "Symmetrisk använder samma nyckel; asymmetrisk använder publika/privata nycklar", IsCorrect = true },
                new AnswerOptionModel { Id = 50, QuestionId = 17, Text = "Symmetrisk kräver certifikat; asymmetrisk gör inte det", IsCorrect = false },
                new AnswerOptionModel { Id = 51, QuestionId = 17, Text = "Asymmetrisk är alltid snabbare än symmetrisk", IsCorrect = false },

                // Q18
                new AnswerOptionModel { Id = 52, QuestionId = 18, Text = "AES", IsCorrect = true },
                new AnswerOptionModel { Id = 53, QuestionId = 18, Text = "RSA", IsCorrect = false },
                new AnswerOptionModel { Id = 54, QuestionId = 18, Text = "ECDSA", IsCorrect = false },

                // Q19
                new AnswerOptionModel { Id = 55, QuestionId = 19, Text = "För att asymmetrisk är långsammare: man byter nycklar asymmetriskt och krypterar data symmetriskt", IsCorrect = true },
                new AnswerOptionModel { Id = 56, QuestionId = 19, Text = "För att slippa nycklar helt", IsCorrect = false },
                new AnswerOptionModel { Id = 57, QuestionId = 19, Text = "För att hashning inte fungerar utan hybrid", IsCorrect = false },

                // Q20
                new AnswerOptionModel { Id = 58, QuestionId = 20, Text = "För att andra ska kunna kryptera till dig eller verifiera signaturer (beroende på användning)", IsCorrect = true },
                new AnswerOptionModel { Id = 59, QuestionId = 20, Text = "För att dekryptera data som du själv krypterat med privat nyckel", IsCorrect = false },
                new AnswerOptionModel { Id = 60, QuestionId = 20, Text = "För att generera slumpmässiga lösenord", IsCorrect = false },

                // Q21
                new AnswerOptionModel { Id = 61, QuestionId = 21, Text = "Avlyssning och manipulation i transit (MITM) mellan klient och server", IsCorrect = true },
                new AnswerOptionModel { Id = 62, QuestionId = 21, Text = "Att servern får virus från klienten", IsCorrect = false },
                new AnswerOptionModel { Id = 63, QuestionId = 21, Text = "Att din hårddisk blir krypterad av ransomware", IsCorrect = false },

                // Q22
                new AnswerOptionModel { Id = 64, QuestionId = 22, Text = "Ett dokument som binder en publik nyckel till en identitet, signerat av en CA", IsCorrect = true },
                new AnswerOptionModel { Id = 65, QuestionId = 22, Text = "En engångskod för MFA", IsCorrect = false },
                new AnswerOptionModel { Id = 66, QuestionId = 22, Text = "En brandväggsregel för port 443", IsCorrect = false },

                // Q23
                new AnswerOptionModel { Id = 67, QuestionId = 23, Text = "Att webbläsaren litar på certifikat som kan härledas till en betrodd rot-CA", IsCorrect = true },
                new AnswerOptionModel { Id = 68, QuestionId = 23, Text = "Att alla certifikat är självsignerade och lika säkra", IsCorrect = false },
                new AnswerOptionModel { Id = 69, QuestionId = 23, Text = "Att DNS automatiskt validerar certifikatens äkthet", IsCorrect = false },

                // Q24
                new AnswerOptionModel { Id = 70, QuestionId = 24, Text = "Webbläsaren varnar för ogiltigt/utgånget certifikat eller mismatch i domän", IsCorrect = true },
                new AnswerOptionModel { Id = 71, QuestionId = 24, Text = "Webbsidan laddar snabbt", IsCorrect = false },
                new AnswerOptionModel { Id = 72, QuestionId = 24, Text = "Låset syns alltid även på HTTP", IsCorrect = false },

                // Q25
                new AnswerOptionModel { Id = 73, QuestionId = 25, Text = "För att angriparen behöver mer än en faktor (t.ex. lösenord + kod/enhet)", IsCorrect = true },
                new AnswerOptionModel { Id = 74, QuestionId = 25, Text = "För att MFA gör lösenord onödiga och kan tas bort", IsCorrect = false },
                new AnswerOptionModel { Id = 75, QuestionId = 25, Text = "För att MFA hindrar alla typer av phishing automatiskt", IsCorrect = false },

                // Q26
                new AnswerOptionModel { Id = 76, QuestionId = 26, Text = "M4r!n#Kaktus_92Q", IsCorrect = true },
                new AnswerOptionModel { Id = 77, QuestionId = 26, Text = "Sommar2026", IsCorrect = false },
                new AnswerOptionModel { Id = 78, QuestionId = 26, Text = "password123", IsCorrect = false },

                // Q27
                new AnswerOptionModel { Id = 79, QuestionId = 27, Text = "Att angripare testar läckta användarnamn/lösenord mot andra tjänster automatiskt", IsCorrect = true },
                new AnswerOptionModel { Id = 80, QuestionId = 27, Text = "Att angripare gissar en PIN-kod på en telefon", IsCorrect = false },
                new AnswerOptionModel { Id = 81, QuestionId = 27, Text = "Att angripare krypterar filer och kräver lösen", IsCorrect = false },

                // Q28
                new AnswerOptionModel { Id = 82, QuestionId = 28, Text = "Använd en lösenordshanterare och unika långa lösenord per tjänst", IsCorrect = true },
                new AnswerOptionModel { Id = 83, QuestionId = 28, Text = "Återanvänd samma lösenord men byt ofta", IsCorrect = false },
                new AnswerOptionModel { Id = 84, QuestionId = 28, Text = "Skriv ner lösenord i en textfil på skrivbordet", IsCorrect = false },

                // Q29
                new AnswerOptionModel { Id = 85, QuestionId = 29, Text = "Role-Based Access Control (åtkomst styrs av roller)", IsCorrect = true },
                new AnswerOptionModel { Id = 86, QuestionId = 29, Text = "Rule-Based Authentication Control", IsCorrect = false },
                new AnswerOptionModel { Id = 87, QuestionId = 29, Text = "Resource-Based Account Control", IsCorrect = false },

                // Q30
                new AnswerOptionModel { Id = 88, QuestionId = 30, Text = "Ge bara de behörigheter som behövs för uppgiften – inte mer", IsCorrect = true },
                new AnswerOptionModel { Id = 89, QuestionId = 30, Text = "Ge alla samma behörighet för enkelhetens skull", IsCorrect = false },
                new AnswerOptionModel { Id = 90, QuestionId = 30, Text = "Ge alltid admin så problem löses snabbare", IsCorrect = false },

                // Q31
                new AnswerOptionModel { Id = 91, QuestionId = 31, Text = "RBAC styr via roller; ABAC styr via attribut (t.ex. avdelning, plats, tid)", IsCorrect = true },
                new AnswerOptionModel { Id = 92, QuestionId = 31, Text = "RBAC kräver alltid biometrisk inloggning; ABAC gör inte det", IsCorrect = false },
                new AnswerOptionModel { Id = 93, QuestionId = 31, Text = "ABAC fungerar bara i Windows och RBAC bara i Linux", IsCorrect = false },

                // Q32
                new AnswerOptionModel { Id = 94, QuestionId = 32, Text = "De har hög påverkan; komprometteras de kan angriparen få full kontroll", IsCorrect = true },
                new AnswerOptionModel { Id = 95, QuestionId = 32, Text = "De kan inte loggas, så de är mindre spårbara", IsCorrect = false },
                new AnswerOptionModel { Id = 96, QuestionId = 32, Text = "De har alltid kortare lösenord per standard", IsCorrect = false },

                // Q33
                new AnswerOptionModel { Id = 97, QuestionId = 33, Text = "Ett sätt att hålla reda på en inloggad användares tillstånd mellan HTTP-anrop", IsCorrect = true },
                new AnswerOptionModel { Id = 98, QuestionId = 33, Text = "En krypteringsalgoritm för lösenord", IsCorrect = false },
                new AnswerOptionModel { Id = 99, QuestionId = 33, Text = "En brandväggsregel för att blockera cookies", IsCorrect = false },

                // Q34
                new AnswerOptionModel { Id = 100, QuestionId = 34, Text = "Cookie är ofta lagring i klienten; token är en bärare av claims/åtkomst som kan lagras på olika sätt", IsCorrect = true },
                new AnswerOptionModel { Id = 101, QuestionId = 34, Text = "Cookie är alltid krypterad; token är alltid okrypterad", IsCorrect = false },
                new AnswerOptionModel { Id = 102, QuestionId = 34, Text = "Tokens kan bara användas i desktop-appar", IsCorrect = false },

                // Q35
                new AnswerOptionModel { Id = 103, QuestionId = 35, Text = "Single Sign-On", IsCorrect = true },
                new AnswerOptionModel { Id = 104, QuestionId = 35, Text = "Secure Sync Online", IsCorrect = false },
                new AnswerOptionModel { Id = 105, QuestionId = 35, Text = "System Session Object", IsCorrect = false },

                // Q36
                new AnswerOptionModel { Id = 106, QuestionId = 36, Text = "Stulen session kan användas längre (session hijacking) om den inte löper ut rimligt", IsCorrect = true },
                new AnswerOptionModel { Id = 107, QuestionId = 36, Text = "Det gör alltid webbplatsen långsammare", IsCorrect = false },
                new AnswerOptionModel { Id = 108, QuestionId = 36, Text = "Det gör att lösenord blir svagare automatiskt", IsCorrect = false }
            );

            // -------------------------
            // OBS: INTE aktivera ValueGeneratedNever() här.
            // Låt SQL Server ha IDENTITY och seed sker via migrations.
            // -------------------------
        }
    }
    }

