using FluentValidation;
using MentorService.Application.Mapping;
using MentorService.Application.Repositories;
using MentorService.Application.Services;
using MentorService.Infrastructure.Repositories;
using Shared.Middleware;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Shared.Config;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = true);
builder.Services.AddControllers();

// add health check
builder.Services.AddHealthChecks();

// add api versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Dependency Injection
builder.Services.AddScoped<IMentorAppService, MentorAppService>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddAutoMapper(typeof(MentorProfile));

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

if (string.IsNullOrEmpty(jwtSettings.SecretKey) || jwtSettings.SecretKey.Length < 32)
{
    throw new InvalidOperationException("JWT SecretKey must be at least 32 characters!");
}

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateIssuer = true,
        ValidIssuer = jwtSettings?.Issuer ?? "ITQuantum",
        ValidateAudience = true,
        ValidAudience = jwtSettings?.Audience ?? "ITQuantumUsers",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// custom middleware 
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
