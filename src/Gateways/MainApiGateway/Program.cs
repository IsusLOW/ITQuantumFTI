using MMLib.Ocelot.Provider.AppConfiguration;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
        .AddOcelotWithSwaggerSupport((o) =>
        {
            o.Folder = "Configuration";
        })
        .AddEnvironmentVariables();

    // --- Service Registration ---
    builder.Services.AddOcelot().AddAppConfiguration();
    builder.Services.AddSwaggerForOcelot(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddHealthChecks();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITQ API Gateway", Version = "v1" });
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

    // --- Build the App ---
    var app = builder.Build();
    app.UseCors("ViteDev");


    // --- Middleware Pipeline ---
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();
    app.UseSwagger();

    // Map health checks and controllers BEFORE Ocelot using UseEndpoints
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health");
    });

    // Activate the Ocelot middleware and its Swagger UI.
    app.UseStaticFiles();
    app.UseSwaggerForOcelotUI(opt =>
        {
            opt.DownstreamSwaggerHeaders = new[]
            {
            new KeyValuePair<string, string>("Key", "Value"),
            new KeyValuePair<string, string>("Key2", "Value2"),
            };
        });

    app.UseOcelot().Wait();

    app.MapControllers();

    // --- Run ---
    app.Run();
}
catch (System.AggregateException ex)
{
    Console.WriteLine(ex.InnerExceptions);
    throw; // Re-throw the original exception to stop execution if needed
}
