using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Seed endast om tabellerna är tomma 
        if (!await context.Categories.AnyAsync())


        {
            context.Categories.AddRange(
                new CategoryModel { Id = 1, Name = "Nätverkssäkerhet" },
                new CategoryModel { Id = 2, Name = "Kryptografi & Säker kommunikation" },
                new CategoryModel { Id = 3, Name = "Identitet, Åtkomst & Autentisering (IAM)" }
            );
            await context.SaveChangesAsync();
        }

        if (!await context.SubCategories.AnyAsync())
        {
            context.SubCategories.AddRange(
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
            await context.SaveChangesAsync();
        }

        if (!await context.Questions.AnyAsync())
        {
            int qId = 1;
            var questions = new List<QuestionModel>();

            void AddQ(int subCategoryId, params string[] texts)
            {
                foreach (var t in texts)
                {
                    questions.Add(new QuestionModel
                    {
                        Id = qId++,
                        SubCategoryId = subCategoryId,
                        Text = t
                    });
                }
            }

            // SubCategory 1 (Q1–Q4)
            AddQ(1,
                "Vad är phishing?",
                "Vilket är ett vanligt tecken på ett phishing-mejl?",
                "Vad är social engineering?",
                "Vad bör du göra om du klickat på en misstänkt länk på jobbet?");

            // SubCategory 2 (Q5–Q8)
            AddQ(2,
                "Vad är huvudsyftet med en brandvägg?",
                "Vad innebär nätverkssegmentering?",
                "Vilken regel är mest riskfylld i en brandvägg?",
                "Vilken princip bör gälla för öppna portar ut mot internet?");

            // SubCategory 3 (Q9–Q12)
            AddQ(3,
                "Vilket är säkrast för ett hemnät: WPA3, WPA2 eller öppet Wi-Fi?",
                "Varför är WPS ofta en säkerhetsrisk?",
                "Vad är en 'evil twin'-attack?",
                "Vilken åtgärd är bäst för gästnätverk?");

            // SubCategory 4 (Q13–Q16)
            AddQ(4,
                "Vad används en hashfunktion främst till?",
                "Vilken egenskap är viktig för en säker hashfunktion?",
                "Varför används 'salt' vid lagring av lösenord?",
                "Varför räcker inte en hash som 'kryptering'?");

            // SubCategory 5 (Q17–Q20)
            AddQ(5,
                "Vad är skillnaden mellan symmetrisk och asymmetrisk kryptering?",
                "Vilket är ett exempel på symmetrisk kryptering?",
                "Varför används ofta hybridkryptografi?",
                "Vad används en publik nyckel till?");

            // SubCategory 6 (Q21–Q24)
            AddQ(6,
                "Vad skyddar TLS (HTTPS) främst mot?",
                "Vad är ett digitalt certifikat?",
                "Vad betyder 'chain of trust' för certifikat?",
                "Vilket är ett varningstecken i webbläsaren kopplat till TLS?");

            // SubCategory 7 (Q25–Q28)
            AddQ(7,
                "Varför är MFA säkrare än endast lösenord?",
                "Vilket lösenord är starkast?",
                "Vad är credential stuffing?",
                "Vilken rekommendation är bäst för lösenordshantering?");

            // SubCategory 8 (Q29–Q32)
            AddQ(8,
                "Vad betyder RBAC?",
                "Vad innebär principen om minsta privilegium (least privilege)?",
                "Vad är skillnaden mellan RBAC och ABAC?",
                "Varför är administratörskonton särskilt känsliga?");

            // SubCategory 9 (Q33–Q36)
            AddQ(9,
                "Vad är en session i webbsammanhang?",
                "Vad är skillnaden mellan en cookie och en token?",
                "Vad står SSO för?",
                "Vad är en risk med för lång session-livslängd?");

            context.Questions.AddRange(questions);
            await context.SaveChangesAsync();
        }

        if (!await context.AnswerOptions.AnyAsync())
        {
            int aId = 1;
            var answers = new List<AnswerOptionModel>(108);

            void AddA(int questionId, string correct, string wrong1, string wrong2)
            {
                answers.Add(new AnswerOptionModel { Id = aId++, QuestionId = questionId, Text = correct, IsCorrect = true });
                answers.Add(new AnswerOptionModel { Id = aId++, QuestionId = questionId, Text = wrong1, IsCorrect = false });
                answers.Add(new AnswerOptionModel { Id = aId++, QuestionId = questionId, Text = wrong2, IsCorrect = false });
            }

            // ===== SubCategory 1: Phishing (Q1–Q4) =====
            AddA(1,
                "Ett försök att lura dig att lämna ut känslig information via falska meddelanden",
                "En metod för att kryptera filer på hårddisken",
                "Ett verktyg för att blockera nätverkstrafik");

            AddA(2,
                "Avsändaradressen/domänen är misstänkt eller felstavad",
                "Mejlet kommer alltid från en känd kontakt",
                "Mejlet saknar alltid bilagor");

            AddA(3,
                "Manipulation av människor för att få dem att göra något som gynnar angriparen",
                "En portskanning av ett nätverk",
                "En automatiserad lösenordsattack mot en server");

            AddA(4,
                "Rapportera enligt rutin (IT/Helpdesk/SOC) och följ incidentprocessen",
                "Skicka länken vidare så andra kan varna sig",
                "Ignorera det och hoppas att inget händer");

            // ===== SubCategory 2: Brandväggar (Q5–Q8) =====
            AddA(5,
                "Att filtrera och kontrollera nätverkstrafik enligt regler",
                "Att skapa nya lösenord åt användare",
                "Att öka Wi-Fi-hastigheten");

            AddA(6,
                "Att dela upp nätet i separata zoner för att begränsa spridning vid intrång",
                "Att slå ihop alla nät till ett för enklare drift",
                "Att kryptera all intern trafik automatiskt");

            AddA(7,
                "Att tillåta all inkommande trafik från internet (0.0.0.0/0) till interna system",
                "Att blockera en specifik port som inte används",
                "Att logga nekad trafik för felsökning");

            AddA(8,
                "Exponera bara det som behövs (minimera attackytan) och stäng resten",
                "Öppna alla standardportar så tjänster fungerar direkt",
                "Öppna portar temporärt utan loggning");

            // ===== SubCategory 3: Wi-Fi (Q9–Q12) =====
            AddA(9,
                "WPA3 är säkrast (därefter WPA2); öppet Wi-Fi är minst säkert",
                "Öppet Wi-Fi är säkrast eftersom det inte har lösenord",
                "WPA2 är alltid osäkert och ska aldrig användas");

            AddA(10,
                "WPS kan vara sårbart (t.ex. PIN-baserade attacker) och gör intrång enklare",
                "WPS gör nätet snabbare men mindre stabilt",
                "WPS krävs för att kunna använda WPA3");

            AddA(11,
                "Ett falskt Wi-Fi-nät som imiterar ett legitimt för att få dig att ansluta",
                "En attack där man gissar routerns admin-lösenord via Bluetooth",
                "En teknik för att öka räckvidden på en router");

            AddA(12,
                "Skapa ett separat gästnät (separat VLAN/isolering) med begränsad åtkomst",
                "Låt gäster använda samma SSID och lösenord som interna nätet",
                "Stäng av lösenord helt för att undvika support");

            // ===== SubCategory 4: Hashning (Q13–Q16) =====
            AddA(13,
                "Att skapa ett fingeravtryck (digest) för integritet/identifiering",
                "Att göra data oläsbar och sedan läsbar igen med nyckel",
                "Att komprimera data för att spara utrymme");

            AddA(14,
                "Motstånd mot kollisioner (svårt att hitta två olika inputs med samma hash)",
                "Att den alltid producerar olika längd på output",
                "Att den går att reversera snabbt");

            AddA(15,
                "För att göra rainbow tables och förberäknade attacker mycket svårare",
                "För att kryptera lösenordet så det går att läsa tillbaka",
                "För att minska längden på lösenordet i databasen");

            AddA(16,
                "Hashning är envägs; kryptering är tvåvägs med nyckel",
                "Hashning ger alltid längre output än kryptering",
                "Hashning kräver alltid en privat nyckel");

            // ===== SubCategory 5: Sym/Asym (Q17–Q20) =====
            AddA(17,
                "Symmetrisk använder samma nyckel; asymmetrisk använder publika/privata nycklar",
                "Symmetrisk kräver certifikat; asymmetrisk gör inte det",
                "Asymmetrisk är alltid snabbare än symmetrisk");

            AddA(18,
                "AES",
                "RSA",
                "ECDSA");

            AddA(19,
                "För att asymmetrisk är långsammare: man byter nycklar asymmetriskt och krypterar data symmetriskt",
                "För att slippa nycklar helt",
                "För att hashning inte fungerar utan hybrid");

            AddA(20,
                "För att andra ska kunna kryptera till dig eller verifiera signaturer (beroende på användning)",
                "För att dekryptera data som du själv krypterat med privat nyckel",
                "För att generera slumpmässiga lösenord");

            // ===== SubCategory 6: TLS/Cert (Q21–Q24) =====
            AddA(21,
                "Avlyssning och manipulation i transit (MITM) mellan klient och server",
                "Att servern får virus från klienten",
                "Att din hårddisk blir krypterad av ransomware");

            AddA(22,
                "Ett dokument som binder en publik nyckel till en identitet, signerat av en CA",
                "En engångskod för MFA",
                "En brandväggsregel för port 443");

            AddA(23,
                "Att webbläsaren litar på certifikat som kan härledas till en betrodd rot-CA",
                "Att alla certifikat är självsignerade och lika säkra",
                "Att DNS automatiskt validerar certifikatens äkthet");

            AddA(24,
                "Webbläsaren varnar för ogiltigt/utgånget certifikat eller mismatch i domän",
                "Webbsidan laddar snabbt",
                "Låset syns alltid även på HTTP");

            // ===== SubCategory 7: Lösenord/MFA (Q25–Q28) =====
            AddA(25,
                "För att angriparen behöver mer än en faktor (t.ex. lösenord + kod/enhet)",
                "För att MFA gör lösenord onödiga och kan tas bort",
                "För att MFA hindrar alla typer av phishing automatiskt");

            AddA(26,
                "M4r!n#Kaktus_92Q",
                "Sommar2026",
                "password123");

            AddA(27,
                "Att angripare testar läckta användarnamn/lösenord mot andra tjänster automatiskt",
                "Att angripare gissar en PIN-kod på en telefon",
                "Att angripare krypterar filer och kräver lösen");

            AddA(28,
                "Använd en lösenordshanterare och unika långa lösenord per tjänst",
                "Återanvänd samma lösenord men byt ofta",
                "Skriv ner lösenord i en textfil på skrivbordet");

            // ===== SubCategory 8: RBAC/ABAC (Q29–Q32) =====
            AddA(29,
                "Role-Based Access Control (åtkomst styrs av roller)",
                "Rule-Based Authentication Control",
                "Resource-Based Account Control");

            AddA(30,
                "Ge bara de behörigheter som behövs för uppgiften – inte mer",
                "Ge alla samma behörighet för enkelhetens skull",
                "Ge alltid admin så problem löses snabbare");

            AddA(31,
                "RBAC styr via roller; ABAC styr via attribut (t.ex. avdelning, plats, tid)",
                "RBAC kräver alltid biometrisk inloggning; ABAC gör inte det",
                "ABAC fungerar bara i Windows och RBAC bara i Linux");

            AddA(32,
                "De har hög påverkan; komprometteras de kan angriparen få full kontroll",
                "De kan inte loggas, så de är mindre spårbara",
                "De har alltid kortare lösenord per standard");

            // ===== SubCategory 9: Sessions/Tokens/SSO (Q33–Q36) =====
            AddA(33,
                "Ett sätt att hålla reda på en inloggad användares tillstånd mellan HTTP-anrop",
                "En krypteringsalgoritm för lösenord",
                "En brandväggsregel för att blockera cookies");

            AddA(34,
                "Cookie är ofta lagring i klienten; token är en bärare av claims/åtkomst som kan lagras på olika sätt",
                "Cookie är alltid krypterad; token är alltid okrypterad",
                "Tokens kan bara användas i desktop-appar");

            AddA(35,
                "Single Sign-On",
                "Secure Sync Online",
                "System Session Object");

            AddA(36,
                "Stulen session kan användas längre (session hijacking) om den inte löper ut rimligt",
                "Det gör alltid webbplatsen långsammare",
                "Det gör att lösenord blir svagare automatiskt");

            context.AnswerOptions.AddRange(answers);
            await context.SaveChangesAsync();
        }
    }
}
