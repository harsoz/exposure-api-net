using exposure_api.SignalRModule.Logic;
using exposure_modules.CalculatorModule.Interface;
using exposure_modules.CalculatorModule.Logic;
using exposure_modules.CalculatorModule.Model;
using exposure_modules.GoombaModule.Interface;
using exposure_modules.GoombaModule.Logic;
using exposure_modules.ShapeModule.Interface;
using exposure_modules.ShapeModule.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile("nominations.json", false, true);
            Configuration = builder.Build();

            // set nomination config
            this.SetNominationConfig();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // dependencies
            services.AddSingleton<IShapeLogic, ShapeLogic>();
            services.AddSingleton<ICalculator, AtmLogic>();
            services.AddSingleton<IGoombaLogic, GoombaLogic>();

            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Exposure API",
                    Description = "Polygonsure API for demos",
                    Contact = new OpenApiContact
                    {
                        Name = "Harley Pasoz",
                        Email = string.Empty,

                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                    }
                });
            });
            services.AddSignalR();
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
            }
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(
             options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                ); //This needs to set everything allowed

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<DataHub>("/datahub");
            });
        }

        private void SetNominationConfig()
        {
            var configpivot = Configuration.GetSection("NominationConfig").Get<PivotNominationConfig>();
            NominationConfig.Coins = configpivot.Coins;
        }
    }
}
