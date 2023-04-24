using Metika.Identity.Context;
using Metika.Identity.Entities;
using Metika.Identity.Services;
using Metika.Security.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Metika.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> options) 
            where TContext : UserDbContext
        {
            return AddIdentityContext<TContext, User, Role>(services,configuration,options);
        }
        public static IServiceCollection AddIdentityContext<TContext,TUser,TRole>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> options) 
            where TContext : UserDbContext 
            where TUser: IdentityUser<long> 
            where TRole : IdentityRole<long>
        {
            services.AddTransient(typeof(IUserService<,>),typeof(UserService<,>));
            services.AddDbContext<TContext>(options);
            services.AddIdentity<TUser, TRole>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();
            services.AddRsaJwt(configuration);
            return services;
        }

        public static IApplicationBuilder UseIdentityContext(this IApplicationBuilder app)
        {
            app.UseSecurityFeatures();
            return app;
        }
    }
}
