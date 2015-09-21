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
            services.AddSingleton<ISessionManager, SessionManager>();
            return services;
        }

        public static IServiceCollection AddNHibernateSession<T>(this IServiceCollection services) where T : class, ISessionFactoryProvider
        {
            services.AddScoped<ISessionProvider, LazySessionProvider>();
            services.AddSingleton<ISessionFactoryProvider, T>();
            services.AddSingleton<ISessionFactory>(x => ((ISessionFactoryProvider)x.GetService(typeof(ISessionFactoryProvider))).Get());
            services.AddSingleton<ISessionManager, SessionManager>();
            return services;
        }
    }
}