using Microsoft.AspNetCore.Builder;
using OnboardingSIGDB1.Api._Base.Middlewares;

namespace OnboardingSIGDB1.Api._Base.Extensions
{
    public static class NotificationHandlingMiddlewareExtension
    {
        public static void UseNotificationHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<NotificationHandlingMiddleware>();
        }
    }
}
