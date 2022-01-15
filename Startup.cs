using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostgresConnect.Data;

namespace PostgresConnect
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
            services.AddCors();
            //Database Tomfoolery
            string connectionString = null;
            //Get the Database environment var
            string envVar = Environment.GetEnvironmentVariable("DATABASE_URL");
            //Check the env ver was set if nto then use local database
            if (string.IsNullOrEmpty(envVar))
            {
                connectionString = Configuration.GetConnectionString("AshsDbContext");
            }
            else
            {
                //Here is where the fun begins
                var uri = new Uri(envVar);
                //Extract user into 
                var username = uri.UserInfo.Split(':')[0];
                var password = uri.UserInfo.Split(':')[1];
                //Extract host
                var host = uri.Host;
                //Build string
                connectionString =
                    //This line is important and was missing from the tutorial @
                    // https://daniellethurow.com/blog/2020/7/16/connecting-to-heroku-postgresql-with-net-core
                    "Host=" + host +
                    "; Database=" + uri.AbsolutePath.Substring(1) +
                    "; Username=" + username +
                    "; Password=" + password +
                    "; Port=" + uri.Port +
                    "; SSL Mode=Require; Trust Server Certificate=true;";

            }
            services.AddDbContext<AshsDbContext>(options =>

                options.UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention()
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .EnableSensitiveDataLogging()

            );
            services.AddControllers();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostgresConnect", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostgresConnect v1");
                    //c.RoutePrefix = "";
                    });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(
                options => options.AllowAnyMethod()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
