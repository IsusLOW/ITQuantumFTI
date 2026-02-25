using MMLib.Ocelot.Provider.AppConfiguration;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
    .AddOcelotWithSwaggerSupport(o =>
    {
        o.Folder = "Configuration";
    })
    .AddEnvironmentVariables();

builder.Services.AddOcelot().AddAppConfiguration();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ITQ API Gateway",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ViteDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("ViteDev");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseSwagger();
app.UseStaticFiles();
app.UseEndpoints(_ => { });

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
});   
app.MapControllers();               

app.UseSwaggerForOcelotUI(opt =>
{
    opt.DownstreamSwaggerHeaders = new[]
    {
        new KeyValuePair<string, string>("Key", "Value"),
        new KeyValuePair<string, string>("Key2", "Value2"),
    };
});

await app.UseOcelot();              

app.Run();