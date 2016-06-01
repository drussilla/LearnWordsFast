using LearnWordsFast.DAL.EF.Repositories;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

        public static IServiceCollection AddEF(this IServiceCollection services)
        {
            services.AddDbContext<Context>(
                options =>
                    options.UseSqlite("Filename=learnwordsfast.db", b => b.MigrationsAssembly("LearnWordsFast.API")));
            
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IInitializeDataManager, InitialDataManager>();
            return services;
        }
    }
}