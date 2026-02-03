using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using SliderService.Application.Services;
using SliderService.Application.Repositories;
using SliderService.Infrastructure.Repositories; 
using Shared.Middleware;
using SliderService.Application.Mapping;
using Asp.Versioning;
using Shared.Extensions; // Correct using for shared extensions
using Asp.Versioning.ApiExplorer; // Required for IApiVersionDescription_provider
using Swashbuckle.AspNetCore.SwaggerGen; // Required for IConfigureOptions<SwaggerGenOptions>

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add health check services
builder.Services.AddHealthChecks();

// 1. Configure API Versioning and Explorer
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// 2. Configure Swagger using a factory to provide a custom title for this specific API.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(provider =>
{
    var apiVersionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();
    return new ConfigureSwaggerOptions(apiVersionDescriptionProvider, "Slider API");
});

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();
});


// Dependency Injection
builder.Services.AddScoped<ISliderService, SliderService.Application.Services.SliderService>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>(); 
builder.Services.AddAutoMapper(typeof(SliderProfile));

var app = builder.Build();

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
        options.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

// app.UseHttpsRedirection();

app.UseAuthorization();

// Map health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
