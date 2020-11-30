using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;
using System.IO;
using System.Text.Json;
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

        public async Task Invoke(HttpContext context, IHostingEnvironment env)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                var mensagemDeErro = env.IsDevelopment() ?
                    $"{Environment.NewLine}{exception}" :
                    "";

                var mensagemParaUsuario = Resource.FormatarResource(Resource.MensagemDeErro500, mensagemDeErro);

                var result = JsonSerializer.Serialize(
                    new
                    {
                        erro = mensagemParaUsuario
                    });

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }
    }
}
