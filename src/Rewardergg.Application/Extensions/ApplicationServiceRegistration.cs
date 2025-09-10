using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rewardergg.Application.Mappers;

namespace Rewardergg.Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                               IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
