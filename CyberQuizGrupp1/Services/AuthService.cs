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
            var user = new ApplicationUser { UserName = dto.Username };
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
    }
}
