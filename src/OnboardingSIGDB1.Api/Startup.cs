using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

     public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => {
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

                //string caminhoAplicacao =
                //    PlatformServices.Default.Application.ApplicationBasePath;
                //string nomeAplicacao =
                //    PlatformServices.Default.Application.ApplicationName;
                //string caminhoXmlDoc =
                //    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("v1/swagger.json", $"API V1");
            });

            app.UseMvc();
        }
    }
}
