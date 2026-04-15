using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Config;
using System.Text;
using Ocelot.ServiceDiscovery;
using MainApiGateway;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
    .AddOcelot("Configuration", builder.Environment);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();

builder.Services.AddSingleton<ServiceDiscoveryFinderDelegate>((sp, config, route) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new AppsettingsServiceDiscoveryProvider(sp, config, route, configuration);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Info = new Microsoft.OpenApi.OpenApiInfo
        {
            Title = "ITQ API Gateway",
            Version = "v1",
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ViteDev", policy =>
    {
        policy.WithOrigins("http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
string secretKey = jwtSettings?.SecretKey ?? string.Empty;

if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
{
    if (builder.Environment.IsDevelopment())
    {
        secretKey = "DefaultDevelopmentKeyThatIsAtLeast32Chars!";
    }
    else
    {
        throw new InvalidOperationException("JWT SecretKey is missing or too short (min 32 chars) in Production!");
    }
}
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.Configure<JwtSettings>(jwtSettingsSection);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer ?? "ITQuantum",
        ValidAudience = jwtSettings?.Audience ?? "ITQuantumUsers",
        IssuerSigningKey = key,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors("ViteDev");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseEndpoints(_ => { });

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
});
app.MapControllers();

// Aggregated OpenAPI endpoint for Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1.json");

    // Endpoint that aggregates all downstream OpenAPI documents
    app.MapGet("/openapi/aggregated.json", async (IHttpClientFactory httpClientFactory) =>
    {
        var httpClient = httpClientFactory.CreateClient();

        // Map service name to gateway route prefix
        var serviceUrls = new[]
        {
            new { Name = "mentor", Url = "http://localhost:5300/openapi/v1.json" },
            new { Name = "auth", Url = "http://localhost:5200/openapi/v1.json" },
            new { Name = "content", Url = "http://localhost:5100/openapi/v1.json" },
            new { Name = "course", Url = "http://localhost:5400/openapi/v1.json" }
        };

        var aggregatedPaths = new System.Text.Json.Nodes.JsonObject();
        var aggregatedSchemas = new System.Text.Json.Nodes.JsonObject();

        foreach (var service in serviceUrls)
        {
            try
            {
                var response = await httpClient.GetAsync(service.Url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    using var doc = System.Text.Json.JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    // Collect schema names from this service
                    var serviceSchemaNames = new List<string>();
                    if (root.TryGetProperty("components", out var components) &&
                        components.TryGetProperty("schemas", out var schemas))
                    {
                        foreach (var schema in schemas.EnumerateObject())
                        {
                            serviceSchemaNames.Add(schema.Name);
                            var schemaKey = $"{service.Name}_{schema.Name}";
                            if (aggregatedSchemas[schemaKey] == null)
                            {
                                aggregatedSchemas.Add(schemaKey, System.Text.Json.Nodes.JsonNode.Parse(schema.Value.GetRawText()));
                            }
                        }
                    }

                    // Merge paths and update $ref to use prefixed schema names
                    if (root.TryGetProperty("paths", out var paths))
                    {
                        foreach (var path in paths.EnumerateObject())
                        {
                            // Use downstream path as-is
                            var gatewayPath = path.Name;

                            if (aggregatedPaths[gatewayPath] == null)
                            {
                                var pathJson = path.Value.GetRawText();

                                // Update all $ref to use prefixed schema names
                                // This covers requestBody, responses, parameters, etc.
                                foreach (var schemaName in serviceSchemaNames)
                                {
                                    pathJson = pathJson.Replace(
                                        $"#/components/schemas/{schemaName}",
                                        $"#/components/schemas/{service.Name}_{schemaName}"
                                    );
                                }

                                aggregatedPaths.Add(gatewayPath, System.Text.Json.Nodes.JsonNode.Parse(pathJson));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch OpenAPI from {service.Name}: {ex.Message}");
            }
        }

        var result = new System.Text.Json.Nodes.JsonObject
        {
            ["openapi"] = "3.0.0",
            ["info"] = new System.Text.Json.Nodes.JsonObject
            {
                ["title"] = "ITQ Aggregated API",
                ["version"] = "v1",
                ["description"] = "Aggregated OpenAPI document from all downstream services"
            },
            ["paths"] = aggregatedPaths,
            ["components"] = new System.Text.Json.Nodes.JsonObject
            {
                ["schemas"] = aggregatedSchemas,
                ["securitySchemes"] = new System.Text.Json.Nodes.JsonObject
                {
                    ["Bearer"] = new System.Text.Json.Nodes.JsonObject
                    {
                        ["type"] = "http",
                        ["scheme"] = "bearer",
                        ["bearerFormat"] = "JWT",
                        ["description"] = "JWT Authorization header. Get token from /api/auth/v1/Auth/register or /api/auth/v1/Auth/login"
                    }
                }
            },
            ["security"] = new System.Text.Json.Nodes.JsonArray
            {
                new System.Text.Json.Nodes.JsonObject
                {
                    ["Bearer"] = new System.Text.Json.Nodes.JsonArray()
                }
            },
            ["servers"] = new System.Text.Json.Nodes.JsonArray
            {
                new System.Text.Json.Nodes.JsonObject
                {
                    ["url"] = "http://localhost:5000",
                    ["description"] = "API Gateway"
                }
            }
        };

        return Results.Json(result, contentType: "application/json");
    });

    app.MapScalarApiReference(options =>
    {
        options.AddDocument("v1", "Gateway API", "/openapi/v1.json");
        options.AddDocument("aggregated", "Aggregated API", "/openapi/aggregated.json", isDefault: true);
    });
}

app.UsePathBase("/gateway");

await app.UseOcelot();
await app.RunAsync();
