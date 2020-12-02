using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnboardingSIGDB1.Data._Contexto;
using OnboardingSIGDB1.Domain._Base.Entidades.AppSettings;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;

namespace OnboardingSIGDB1.IOC._Extensions
{
    public static class OnboardingSIGDB1ContextExtension
    {
        public static void AddOnboardingSIGDB1Context(this IServiceCollection services,
            AppSettingsDoOnboardingSIGDB1 appSettingsDoOnboardingSIGDB1)
        {
            services.AddDbContext<OnboardingSIGDB1Context>(options =>
             {
                 options.UseSqlServer(appSettingsDoOnboardingSIGDB1.StringDeConexoes.OnboardingSIGDB1);
             });
        }
    }
}
