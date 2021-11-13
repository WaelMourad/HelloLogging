using HelloLogging.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace HelloLogging.IOC.DatabaseContext
{
    public static class DatabaseContextStartupHelper
    {
        public static IServiceCollection AddInMemoryDbContext (this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("TestDB"));

            SeedTestData(services);

            return services;
        }

        private static void SeedTestData(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var dataContext = sp.GetService<DataContext>();

            dataContext.AddTestData(dataContext);
        }
    }
}
