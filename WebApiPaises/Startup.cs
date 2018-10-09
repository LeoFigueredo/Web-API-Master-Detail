using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiPaises.Models;

namespace WebApiPaises
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
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseInMemoryDatabase("paisDB"));
            services.AddMvc().AddJsonOptions(ConfigureJson);
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            if (! context.Paises.Any())
            {
                context.Paises.AddRange(new List<Pais>()
                {
                    new Pais() { Nombre = "Venezuela", Provincias = new List<Provincia>(){
                                new Provincia(){ Nombre = "Táchira"},
                                new Provincia(){ Nombre = "Carácas"},
                                new Provincia(){ Nombre = "Mérida"},
                                new Provincia(){ Nombre = "Zulia"}
                                } }, 
                    new Pais() { Nombre = "Brasil", Provincias = new List<Provincia>(){
                                new Provincia(){ Nombre = "Rio"},
                                new Provincia(){ Nombre = "Sao Paulo"},
                                new Provincia(){ Nombre = "Brasilia"},
                                new Provincia(){ Nombre = "Porto Alegre"}
                                } },
                    new Pais() { Nombre = "Colombia", Provincias = new List<Provincia>(){
                                new Provincia(){ Nombre = "Santander"},
                                new Provincia(){ Nombre = "Bogotá"},
                                new Provincia(){ Nombre = "Cali"},
                                new Provincia(){ Nombre = "Antioquia"}
                                } },
                    new Pais() { Nombre = "Ecuador" , Provincias = new List<Provincia>(){
                                new Provincia(){ Nombre = "Quito"},
                                new Provincia(){ Nombre = "Guayaquil"},
                                } }
                });
                context.SaveChanges();

            }
        }
    }
}
