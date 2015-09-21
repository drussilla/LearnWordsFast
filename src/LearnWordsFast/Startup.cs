using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.NHibernate;
using LearnWordsFast.DAL.NHibernate.Repositories;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NHibernate;
using ILoggerFactory = Microsoft.Framework.Logging.ILoggerFactory;

namespace LearnWordsFast
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment hostingEnv, IApplicationEnvironment appEnv)
        {
            _hostingEnv = hostingEnv;

            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{_hostingEnv.EnvironmentName}.json", optional: true)
                .AddUserSecrets();

            _configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<NHibernateOptions>(option =>
            {
                option.Host = _configuration["Data:DefaultConnection:Host"];
                option.Port = int.Parse(_configuration["Data:DefaultConnection:Port"]);
                option.Database = _configuration["Data:DefaultConnection:Database"];
                option.User = _configuration["Data:DefaultConnection:User"];
                option.Password = _configuration["Data:DefaultConnection:Password"];
            });

            services.Configure<MvcJsonOptions>(options =>
            {
                if (_hostingEnv.IsDevelopment())
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                }

                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddNHibernateSession<SessionFactoryProvider>();

            services
                .AddIdentity<User, string>()
                .AddUserStore<UserRepository>()
                .AddRoleStore<RoleRepository>()
                .AddDefaultTokenProviders();

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.CookieHttpOnly = false;
                options.LoginPath = null;
            });

            services.AddMvc();

            services.AddSingleton(_ => _configuration);

            // todo: move registration to DAL or HNibernateDAL
            services.AddScoped<IWordRepository, WordNHibernateRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddScoped(x => x.GetService<ISessionProvider>().GetSession());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseNHibernateSession();

            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action=GetAll}/{id?}");

                routes.MapRoute(
                    name: "catchAll",
                    template: "{*any}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation("Application initialized");
            logger.LogInformation($"Environment: {_hostingEnv.EnvironmentName}");

            logger.LogInformation("Database name: " + _configuration["Data:DefaultConnection:Database"]);
        }
    }
}
