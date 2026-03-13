using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Implementations;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.BLL.Services;
using CyberQuizGrupp1.BLL.Options;


var builder = WebApplication.CreateBuilder(args);

//lägg till dbcontext med sql server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//registrera repositories (dependency injection)
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IUserResultRepository, UserResultRepository>(); 

//registrera services (business logic)
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IProgressService, ProgressService>();

//registrera ollama options från appsettings
builder.Services.Configure<OllamaOptions>(builder.Configuration.GetSection("Ollama"));
//registrera coaching service (business logic)
builder.Services.AddScoped<ICoachingService, CoachingService>();
//registrera ollama ai-klient med httpclient
builder.Services.AddHttpClient<IAiCoachClient, OllamaCoachClient>();
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
        policy.WithOrigins("https://localhost:7142", "http://localhost:5110") //justera portar efter behov
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

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