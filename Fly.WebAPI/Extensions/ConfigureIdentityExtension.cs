using Fly.Core.Entities;
using Fly.Data;
using Microsoft.AspNetCore.Identity;

namespace Fly.WebAPI.Extensions
{
    public static class ConfigureIdentityExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<FlyDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
