
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using raro_labs_cep.Services;

namespace raro_labs_cep
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //Configuração simples do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Contact = new OpenApiContact()
                        {
                            Email = "leonardo130584@gmail.com",
                            Name = "Leonardo M. Ott"
                        },
                        Title = "Desafio Raro Labs - Consulta Cep",
                        Version = "v1",
                        Description = "Desafio: <br> Criação de uma webapi que consuma um serviço de consulta de Cep gratuito. <br> https://viacep.com.br"
                    }); 
            });

            //Versionamento da API
            services.AddApiVersioning();

            //Injeção de Dependência
            services.AddScoped<IConsultaCep, ConsultaCep>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Raro Labs - Consulta Cep v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
