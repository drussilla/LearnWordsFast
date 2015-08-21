﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWordsFast.DAL.NHibernate;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Repositories;
using LearnWordsFast.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;
using NHibernate;

namespace LearnWordsFast
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json");

            configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNHibernateSession(x => SessionFactoryProvider.GetSessionFactory());
            services.AddMvc();

            services.AddSingleton(_ => configuration);

            services.AddScoped<IWordRepository, WordNHibernateRepository>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseNHibernateSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
