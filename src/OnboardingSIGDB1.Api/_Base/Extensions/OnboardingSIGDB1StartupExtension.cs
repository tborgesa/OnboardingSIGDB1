using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain._Base.Entidades.AppSettings;
using OnboardingSIGDB1.IOC;
using OnboardingSIGDB1.IOC._Extensions;

namespace OnboardingSIGDB1.Api._Base.Extensions
{
    public static class OnboardingSIGDB1StartupExtension
    {
        public static void AddOnboardingSIGDB1(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsDoOnboardingSIGDB1 = new AppSettingsDoOnboardingSIGDB1();
            Configuration.Bind("OnboardingSIGDB1", appSettingsDoOnboardingSIGDB1);
            services.AddSingleton(appSettingsDoOnboardingSIGDB1);

            services.AddOnboardingSIGDB1Context(appSettingsDoOnboardingSIGDB1);

            services.AddOnboardingSIGDB1Services();
        }

        public static void UseOnboardingSIGDB1(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandling();

            app.UseNotificationHandling();

            app.UseSwaggerDoc(env);
        }
    }
}
