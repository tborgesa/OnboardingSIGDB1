using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace OnboardingSIGDB1.Api._Base.Middleware
{
    public static class SwaggerMiddlewareExtension
    {
        public static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "OnboardingSIGDB1",
                        Version = "v1",
                        Description = "Projeto de OnBoarding para o projeto do SENAC - DB1",
                        Contact = new OpenApiContact
                        {
                            Name = "Thiago Borges Amaral",
                            Url = new Uri("https://github.com/tborgesa")
                        }
                    });
            });
        }

        public static void UseSwaggerDoc(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", $"OnboardingSIGDB1 V1 ({env.EnvironmentName})");
            });
        }
    }
}
