using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

namespace Telerik_Reporting_Rest_Service
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
            //services.AddRazorPages();
            services.AddControllers().AddNewtonsoftJson();

            // Configure dependencies for ReportsController.
            services.TryAddSingleton<IReportServiceConfiguration>(sp =>
                new ReportServiceConfiguration
                {
                    ReportingEngineConfiguration = ConfigurationHelper.ResolveConfiguration(sp.GetService<IWebHostEnvironment>()),
                    HostAppId = "ReportingCore3App",
                    Storage = new FileStorage(),
                    ReportSourceResolver = new UriReportSourceResolver(
                        System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports"))
                });

            services.AddCors(corsOption => corsOption.AddPolicy(
              "ReportingRestPolicy",
              corsBuilder =>
              {
                  corsBuilder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
              }));
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
                app.UseExceptionHandler("/Error");
            }

            //app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseCors("ReportingRestPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
