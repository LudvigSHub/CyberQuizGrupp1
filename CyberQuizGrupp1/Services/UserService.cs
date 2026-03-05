using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CyberQuizGrupp1.UI.Services
{
    public class UserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }
        //metod som hämtar inloggad användare via authenticationstateprovider, hämtar inloggad för attt sen kunna visa info som gäller just den användaren
        //skapar den här i så jag kan hämta den på andra ställen ist för att behöva skriva om den på flera pages
        public async Task<string> GetUserIdAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        }
    }
}
