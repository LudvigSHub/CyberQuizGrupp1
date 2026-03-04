using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Implementations;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

//lägg till dbcontext med sql server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

////konfigurera identity för autentisering
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    //lösenordskrav som går att justera för enklare testning!
//    options.Password.RequireDigit = true; //måste innehålla minst en siffra
//    options.Password.RequiredLength = 8; //minsta längd på lösenord är 8 tecken
//    options.Password.RequireNonAlphanumeric = false; //kräver inte specialtecken. justera om vi behöver.
//    options.Password.RequireUppercase = true; //måste innehålla minst en versal (ex. A)
//    options.Password.RequireLowercase = true; // måste innehålla minst en gemen (ex. a)
//})
//.AddEntityFrameworkStores<AppDbContext>()
//.AddDefaultTokenProviders();


//registrera repositories (dependency injection)
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
//builder.Services.AddScoped<IQuestionRepository, QuestionRepository>(); //lägg till senare
//builder.Services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>(); //lägg till senare
//builder.Services.AddScoped<IUserResultRepository, UserResultRepository>(); //lägg till senare

//registrera services (business logic)
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>(); //röd markering på grund av att BLL inte implementerat ISubCategoryService och SubCategoryService än?
//builder.Services.AddScoped<IQuestionService, QuestionService>(); //lägg till senare
//builder.Services.AddScoped<IProgressionService, ProgressionService>(); //lägg till senare

//lägg till controllers
builder.Services.AddControllers();

//lägg till swagger för api-dokumentation och testning
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//lägg till cors för blazor-kommunikation
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7231", "http://localhost:5037") //justera portar efter behov
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

//seeda databasen vid start (lägg till senare när DatabaseSeeder är klar)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DatabaseSeeder.SeedAsync(context);

    //var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    //await DataBaseSeeder.SeedAsync(context);
}

//konfigurera http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseAuthentication(); //viktigt för identity
app.UseAuthorization();
app.MapControllers();

app.Run();