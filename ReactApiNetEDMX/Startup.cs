using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using React.AspNet;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using Ninject;
using System.Web.Mvc;
using Ninject.Web.WebApi;
using ReactApiNetEDMX.Store.Interfaces;
using ReactApiNetEDMX.Store.Repositories;

namespace ReactCoreApiApp
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
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();

            //services.AddControllers();
            //services.AddDbContext<ShopContext>();
            var shopEntitiesConnString = Configuration.GetConnectionString("ShopEntities");
            services.AddScoped(_ => new ReactApiNetEDMX.Store.Repositories.ShopEntities(shopEntitiesConnString));
            services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));
            services.AddControllers().AddNewtonsoftJson(); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseReact(config => { });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use((ctx, next) =>
            {
                ctx.Response.Headers.Add("Access-Control-Expose-Headers", "*");
                if (ctx.Request.Method.ToLower() == "options")
                {
                    ctx.Response.StatusCode = 204;

                    return Task.CompletedTask;
                }
                return next();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot/web-admin";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
