using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CyberQuizGrupp1.UI.Services
{
    public class UserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public string UserId { get; private set; } = "";
        public string Username { get; private set; } = "";
        public string Email { get; private set; } = "";
        public bool IsAuthenticated { get; private set; }

        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task LoadUserAsync()
        {
            //hämtar allt som har med inloggad användaren att göra
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            IsAuthenticated = user.Identity?.IsAuthenticated ?? false;
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            Username = user.FindFirst(ClaimTypes.Name)?.Value ?? "";
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "";
        }
    }
}
