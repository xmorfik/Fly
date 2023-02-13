﻿using Fly.Core.Entities;
using Fly.Data;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace Fly.IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<FlyDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var alice = userMgr.FindByNameAsync("andrii").Result;
            if (alice == null)
            {
                alice = new User
                {
                    UserName = "andrii",
                    Email = "a.dukhno@outlook.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alice, "PassWord123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "andrii"),
                            new Claim(JwtClaimTypes.GivenName, "_"),
                            new Claim(JwtClaimTypes.FamilyName, "_"),
                            new Claim(JwtClaimTypes.WebSite, "_"),
                            new Claim("Role", "Administrator"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }
        }
    }
}
