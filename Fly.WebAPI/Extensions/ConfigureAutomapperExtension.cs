﻿using System.Reflection;

namespace Fly.WebAPI.Extensions;

public static class ConfigureAutomapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.Load("Fly.Core"));
        return services;
    }
}
