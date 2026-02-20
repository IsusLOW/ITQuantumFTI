using ContentService.Application.Mapping;
using ContentService.Application.Repositories;
using ContentService.Application.Services;
using ContentService.Config;
using ContentService.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Shared.Extensions;
using Shared.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = true);

// Add services to the container.
builder.Services.AddControllers();

// Add health check services
builder.Services.AddHealthChecks();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Swagger configuration
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();

    // Filter endpoints by controller name and assign to appropriate Swagger doc
    options.DocInclusionPredicate((version, desc) =>
    {
        var controllerName = desc.ActionDescriptor is ControllerActionDescriptor controllerDescriptor
            ? controllerDescriptor.ControllerName
            : "";

        if (version == "news-v1.0" && controllerName == "News") return true;
        if (version == "slider-v1.0" && controllerName == "Slider") return true;
        if (version == "gallery-v1.0" && controllerName == "Gallery") return true;

        return false;
    });
});

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Dependency Injection
builder.Services.AddScoped<INewsAppService, NewsAppService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ISliderAppService, SliderAppService>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IGalleryAppService, GalleryAppService>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddAutoMapper(typeof(NewsProfile), typeof(SliderProfile), typeof(GalleryProfile));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/news-v1.0/swagger.json", "NEWS API V1.0");
        options.SwaggerEndpoint("/swagger/slider-v1.0/swagger.json", "SLIDER API V1.0");
        options.SwaggerEndpoint("/swagger/gallery-v1.0/swagger.json", "GALLERY API V1.0");
    });
}

app.MapHealthChecks("/health");

app.Run();
