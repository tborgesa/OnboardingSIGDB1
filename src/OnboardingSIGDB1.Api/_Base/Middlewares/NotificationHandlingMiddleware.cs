using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api._Base.Middlewares
{
    public class NotificationHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public NotificationHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IDomainNotificationHandler notificacaoDeDominio)
        {
            await _next.Invoke(context);

            if (notificacaoDeDominio.HasNotifications)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(CriarMensagemDeNotificacaoCustomizada(notificacaoDeDominio));
            }
        }

        private string CriarMensagemDeNotificacaoCustomizada(IDomainNotificationHandler notificacaoDeDominio)
        {
            var notificacaoes = notificacaoDeDominio.GetNotifications().Select(_ => _.Value);
            return JsonConvert.SerializeObject(notificacaoes);
        }
    }
}
