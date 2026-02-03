using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using MMLib.SwaggerForOcelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Явно включаем маршрутизацию, чтобы эндпоинты были определены до Ocelot
app.UseRouting();

// Используем современный синтаксис для регистрации эндпоинтов, что уберет предупреждение
app.MapHealthChecks("/health");
app.MapControllers();

// Добавляем корневой эндпоинт, который будет отвечать на запросы к '/'
app.MapGet("/", () => "Public Gateway is running");

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

// Ocelot используется в самом конце для всех остальных маршрутов
await app.UseOcelot();

app.Run();
