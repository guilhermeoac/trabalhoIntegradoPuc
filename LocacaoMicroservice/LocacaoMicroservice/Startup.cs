using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Locacao.Aplicacao;
using Locacao.Aplicacao.Interfaces;
using Locacao.Dominio.Repositorios;
using Locacao.Infraestrutura.DBContexts;
using Locacao.Infraestrutura.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LocacaoMicroservice
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
            services.AddDbContext<LocacaoContext>(u => u.UseSqlServer(Configuration.GetConnectionString("LocacaoVeicDB"), m => m.MigrationsAssembly("Locacao.Api")));
            services.AddScoped<ILocacaoAplicacao, LocacaoAplicacao>();
            services.AddScoped<ILocacaoRepositorio, LocacaoRepositorio>();
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Serviço de Locação",
                    Description = "Serviço responsável pelo domínio da locação do veiculo, com base na arquitetura Onion contendo as regras de negócio, integração com o serviço de veículo e disponibilização de rotas para a camada de apresentação do usuário (SPA) seguindo os conceitos da arquitetura de API Rest.",
                    Contact = new OpenApiContact
                    {
                        Name = "Alan Samora Rodrigues",
                        Email = "1278768@sga.pucminas.br"
                    }
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "Locacao.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locacao Microservice V1");
            });
        }
    }
}
