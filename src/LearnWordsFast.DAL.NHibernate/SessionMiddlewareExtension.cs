using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace LearnWordsFast.DAL.NHibernate
{
    public static class SessionMiddlewareExtension
    {
        public static IApplicationBuilder UseNHibernateSession(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SessionMiddleware>();
        }

        public static IServiceCollection AddNHibernateSession(this IServiceCollection services)
        {
            services.AddScoped<ISessionProvider, LazySessionProvider>();
            return services;
        }
    }
}