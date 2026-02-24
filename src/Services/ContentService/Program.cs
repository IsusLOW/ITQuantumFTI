using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using ContentService.Config;
using Shared.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using FluentValidation;
using ContentService.Application.Mapping;
using ContentService.Application.Repositories;
using ContentService.Application.Services;
using ContentService.Infrastructure.Repositories;
using Shared.Middleware;


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

// custom middleware 
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/news-v1.0/swagger.json", "News API V1.0");
        options.SwaggerEndpoint("/swagger/slider-v1.0/swagger.json", "Slider API V1.0");
        options.SwaggerEndpoint("/swagger/gallery-v1.0/swagger.json", "Gallery API V1.0");
    });
}

app.MapHealthChecks("/health");

app.Run();
