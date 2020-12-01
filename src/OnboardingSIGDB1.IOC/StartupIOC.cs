using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data._Contexto;
using OnboardingSIGDB1.Domain._Base.Entidades.AppSettings;
using OnboardingSIGDB1.IOC.Providers;

namespace OnboardingSIGDB1.IOC
{
    public static class StartupIOC
    {
        public static void AddOnboardingSIGDB1Services(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsDoOnboardingSIGDB1 = new AppSettingsDoOnboardingSIGDB1();
            Configuration.Bind("OnboardingSIGDB1", appSettingsDoOnboardingSIGDB1);
            services.AddSingleton(appSettingsDoOnboardingSIGDB1);

            services.AddDomainBaseService();

            services.AddOnboardingSIGDB1Service();

            services.AddOnboardingSIGDB1Repositorio();
        }
    }
}
