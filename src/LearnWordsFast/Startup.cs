using LearnWordsFast.DAL.NHibernate;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;

namespace LearnWordsFast
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<NHibernateOptions>(option =>
            {
                option.Host = configuration["Data:DefaultConnection:Host"];
                option.Port = int.Parse(configuration["Data:DefaultConnection:Port"]);
                option.Database = configuration["Data:DefaultConnection:Database"];
                option.User = configuration["Data:DefaultConnection:User"];
                option.Password = configuration["Data:DefaultConnection:Password"];
            });

            services.AddNHibernateSession();
            services.AddMvc();

            services.AddSingleton(_ => configuration);

            services.AddScoped<IWordRepository, WordNHibernateRepository>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();

            services.AddSingleton(x => ((ILoggerFactory)x.GetService(typeof(ILoggerFactory))).CreateLogger("Application"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseNHibernateSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation("Application initialized");
            logger.LogInformation($"Environment: {hostingEnv.EnvironmentName}");

            logger.LogInformation("Database name: " + configuration["Data:DefaultConnection:Database"]);
        }
    }
}
