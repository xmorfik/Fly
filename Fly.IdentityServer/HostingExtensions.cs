using Duende.IdentityServer;
using Fly.Core.Entities;
using Fly.Data;
using Fly.WebAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Fly.IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddPostgres(builder.Configuration);

            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<FlyDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<User>()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddDeveloperSigningCredential();

            var configuration = builder.Configuration;
            //builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            //{
            //    microsoftOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //    microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
            //    microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
            //});

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();
            app.MapRazorPages();

            return app;
        }
    }
}