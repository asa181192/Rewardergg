using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rewardergg.Application.Interfaces;
using Rewardergg.Application.Services;
using Rewardergg.Infrastructure.Services;

namespace Rewardergg.Infrastructure.Extensions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.AddScoped<IStartggService, StartggService>();
            services.AddScoped<IAuthWorkflowService, AuthWorkflowService>();

            return services;
        }
    }
}
