using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public static class SessionMiddlewareExtension
    {
        public static IApplicationBuilder UseNHibernateSession(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SessionMiddleware>();
        }

        public static IServiceCollection AddNHibernateSession(this IServiceCollection services, Func<IServiceProvider, ISessionFactory> sessionFactoryProvider)
        {
            services.AddScoped<ISessionProvider, LazySessionProvider>();
            services.AddSingleton(sessionFactoryProvider);
            return services;
        }
    }
}