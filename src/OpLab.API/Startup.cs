using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpLab.API.Domain.Repositories;
using OpLab.API.Application;
using OpLab.API.Infrastructure.Data.Repositories;
using OpLab.API.Infrastructure.Data.Context;

namespace OpLab.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddAutoMapper();

            services
                .AddDbContext<CitiesContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("OpLabConnection")))
                .AddDbContext<PointsOfInterestContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("OpLabConnection")));

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IPointOfInterestRepository, PointOfInterestRepository>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IPointOfInterestService, PointOfInterestService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
