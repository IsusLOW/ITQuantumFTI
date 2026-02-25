using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using MentorService.Config;
using Shared.Extensions;
using FluentValidation;
using MentorService.Application.Mapping;
using MentorService.Application.Repositories;
using MentorService.Application.Services;
using MentorService.Infrastructure.Repositories;
using Shared.Middleware;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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

// swagger configuration
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(
    options =>
    {
        // add a custom operation filter which sets default values
        options.OperationFilter<SwaggerDefaultValues>();

        // integrate xml comments
    });

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Dependency Injection
builder.Services.AddScoped<IMentorAppService, MentorAppService>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddAutoMapper(typeof(MentorProfile));

var app = builder.Build();

// custom middleware 
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.MapHealthChecks("/health");


app.Run();
