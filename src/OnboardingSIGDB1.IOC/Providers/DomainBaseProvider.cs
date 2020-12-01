using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain._Base.Interfaces;

namespace OnboardingSIGDB1.IOC.Providers
{
    public static class DomainBaseProvider
    {
        public static void AddDomainBaseService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDomainNotificationHandler), typeof(DomainNotificationHandler));
        }
    }
}
