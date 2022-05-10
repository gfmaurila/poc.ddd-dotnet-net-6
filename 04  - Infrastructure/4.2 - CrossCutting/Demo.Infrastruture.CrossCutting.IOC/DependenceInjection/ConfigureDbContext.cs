using Demo.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastruture.CrossCutting.IOC.DependenceInjection
{
    public class ConfigureDbContext
    {
        public static void ConfigureDependenceDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(options => options.UseSqlServer(configuration.GetConnectionString("UserAPI")));
        }
    }
}
