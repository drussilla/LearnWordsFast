using System.Threading.Tasks;
using LearnWordsFast.API.Services;
using LearnWordsFast.DAL.EF;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LearnWordsFast.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{_hostingEnv.EnvironmentName}.json", optional: true)
                .AddUserSecrets();

            _configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<NHibernateOptions>(option =>
            //{
            //    option.Host = _configuration["Data:DefaultConnection:Host"];
            //    option.Port = int.Parse(_configuration["Data:DefaultConnection:Port"]);
            //    option.Database = _configuration["Data:DefaultConnection:Database"];
            //    option.User = _configuration["Data:DefaultConnection:User"];
            //    option.Password = _configuration["Data:DefaultConnection:Password"];
            //});

            services.Configure<MvcJsonOptions>(options =>
            {
                if (_hostingEnv.IsDevelopment())
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                }

                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.Configure<MvcOptions>(options =>
            {
                options.CacheProfiles.Add("IndexPage",
                    new CacheProfile
                    {
                        Duration = 60 * 60 * 24
                    });
            });

            //services.AddNHibernateSession<SessionFactoryProvider>();

            services
                .AddIdentity<User, string>()
                .AddEF()
                .AddDefaultTokenProviders();

            services
                .AddScoped<ISignInManager, AspNetMvcSignInManager>();
            services
                .AddScoped<IUserManager, AspNetMvcUserManager>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Cookies.ApplicationCookie.CookieHttpOnly = false;
                // work around to disable redirect on unauthorized access.
                // setting LoginPath to null does not work anymore.
                // TODO: use bearer token instead
                options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = x =>
                    {
                        x.Response.StatusCode = 401;
                        return Task.FromResult(0);
                    }
                };
            });

            services.AddMvc();

            services.AddSingleton(_ => _configuration);

            services.AddEF();
            // todo: move registration to DAL or HNibernateDAL
            //services.AddScoped<IWordRepository, WordRepository>();
            //services.AddScoped<ILanguageRepository, LanguageRepository>();
            //services.AddSingleton<IInitializeDataManager, InitialDataManager>();

            services.AddScoped<ITrainingService, TrainingService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddScoped<ITrainingWordProvider, TrainingWordProvider>();
            services.AddScoped<ITrainingSessionFactory, TrainingSessionFactory>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            //app.UseNHibernateSession();
            app.UseEFContext();

            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
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

            app.ApplicationServices.GetService<IInitializeDataManager>().Initialize();
        }
    }
}
