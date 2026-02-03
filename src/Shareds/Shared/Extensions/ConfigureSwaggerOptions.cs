using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shared.Extensions
{
    // This class configures Swagger to generate separate documents for each API version using a primary constructor.
    // It also allows setting a custom title for the API.
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, string apiTitle = "API")
        : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            // Use the 'provider' parameter from the primary constructor.
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                // Use the 'apiTitle' parameter from the primary constructor.
                Title = $"{apiTitle} v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
