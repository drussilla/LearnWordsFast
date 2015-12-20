using LearnWordsFast.DAL.EF.Repositories;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWordsFast.DAL.EF
{
    public static  class MiddlewareExtensions
    {
        public static IdentityBuilder AddEF(this IdentityBuilder services)
        {
            services
                .AddUserStore<UserRepository>()
                .AddRoleStore<RoleRepository>();
            return services;
        }

        public static IApplicationBuilder UseEFContext(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SessionMiddleware>();
        }

        public static IServiceCollection AddEF(this IServiceCollection services)
        {
            services.AddEntityFramework();
            services.AddScoped<IDbContext, LazyDbContext>();
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IInitializeDataManager, InitialDataManager>();
            return services;
        }
    }
}