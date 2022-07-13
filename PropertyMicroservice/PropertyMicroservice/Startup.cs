using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using PropertyMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyMicroservice.Context;
using PropertyMicroservice.Models;
using Microsoft.OpenApi.Models;
using PropertyMicroservice.Middleware;

namespace PropertyMicroservice
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
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IAuthMiddleware, AuthMiddleware>();
            services.AddDbContext<PropertyMicroserviceDbContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString("constr")));
            services.AddSwaggerGen(p => 
            {
                p.SwaggerDoc(name: "v1.0", new OpenApiInfo { Title = ".Property Microservice", Version = "1.0" });
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1.0/swagger.json", "Manager Microservice (V 1.0)");
            });

            loggerFactory.AddLog4Net();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<PropertyMicroserviceDbContext>();
            SeedData(context);
        }

        public static void SeedData(PropertyMicroserviceDbContext context)
        {
            context.Properties.AddRange(new Property[]
            {
                new Property() { PropertyType = "Commercial", Locality = "Kanpur", Budget = 4500000 },
                new Property() { PropertyType = "Residential", Locality = "Kanpur", Budget = 2900000 },
                new Property() { PropertyType = "Commercial", Locality = "Delhi", Budget = 4600000 },
                new Property() { PropertyType = "Residential", Locality = "Delhi", Budget = 8700000 },
                new Property() { PropertyType = "Commercial", Locality = "Pune", Budget = 7800000 },
                new Property() { PropertyType = "Residential", Locality = "Pune", Budget = 6400000 },                                                                      
                new Property() { PropertyType = "Commercial", Locality = "Mumbai", Budget = 99900000 },
                new Property() { PropertyType = "Residential", Locality = "Mumbai", Budget = 94000000 },
                new Property() { PropertyType = "Commercial", Locality = "Gurugram", Budget = 9900000 },
                new Property() { PropertyType = "Residential", Locality = "Gurugram", Budget = 67000000 }
            });
            context.SaveChanges();
        }
    }
}
