using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.IOC.Providers;
using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.IOC
{
    public static class StartupIOC
    {
        public static void ResolverAsDependenciasDoOnboardingSIGDB1(this IServiceCollection services)
        {
            services.AddDomainBaseService();

            services.AddOnboardingSIGDB1DbContextService();

            services.AddOnboardingSIGDB1Service();

            services.AddOnboardingSIGDB1Repositorio();
        }
    }
}
