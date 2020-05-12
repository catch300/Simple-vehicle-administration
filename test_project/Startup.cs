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
using Autofac.Extensions.DependencyInjection;
using test_project.Models.ViewModels;

namespace test_project
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<VehicleContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            
            //services.AddScoped<IVehicleMake, VehicleMakeService>();
            //services.AddScoped<IVehicleModel, VehicleModelService>();

            // Create the container builder.
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<VehicleMake>().As<VehicleMakeVM>();
            builder.RegisterType<VehicleModel>().As<VehicleModelVM>();
            ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
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
        }
    }
}
