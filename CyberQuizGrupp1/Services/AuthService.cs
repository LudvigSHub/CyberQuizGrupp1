using CyberQuizGrupp1.DAL.Identity;
using CyberQuizGrupp1.SHARED.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CyberQuizGrupp1.UI.Services
{
    public class AuthService
    {
        //service som innehåller allt med authentication, login, register etc. för att separera så mycket som möjligt och göra det lättare att ändra/felsöka
        //använder signinamanger och usermanager för att hantera inloggning/registrereing med identity
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> LoginAsync(LoginDTO dto)
        {
            //inbygda metoden FindByNameAsync (i usermanager) letar genom table för en user med det username / kolla ifall en användare med det usernamnet finns
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null) // om användare inte hittas returnera false 
            {
                return false;
            }

            //inbygda metoden CheckPasswordSignInAsync (i signinmanager) kontrollerar ifall password matchar den användare som hittades
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false); // false för att inte låsa konto vid failade attempts

            if (result.Succeeded) //om lösenord och användarnamn matchar gör detta:
            {
                return result.Succeeded; //FRÅGA: vad gör vi med result? hur displayar vi result?
                                         //SVAR: return true / false tillbaka till ui (login page) och ui bestämmer vad som displayas vid true (if (success) och vid false (else ...)
            }

            return false; //om lösenord var fel
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            //skapar en ny user (instans av applicationuser) endast med username från dto
            //sparar dock inte password här efetrsom det  hade sparats som ren text och innebär en securisty risk, vi aldrig sparadet råa lösenordet i user objectet
            var user = new ApplicationUser 
            { 
                UserName = dto.Username,
                Email = dto.Email
            };
            //skcikar password separat till usermanager som hanterar hashning och säker lagring av lösenordet i databasen
            //skickar med både användarnamn(user) och password som en egen parameter för att skapa en ny användare i databasen
            var result = await _userManager.CreateAsync(user, dto.Password);

            //DEBUG:
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Identity error: {error.Code} - {error.Description}");
                }
            }

            return result.Succeeded; //FRÅGA: vad gör vi med result? hur displayar vi result?
            //SVAR: returnerar result true / false tillbaka till pagen (ui register page) och ui bestämmer vad som displayas vid true (if (success) och vid false (else ...)
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ChangeEmailAsync(string userId, string newEmail)
        {
            //kontrollera att användaren vi fick som parameter faktiskt finns, spara det i user
            var user = await _userManager.FindByIdAsync(userId); //leta upp användaren i databasen
            if(user is null) //om användare inte finns, avbryt
            { 
                return false; 
            }

            //Identity kräver en säkerhetsoken av säkerhetsskäl för att byta email, kollar att ändringen är tillåten så inte vem som helst ska kunna ändra andras email tex via apiet. tokenen är bevis på att ändringen skapades i servern
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token); //byter emailen i databasen
            return result.Succeeded;   
        }
        public async Task<string> GetEmailAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); //leta upp användaren i databasen via userId
            return user?.Email ?? ""; //user?.Email - om user inte är null hämta email, annars (??) returnera null
        }

        public async Task<bool> ChangePasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId); //leta upp användaren
            if (user is null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user); //generera säkerhetstoken, bevis på att ändringen skapades i servern och inte av någon utanför
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword); //ersätter det gamla lösenordet med det nya. behöver user för att bestämma vems lösenord som ska bytas, token - bevis på ändringen är äkta, newpassword - det nya lösneordet sparas

            // DEBUG:
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Identity error: {error.Code} - {error.Description}");
            }

            return result.Succeeded;
        }
    }
}
