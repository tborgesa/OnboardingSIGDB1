using Microsoft.AspNetCore.Builder;
using OnboardingSIGDB1.Api._Base.Middlewares;

namespace OnboardingSIGDB1.Api._Base.Extensions
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
