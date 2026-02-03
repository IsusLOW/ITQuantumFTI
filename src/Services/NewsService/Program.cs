using FluentValidation;
using Microsoft.Extensions.Options;
using NewsService.Application.Mapping;
using NewsService.Application.Repositories;
using NewsService.Application.Services;
using NewsService.Infrastructure.Repositories;
using Shared.Extensions;
using Shared.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NewsService.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = true);
builder.Services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });

builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                });

// --- FluentValidation Registration ---
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
// -------------------------------------

builder.Services.AddControllers();
builder.Services.AddHealthChecks();


builder.Services.AddScoped<INewsAppService, NewsAppService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddAutoMapper(typeof(NewsProfile));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
