using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data._Base;
using System.Linq;

namespace OnboardingSIGDB1.IOC.Providers
{
    public static class OnboardingSIGDB1RepositorioProvider
    {
        public static void AddOnboardingSIGDB1Repositorio(this IServiceCollection services)
        {
            var onboardingSIGDB1RepositorioAssembly = typeof(OnboardingSIGDB1Repositorio).Assembly;
            var listaDeOnboardingSIGDB1RepositorioFilha =
                from type in onboardingSIGDB1RepositorioAssembly.GetExportedTypes()
                where (
                    type.BaseType == typeof(OnboardingSIGDB1Repositorio) ||
                    type.BaseType?.BaseType == typeof(OnboardingSIGDB1Repositorio)
                )
                && !type.IsAbstract
                select new { Interfaces = type.GetInterfaces(), Implementacao = type };

            foreach (var onboardingSIGDB1RepositorioFilha in listaDeOnboardingSIGDB1RepositorioFilha)
            {
                if (onboardingSIGDB1RepositorioFilha.Interfaces.Any())
                {
                    foreach (var interFace in onboardingSIGDB1RepositorioFilha.Interfaces)
                    {
                        services.AddScoped(interFace, onboardingSIGDB1RepositorioFilha.Implementacao);
                    }
                }
            }
        }
    }
}

