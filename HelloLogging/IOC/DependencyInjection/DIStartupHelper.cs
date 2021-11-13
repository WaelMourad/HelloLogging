using HelloLogging.Interfaces;
using HelloLogging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HelloLogging.IOC.DependencyInjection
{
    public static class DIStartupHelper
    {
        public static IServiceCollection AddInfrastructre(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
