using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace OnboardingSIGDB1.Api._Base.Extensions
{
    public static class OnboardingSIGDB1StartupExtension
    {
        public static void UseOnboardingSIGDB1(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandling();

            app.UseNotificationHandling();

            app.UseSwaggerDoc(env);
        }
    }
}
