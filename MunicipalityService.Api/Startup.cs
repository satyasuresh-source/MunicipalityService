using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MunicipalityService.Api.Filters;
using MunicipalityService.Api.Middleware;
using MunicipalityService.Business;
using MunicipalityService.Business.Interfaces;
using MunicipalityService.DataAccess;
using MunicipalityService.DataAccess.Interfaces;
using MunicipalityService.Models.Utilites;
using MunicipalityService.Models.Utilites.Interfaces;
using Serilog;

namespace MunicipalityService.Api
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
            services.AddTransient<IFileAccessManager, FileAcessManager>();
            services.AddTransient(typeof(IFileAccessManager), typeof(FileAcessManager));

            services.AddTransient<ITaxManager, TaxManager>();
            services.AddTransient(typeof(ITaxManager), typeof(TaxManager));
                        
            services.AddTransient<ITaxDataAccess, TaxDataAccess>();
            services.AddTransient(typeof(ITaxDataAccess), typeof(TaxDataAccess));

            services.AddTransient<IFileAccessDataAccess, FileAccessDataAccess>();
            services.AddTransient(typeof(IFileAccessDataAccess), typeof(FileAccessDataAccess));
            
            services.AddTransient<IFileProcessor, FileProcessor>();
            services.AddTransient(typeof(IFileProcessor), typeof(FileProcessor));

            services.AddScoped<ValidateModelStateAttribute>();
            
            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Rest Services",
                    Description = "ASP.NET Core Web API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandlingMiddleWare();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Information()
                             .MinimumLevel.Override("Logging INFO", Serilog.Events.LogEventLevel.Debug)
                             .WriteTo.File("Logs/LogInformation.txt")
                             .CreateLogger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            

            app.UseSwagger();
                        
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

        }
    }
}
