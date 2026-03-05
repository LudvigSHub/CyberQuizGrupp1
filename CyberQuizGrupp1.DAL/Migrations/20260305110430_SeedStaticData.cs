using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CyberQuizGrupp1.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaticData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nätverkssäkerhet" },
                    { 2, "Kryptografi & Säker kommunikation" },
                    { 3, "Identitet, Åtkomst & Autentisering (IAM)" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "IsLocked", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, "Phishing & Social Engineering" },
                    { 2, 1, true, "Brandväggar & Segmentering" },
                    { 3, 1, true, "Wi-Fi & Trådlös säkerhet" },
                    { 4, 2, false, "Hashning & Integritet" },
                    { 5, 2, true, "Symmetrisk & Asymmetrisk kryptering" },
                    { 6, 2, true, "TLS/HTTPS & Certifikat" },
                    { 7, 3, false, "Lösenord & MFA" },
                    { 8, 3, true, "RBAC/ABAC & Principer" },
                    { 9, 3, true, "Sessions, Tokens & SSO" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "SubCategoryId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Vad är phishing?" },
                    { 2, 1, "Vilket är ett vanligt tecken på ett phishing-mejl?" },
                    { 3, 1, "Vad är social engineering?" },
                    { 4, 1, "Vad bör du göra om du klickat på en misstänkt länk på jobbet?" },
                    { 5, 2, "Vad är huvudsyftet med en brandvägg?" },
                    { 6, 2, "Vad innebär nätverkssegmentering?" },
                    { 7, 2, "Vilken regel är mest riskfylld i en brandvägg?" },
                    { 8, 2, "Vilken princip bör gälla för öppna portar ut mot internet?" },
                    { 9, 3, "Vilket är säkrast för ett hemnät: WPA3, WPA2 eller öppet Wi-Fi?" },
                    { 10, 3, "Varför är WPS ofta en säkerhetsrisk?" },
                    { 11, 3, "Vad är en 'evil twin'-attack?" },
                    { 12, 3, "Vilken åtgärd är bäst för gästnätverk?" },
                    { 13, 4, "Vad används en hashfunktion främst till?" },
                    { 14, 4, "Vilken egenskap är viktig för en säker hashfunktion?" },
                    { 15, 4, "Varför används 'salt' vid lagring av lösenord?" },
                    { 16, 4, "Varför räcker inte en hash som 'kryptering'?" },
                    { 17, 5, "Vad är skillnaden mellan symmetrisk och asymmetrisk kryptering?" },
                    { 18, 5, "Vilket är ett exempel på symmetrisk kryptering?" },
                    { 19, 5, "Varför används ofta hybridkryptografi?" },
                    { 20, 5, "Vad används en publik nyckel till?" },
                    { 21, 6, "Vad skyddar TLS (HTTPS) främst mot?" },
                    { 22, 6, "Vad är ett digitalt certifikat?" },
                    { 23, 6, "Vad betyder 'chain of trust' för certifikat?" },
                    { 24, 6, "Vilket är ett varningstecken i webbläsaren kopplat till TLS?" },
                    { 25, 7, "Varför är MFA säkrare än endast lösenord?" },
                    { 26, 7, "Vilket lösenord är starkast?" },
                    { 27, 7, "Vad är credential stuffing?" },
                    { 28, 7, "Vilken rekommendation är bäst för lösenordshantering?" },
                    { 29, 8, "Vad betyder RBAC?" },
                    { 30, 8, "Vad innebär principen om minsta privilegium (least privilege)?" },
                    { 31, 8, "Vad är skillnaden mellan RBAC och ABAC?" },
                    { 32, 8, "Varför är administratörskonton särskilt känsliga?" },
                    { 33, 9, "Vad är en session i webbsammanhang?" },
                    { 34, 9, "Vad är skillnaden mellan en cookie och en token?" },
                    { 35, 9, "Vad står SSO för?" },
                    { 36, 9, "Vad är en risk med för lång session-livslängd?" }
                });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, true, 1, "Ett försök att lura dig att lämna ut känslig information via falska meddelanden" },
                    { 2, false, 1, "En metod för att kryptera filer på hårddisken" },
                    { 3, false, 1, "Ett verktyg för att blockera nätverkstrafik" },
                    { 4, true, 2, "Avsändaradressen/domänen är misstänkt eller felstavad" },
                    { 5, false, 2, "Mejlet kommer alltid från en känd kontakt" },
                    { 6, false, 2, "Mejlet saknar alltid bilagor" },
                    { 7, true, 3, "Manipulation av människor för att få dem att göra något som gynnar angriparen" },
                    { 8, false, 3, "En portskanning av ett nätverk" },
                    { 9, false, 3, "En automatiserad lösenordsattack mot en server" },
                    { 10, true, 4, "Rapportera enligt rutin (IT/Helpdesk/SOC) och följ incidentprocessen" },
                    { 11, false, 4, "Skicka länken vidare så andra kan varna sig" },
                    { 12, false, 4, "Ignorera det och hoppas att inget händer" },
                    { 13, true, 5, "Att filtrera och kontrollera nätverkstrafik enligt regler" },
                    { 14, false, 5, "Att skapa nya lösenord åt användare" },
                    { 15, false, 5, "Att öka Wi-Fi-hastigheten" },
                    { 16, true, 6, "Att dela upp nätet i separata zoner för att begränsa spridning vid intrång" },
                    { 17, false, 6, "Att slå ihop alla nät till ett för enklare drift" },
                    { 18, false, 6, "Att kryptera all intern trafik automatiskt" },
                    { 19, true, 7, "Att tillåta all inkommande trafik från internet (0.0.0.0/0) till interna system" },
                    { 20, false, 7, "Att blockera en specifik port som inte används" },
                    { 21, false, 7, "Att logga nekad trafik för felsökning" },
                    { 22, true, 8, "Exponera bara det som behövs (minimera attackytan) och stäng resten" },
                    { 23, false, 8, "Öppna alla standardportar så tjänster fungerar direkt" },
                    { 24, false, 8, "Öppna portar temporärt utan loggning" },
                    { 25, true, 9, "WPA3 är säkrast (därefter WPA2); öppet Wi-Fi är minst säkert" },
                    { 26, false, 9, "Öppet Wi-Fi är säkrast eftersom det inte har lösenord" },
                    { 27, false, 9, "WPA2 är alltid osäkert och ska aldrig användas" },
                    { 28, true, 10, "WPS kan vara sårbart (t.ex. PIN-baserade attacker) och gör intrång enklare" },
                    { 29, false, 10, "WPS gör nätet snabbare men mindre stabilt" },
                    { 30, false, 10, "WPS krävs för att kunna använda WPA3" },
                    { 31, true, 11, "Ett falskt Wi-Fi-nät som imiterar ett legitimt för att få dig att ansluta" },
                    { 32, false, 11, "En attack där man gissar routerns admin-lösenord via Bluetooth" },
                    { 33, false, 11, "En teknik för att öka räckvidden på en router" },
                    { 34, true, 12, "Skapa ett separat gästnät (separat VLAN/isolering) med begränsad åtkomst" },
                    { 35, false, 12, "Låt gäster använda samma SSID och lösenord som interna nätet" },
                    { 36, false, 12, "Stäng av lösenord helt för att undvika support" },
                    { 37, true, 13, "Att skapa ett fingeravtryck (digest) för integritet/identifiering" },
                    { 38, false, 13, "Att göra data oläsbar och sedan läsbar igen med nyckel" },
                    { 39, false, 13, "Att komprimera data för att spara utrymme" },
                    { 40, true, 14, "Motstånd mot kollisioner (svårt att hitta två olika inputs med samma hash)" },
                    { 41, false, 14, "Att den alltid producerar olika längd på output" },
                    { 42, false, 14, "Att den går att reversera snabbt" },
                    { 43, true, 15, "För att göra rainbow tables och förberäknade attacker mycket svårare" },
                    { 44, false, 15, "För att kryptera lösenordet så det går att läsa tillbaka" },
                    { 45, false, 15, "För att minska längden på lösenordet i databasen" },
                    { 46, true, 16, "Hashning är envägs; kryptering är tvåvägs med nyckel" },
                    { 47, false, 16, "Hashning ger alltid längre output än kryptering" },
                    { 48, false, 16, "Hashning kräver alltid en privat nyckel" },
                    { 49, true, 17, "Symmetrisk använder samma nyckel; asymmetrisk använder publika/privata nycklar" },
                    { 50, false, 17, "Symmetrisk kräver certifikat; asymmetrisk gör inte det" },
                    { 51, false, 17, "Asymmetrisk är alltid snabbare än symmetrisk" },
                    { 52, true, 18, "AES" },
                    { 53, false, 18, "RSA" },
                    { 54, false, 18, "ECDSA" },
                    { 55, true, 19, "För att asymmetrisk är långsammare: man byter nycklar asymmetriskt och krypterar data symmetriskt" },
                    { 56, false, 19, "För att slippa nycklar helt" },
                    { 57, false, 19, "För att hashning inte fungerar utan hybrid" },
                    { 58, true, 20, "För att andra ska kunna kryptera till dig eller verifiera signaturer (beroende på användning)" },
                    { 59, false, 20, "För att dekryptera data som du själv krypterat med privat nyckel" },
                    { 60, false, 20, "För att generera slumpmässiga lösenord" },
                    { 61, true, 21, "Avlyssning och manipulation i transit (MITM) mellan klient och server" },
                    { 62, false, 21, "Att servern får virus från klienten" },
                    { 63, false, 21, "Att din hårddisk blir krypterad av ransomware" },
                    { 64, true, 22, "Ett dokument som binder en publik nyckel till en identitet, signerat av en CA" },
                    { 65, false, 22, "En engångskod för MFA" },
                    { 66, false, 22, "En brandväggsregel för port 443" },
                    { 67, true, 23, "Att webbläsaren litar på certifikat som kan härledas till en betrodd rot-CA" },
                    { 68, false, 23, "Att alla certifikat är självsignerade och lika säkra" },
                    { 69, false, 23, "Att DNS automatiskt validerar certifikatens äkthet" },
                    { 70, true, 24, "Webbläsaren varnar för ogiltigt/utgånget certifikat eller mismatch i domän" },
                    { 71, false, 24, "Webbsidan laddar snabbt" },
                    { 72, false, 24, "Låset syns alltid även på HTTP" },
                    { 73, true, 25, "För att angriparen behöver mer än en faktor (t.ex. lösenord + kod/enhet)" },
                    { 74, false, 25, "För att MFA gör lösenord onödiga och kan tas bort" },
                    { 75, false, 25, "För att MFA hindrar alla typer av phishing automatiskt" },
                    { 76, true, 26, "M4r!n#Kaktus_92Q" },
                    { 77, false, 26, "Sommar2026" },
                    { 78, false, 26, "password123" },
                    { 79, true, 27, "Att angripare testar läckta användarnamn/lösenord mot andra tjänster automatiskt" },
                    { 80, false, 27, "Att angripare gissar en PIN-kod på en telefon" },
                    { 81, false, 27, "Att angripare krypterar filer och kräver lösen" },
                    { 82, true, 28, "Använd en lösenordshanterare och unika långa lösenord per tjänst" },
                    { 83, false, 28, "Återanvänd samma lösenord men byt ofta" },
                    { 84, false, 28, "Skriv ner lösenord i en textfil på skrivbordet" },
                    { 85, true, 29, "Role-Based Access Control (åtkomst styrs av roller)" },
                    { 86, false, 29, "Rule-Based Authentication Control" },
                    { 87, false, 29, "Resource-Based Account Control" },
                    { 88, true, 30, "Ge bara de behörigheter som behövs för uppgiften – inte mer" },
                    { 89, false, 30, "Ge alla samma behörighet för enkelhetens skull" },
                    { 90, false, 30, "Ge alltid admin så problem löses snabbare" },
                    { 91, true, 31, "RBAC styr via roller; ABAC styr via attribut (t.ex. avdelning, plats, tid)" },
                    { 92, false, 31, "RBAC kräver alltid biometrisk inloggning; ABAC gör inte det" },
                    { 93, false, 31, "ABAC fungerar bara i Windows och RBAC bara i Linux" },
                    { 94, true, 32, "De har hög påverkan; komprometteras de kan angriparen få full kontroll" },
                    { 95, false, 32, "De kan inte loggas, så de är mindre spårbara" },
                    { 96, false, 32, "De har alltid kortare lösenord per standard" },
                    { 97, true, 33, "Ett sätt att hålla reda på en inloggad användares tillstånd mellan HTTP-anrop" },
                    { 98, false, 33, "En krypteringsalgoritm för lösenord" },
                    { 99, false, 33, "En brandväggsregel för att blockera cookies" },
                    { 100, true, 34, "Cookie är ofta lagring i klienten; token är en bärare av claims/åtkomst som kan lagras på olika sätt" },
                    { 101, false, 34, "Cookie är alltid krypterad; token är alltid okrypterad" },
                    { 102, false, 34, "Tokens kan bara användas i desktop-appar" },
                    { 103, true, 35, "Single Sign-On" },
                    { 104, false, 35, "Secure Sync Online" },
                    { 105, false, 35, "System Session Object" },
                    { 106, true, 36, "Stulen session kan användas längre (session hijacking) om den inte löper ut rimligt" },
                    { 107, false, 36, "Det gör alltid webbplatsen långsammare" },
                    { 108, false, 36, "Det gör att lösenord blir svagare automatiskt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
