using FleetManagement.Application;
using FleetManagement.Common;
using FleetManagement.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using FleetManagement.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using FleetManagement.Infrastructure.Context;

namespace FleetManagement.Api
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
            services.AddDbContext<FleetManagementContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:fleetdb"]));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FleetManagement.Api", Version = "v1" });
            });

            services.AddApplicationLayer();
            services.AddCommonLayer();
            services.AddInfrastructureLayer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FleetManagement.Api v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
