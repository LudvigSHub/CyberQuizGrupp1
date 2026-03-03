using CyberQuizGrupp1.DAL.Identity;
using CyberQuizGrupp1.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(
        AuthService authService,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _authService = authService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return Redirect("login");
        }
       
        await _signInManager.SignInAsync(user, isPersistent: false);
        return Redirect("/categories");

    }
}