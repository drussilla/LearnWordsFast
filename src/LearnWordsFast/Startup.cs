using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LearnWordsFast
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.CacheProfiles.Add("IndexPage",
                    new CacheProfile
                    {
                        Duration = 60 * 60 * 24
                    });
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "catchAll",
                    template: "{*any}",
                    defaults: new { controller = "Home", action = "Index" });

            });

            var logger = loggerFactory.CreateLogger("Startup Client");
            logger.LogInformation("Application initialized");
        }
    }
}
