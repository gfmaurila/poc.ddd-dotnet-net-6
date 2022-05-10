using Demo.Domain.Core.Interfaces.Repositorys;
using Demo.Infrastruture.Repository.Repositorys;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastruture.CrossCutting.IOC.DependenceInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenceRepository(IServiceCollection services)
        {
            #region IOC Repositorys SQL
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
