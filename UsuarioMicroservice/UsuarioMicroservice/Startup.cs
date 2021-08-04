using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UsuarioMicroservice.DBContexts;
using UsuarioMicroservice.Repository;

namespace UsuarioMicroservice
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
            services.AddDbContext<UsuarioContext>(u => u.UseSqlServer(Configuration.GetConnectionString("UsuarioDB")));
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IOperadorRepository, OperadorRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Serviço de Usuario",
                    Description = "Serviço responsável pelo domínio de usuário, realizando cruds para o cliente e o operador seguindo os conceitos da arquitetura de API Rest.",
                    Contact = new OpenApiContact
                    {
                        Name = "Alan Samora Rodrigues",
                        Email = "1278768@sga.pucminas.br"
                    }
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "UsuarioMicroservice.xml");
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuario Microservice V1");
            });
        }
    }
}
