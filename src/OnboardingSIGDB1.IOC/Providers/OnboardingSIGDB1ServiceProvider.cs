using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain._Base.Services;
using System.Linq;

namespace OnboardingSIGDB1.IOC.Providers
{
    public static class OnboardingSIGDB1ServiceProvider
    {
        public static void AddOnboardingSIGDB1Service(this IServiceCollection services)
        {
            var onboardingSIGDB1ServiceAssembly = typeof(OnboardingSIGDB1Service).Assembly;
            var listaDeOnboardingSIGDB1ServiceFilha =
                from type in onboardingSIGDB1ServiceAssembly.GetExportedTypes()
                where type.BaseType == typeof(OnboardingSIGDB1Service)
                select new { Interfaces = type.GetInterfaces(), Implementacao = type };

            foreach (var onboardingSIGDB1ServiceFilha in listaDeOnboardingSIGDB1ServiceFilha)
            {
                if (onboardingSIGDB1ServiceFilha.Interfaces.Any())
                {
                    foreach (var service in onboardingSIGDB1ServiceFilha.Interfaces)
                    {
                        services.AddScoped(service, onboardingSIGDB1ServiceFilha.Implementacao);
                    }
                }
            }
        }
    }
}

