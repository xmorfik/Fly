using Azure.Communication.Email.Models;
using Duende.IdentityServer;
using Fly.Core.Entities;
using Fly.Core.Services;
using Fly.Data;
using Fly.Services;
using Fly.WebAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Reflection;

namespace Fly.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddSqlServer(builder.Configuration);

        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<FlyDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddScoped<IEmailSender<EmailMessage>, EmailSender>();

        var cfg = builder.Configuration.GetSection(ClientUriConfiguration.Configuration).Get<ClientUriConfiguration>();
        Config.ClientUriConfiguration = cfg;

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

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
        {
            microsoftOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
            microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
        });

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