using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot.Configuration;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace MainApiGateway
{
    public class AppsettingsServiceDiscoveryProvider : IServiceDiscoveryProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceProviderConfiguration _config;
        private readonly DownstreamRoute _downstreamRoute;
        private readonly IConfiguration _configuration;

        public AppsettingsServiceDiscoveryProvider(
            IServiceProvider serviceProvider, 
            ServiceProviderConfiguration config, 
            DownstreamRoute downstreamRoute,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _downstreamRoute = downstreamRoute;
            _configuration = configuration;
        }

        public async Task<List<Service>> GetAsync()
        {
            var serviceName = _downstreamRoute?.ServiceName;
    
            // Отладка в консоль
            Console.WriteLine($"[SD] ServiceName from route: '{serviceName}'");
            
            if (string.IsNullOrEmpty(serviceName))
                return new List<Service>();

            // Читаем через GetSection — надёжнее индексера
            var serviceConfig = _configuration
                .GetSection("Services")
                .GetSection(serviceName)
                .GetValue<string>("DownstreamPath");
            
            Console.WriteLine($"[SD] DownstreamPath for '{serviceName}': '{serviceConfig}'");
            
            if (string.IsNullOrEmpty(serviceConfig))
                return new List<Service>();

            try
            {
                var uri = new Uri(serviceConfig);
                Console.WriteLine($"[SD] ✓ Resolved: {serviceName} -> {uri}");
                
                return new List<Service>
                {
                    new Service(
                        name: serviceName,
                        hostAndPort: new ServiceHostAndPort(uri.Host, (ushort)uri.Port),
                        id: $"{serviceName}-1",
                        version: "1.0",
                        tags: new[] { serviceName }
                    )
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SD] ✗ Error: {ex.Message}");
                return new List<Service>();
            }
        }
    }
}