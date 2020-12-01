using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Api._Base.Extensions;
using OnboardingSIGDB1.IOC;

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
            services.AddOnboardingSIGDB1Services();

            services.AddMvc();

            services.AddSwaggerDoc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDoc(env);

            app.UseExceptionHandling();

            app.UseMvc();
        }
    }
}
