using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.IOC.Providers;

namespace OnboardingSIGDB1.IOC
{
    public static class StartupIOC
    {
        public static void AddOnboardingSIGDB1Services(this IServiceCollection services)
        {
            services.AddOnboardingSIGDB1Service();
        }
    }
}
