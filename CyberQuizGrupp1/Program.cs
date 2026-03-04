
using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Identity;
using CyberQuizGrupp1.UI.Components;
using CyberQuizGrupp1.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//LAGT TILL DETTA FÖR ATT ANVÄNDA IDENTITY, INSTALLERADE IDENTITY ENTITYFRAMEWORK OCH SQL SERVER
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//added:
builder.Services.AddScoped<UserService>();

//added:
builder.Services.AddScoped<AuthService>();
//added:
builder.Services.AddHttpClient<CategoryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7231/");
});
//added:
builder.Services.AddHttpClient<SubCategoryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7231/");
});
//added:
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    //ÄNDRA KRAV FÖR LÖSENORD:
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6; //blir minst 7 tecken (börjar på 0)
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
//added för att hantera cookies:
builder.Services.AddHttpContextAccessor();
//added:
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
//ADDED:
app.UseAuthentication();
//ADDED:
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();
app.Run();
