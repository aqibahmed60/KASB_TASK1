using Bookstore.Data.Interfaces;
using Bookstore.Data.Models;
using Bookstore.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI1
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
            services.AddTransient<IBookRepository,BookDatabase>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStoreAPI1", Version = "v1" });
            });

            var connection = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Iqra\Documents\Bookstore.mdf; Integrated Security = True; Connect Timeout = 30";

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreAPI1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors
            (
                options => options.WithOrigins("https://localhost:44316")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials() 
                
             );


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var servicescope=app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = servicescope.ServiceProvider.GetRequiredService<BookStoreContext>();

                context.Database.EnsureCreated();
            }


        }
    }
}
