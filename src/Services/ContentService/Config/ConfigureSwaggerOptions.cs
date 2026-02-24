using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ContentService.Config
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            // News API
            options.SwaggerDoc("news-v1.0", CreateInfoForApiVersion("News API", "News endpoints", "v1.0"));
            
            // Slider API
            options.SwaggerDoc("slider-v1.0", CreateInfoForApiVersion("Slider API", "Slider endpoints", "v1.0"));
            
            // Gallery API
            options.SwaggerDoc("gallery-v1.0", CreateInfoForApiVersion("Gallery API", "Gallery endpoints", "v1.0"));
        }

        static OpenApiInfo CreateInfoForApiVersion(string title, string description, string version)
        {
            return new OpenApiInfo()
            {
                Title = title,
                Version = version,
                Description = description,
                Contact = new OpenApiContact() { Name = "IsusLOVVS", Email = "suslovvs2002@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };
        }
    }
}
