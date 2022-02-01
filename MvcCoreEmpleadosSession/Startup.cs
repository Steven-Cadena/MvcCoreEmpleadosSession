using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCoreEmpleadosSession.Data;
using MvcCoreEmpleadosSession.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession
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
            string cadenahospital = this.Configuration.GetConnectionString("cadenahospital");
            string cadenahospitalcasa = this.Configuration.GetConnectionString("cadenahospitalcasa");

            //ACCESO A DATOS SQL SERVER *********************SQL SERVER
            services.AddTransient<RepositoryEmpleados>();
            services.AddDbContext<EmpleadosContext>(options => options.UseSqlServer(cadenahospital));
            /**********************************************************/
            //UNA SESSION FUNCIONA POR TIEMPO DE INACTIVIDAD
            //DEBEMOS INDICAR EL TIEMPO QUE DURARAN LOS OBJETOS 
            //DENTRO DE LA SESSION
            /****************************IMPORTANTE*/
            services.AddHttpContextAccessor();
            //services.AddSingleton<IHttpContextAccesor, HttpContextAccesor>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            /***************************************/
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //importante para la session y el orden también
            app.UseSession();
            /*****************************/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
