using FluentValidation;
using Microsoft.Extensions.Options;
using SliderService.Application.Mapping;
using SliderService.Application.Repositories;
using SliderService.Application.Services;
using SliderService.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Shared.Middleware;
using Shared.Extensions;
using SliderService.Config;
using SliderService.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = true);


// --- FluentValidation Registration ---
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
// -------------------------------------

// Add services to the container.
builder.Services.AddControllers();

// Add health check services
builder.Services.AddHealthChecks();

// 1. Configure API Versioning and Explorer
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

// 2. Configure Swagger using a factory to provide a custom title for this specific API.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();
                });

// Dependency Injection
builder.Services.AddScoped<ISliderAppService, SliderAppService>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddAutoMapper(typeof(SliderProfile));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseRouting();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    // 3. Update SwaggerUI to create an endpoint for each API version
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

// Map health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
