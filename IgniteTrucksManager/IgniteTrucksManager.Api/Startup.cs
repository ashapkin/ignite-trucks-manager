using Apache.Ignite.Core;
using IgniteTrucksManager.Core.ComputeTasks;
using IgniteTrucksManager.Core.ExternalProvider;
using IgniteTrucksManager.Core.Ignite;
using IgniteTrucksManager.Core.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IgniteTrucksManager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIgnite, IIgnite>(serviceProvider => IgniteFactory.Start());
            services.AddSingleton<ITrucksRepository, TrucksRepository>();
            services.AddSingleton<ExternalDataProvider, ExternalDataProvider>();
            services.AddSingleton<IIgniteCompute, IgniteCompute>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            loggerFactory.AddLog4Net("config/log4net.xml");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}