using Microsoft.AspNetCore.Builder;
using OnboardingSIGDB1.Api._Base.Middlewares;

namespace OnboardingSIGDB1.Api._Base.Extensions
{
    public static class CommitHandlingMiddlewareExtension
    {
        public static void UseCommitHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<CommitHandlingMiddleware>();
        }
    }
}
