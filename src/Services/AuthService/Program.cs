using System.Text;
using AuthService.Application.Mapping;
using AuthService.Application.Repositories;
using AuthService.Application.Services;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Config;
using Shared.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = true);
builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
    });

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//Services
builder.Services.AddScoped<IAuthAppService, AuthAppService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//Mapper
builder.Services.AddAutoMapper(typeof(UserProfile));

//JWT config
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

string secretKey = jwtSettings?.SecretKey ?? string.Empty;

if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
{
    if (builder.Environment.IsDevelopment())
    {
        // В разработке используем заглушку, если забыли прописать в appsettings.Development.json
        secretKey = "DefaultDevelopmentKeyThatIsAtLeast32Chars!";
    }
    else
    {
        // В Prod-среде это критическая ошибка безопасности
        throw new InvalidOperationException("JWT SecretKey is missing or too short (min 32 chars) in Production!");
    }
}

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer ?? "ITQuantum",
        ValidAudience = jwtSettings?.Audience ?? "ITQuantumUsers",
        IssuerSigningKey = key,
        ClockSkew = TimeSpan.Zero // Убираем задержку в 5 минут (токен протухает секунда в секунду)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
