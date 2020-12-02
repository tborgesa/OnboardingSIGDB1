using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api._Base.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception excecao)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(CriarMensagemDeErroCustomizada(excecao));
            }
        }

        private string CriarMensagemDeErroCustomizada(Exception excecao)
        {
            dynamic retornoDeErro = new ExpandoObject();
            retornoDeErro.MensagemParaOUsuario = Resource.MensagemDeErro500;

#if DEBUG
            retornoDeErro.MensagemParaODesenvolvedor = excecao.Message;
            retornoDeErro.DetalheParaODesenvolvedor = excecao.StackTrace.Split("\r\n");
#endif

            return JsonConvert.SerializeObject(retornoDeErro);
        }
    }
}
