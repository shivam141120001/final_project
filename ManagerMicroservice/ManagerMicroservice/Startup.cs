using ManagerMicroservice.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManagerMicroservice.Context;
using ManagerMicroservice.Models;
using Microsoft.OpenApi.Models;
using ManagerMicroservice.Middleware;

namespace ManagerMicroservice
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
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IAuthMiddleware, AuthMiddleware>();
            services.AddDbContext<ManagerMicroserviceDbContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString("connstr")));
            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1.0", new OpenApiInfo { Title = "Manager Microservice", Version = "1.0" }));
            services.AddCors();
           
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            loggerFactory.AddLog4Net();

            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1.0/swagger.json", "Manager Microservice (V 1.0)");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ManagerMicroserviceDbContext>();
            SeedData(context);
        }

        public static void SeedData(ManagerMicroserviceDbContext context)
        {
            context.Executives.AddRange(new Executive[] {
                new Executive() { EmailId="amit@gmail.com", ContactNumber= 8776543210, Locality="Delhi", Name="Amit", Username="executive_amit", Password="executive_amit" },
                new Executive() { EmailId="abhay@gmail.com", ContactNumber= 9876543210, Locality="Gurugram", Name="Abhay", Username="executive_abhay", Password="executive_abhay" },
                new Executive() { EmailId="shubhi@gmail.com", ContactNumber= 7896543210, Locality="Kanpur", Name="Shubhangi", Username="executive_shubhi", Password="executive_shubhi" },
                new Executive() { EmailId="shivi@gmail.com", ContactNumber= 6976543210, Locality="Meerut", Name="Shivam", Username="executive_shivi", Password="executive_shivi" }
            });
            context.Managers.AddRange(new Manager[]
            {
                new Manager()
                {
                    Name="Anjali",
                    EmailId="anjali@gmail.com",
                    ContactNumber= 9875453210,
                    Locality="Kanpur",
                    Username="manager_anjali",
                    Password="manager#anjali",
                }
            });
            context.SaveChanges();
        }
    }
}
