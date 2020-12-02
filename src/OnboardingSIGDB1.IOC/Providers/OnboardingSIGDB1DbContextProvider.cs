using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data._Contexto;
using OnboardingSIGDB1.Domain._Base.Interfaces;

namespace OnboardingSIGDB1.IOC.Providers
{
    public static  class OnboardingSIGDB1DbContextProvider
    {
        public static void AddOnboardingSIGDB1DbContextService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<DbContext>(p => p.GetService<OnboardingSIGDB1Context>());
        }
    }
}
