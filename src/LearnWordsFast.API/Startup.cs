using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LearnWordsFast.API.Infrastructure;
using LearnWordsFast.API.Services;
using LearnWordsFast.DAL.EF;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LearnWordsFast.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IConfiguration _configuration;
        private RsaSecurityKey key;
        private TokenAuthOptions tokenOptions;

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
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(2048);
            RSAParameters publicKey = myRSA.ExportParameters(true);
            key = new RsaSecurityKey(publicKey);

            tokenOptions = new TokenAuthOptions
            {
                Audience = "",
                Issuer = "LWF",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature)
            };

            services.AddSingleton<TokenAuthOptions>(tokenOptions);

            //services.Configure<NHibernateOptions>(option =>
            //{
            //    option.Host = _configuration["Data:DefaultConnection:Host"];
            //    option.Port = int.Parse(_configuration["Data:DefaultConnection:Port"]);
            //    option.Database = _configuration["Data:DefaultConnection:Database"];
            //    option.User = _configuration["Data:DefaultConnection:User"];
            //    option.Password = _configuration["Data:DefaultConnection:Password"];
            //});

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

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

            services.AddCors();
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
            
            app.UseStaticFiles();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                TokenValidationParameters = new TokenValidationParameters()
                { 
                IssuerSigningKey = key,
                ValidAudience = tokenOptions.Audience,
                ValidIssuer = tokenOptions.Issuer,
                // When receiving a token, check that we've signed it.
                ValidateIssuerSigningKey = true,
                // When receiving a token, check that it is still valid.
                ValidateLifetime = true,
                // This defines the maximum allowable clock skew - i.e. provides a tolerance on the 
                // token expiry time when validating the lifetime. As we're creating the tokens locally
                // and validating them on the same machines which should have synchronised 
                // time, this can be set to zero. Where external tokens are used, some leeway here 
                // could be useful.
                ClockSkew = TimeSpan.Zero,}
            });

            app.UseIdentity();

            app.UseCors(
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action=GetAll}/{id?}");
            });
            
            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation("Application initialized");
            logger.LogInformation($"Environment: {_hostingEnv.EnvironmentName}");

            logger.LogInformation("Database name: " + _configuration["Data:DefaultConnection:Database"]);

            app.ApplicationServices.GetService<IInitializeDataManager>().Initialize();
        }
    }
}
