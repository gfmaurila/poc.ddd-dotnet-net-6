using Demo.Domain.Core.Interfaces.Services;
using Demo.Domain.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using PocNugetEncryptDecrypt.Interfaces;
using PocNugetEncryptDecrypt.Services;

namespace Demo.Infrastruture.CrossCutting.IOC.DependenceInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenceService(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPasswordService, PasswordService>();
        }
    }
}
