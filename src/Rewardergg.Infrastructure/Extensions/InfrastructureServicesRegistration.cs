using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rewardergg.Application.Interfaces;
using Rewardergg.Infrastructure.Repositories;

namespace Rewardergg.Infrastructure.Extensions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
