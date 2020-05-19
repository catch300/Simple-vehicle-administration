using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_service.Models;
using Microsoft.EntityFrameworkCore;
using Project_service.Service;
using AutoMapper;
using test_project.MappingProfiles;
using Autofac;
using test_project.Models.ViewModels;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Project_service.PagingFIlteringSorting;

namespace test_project
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews();

            services.AddDbContext<VehicleContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

        }

        //Container Builder for Autofac 
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().As<IVehicleMake>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModel>();
            builder.AddAutoMapper(typeof(Startup).Assembly);

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=VehicleMake}/{action=Index}/{id?}");
            });

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }
    }
}
