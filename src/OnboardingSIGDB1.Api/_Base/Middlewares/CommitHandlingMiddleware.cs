using Microsoft.AspNetCore.Http;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api._Base.Middlewares
{
    public class CommitHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CommitHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IDomainNotificationHandler notificacaoDeDominio, IUnitOfWork unitOfWork)
        {
            await _next.Invoke(context);

            string metodoHttp = context.Request.Method;
            var metodosQuePermitemAlteracao = new string[] { Resource.Post, Resource.Put, Resource.Delete };
            if (!metodosQuePermitemAlteracao.Contains(metodoHttp)) return;

            if (notificacaoDeDominio.HasNotifications) return;

            unitOfWork.Commit();
        }
    }
}
