using Microsoft.OpenApi.Models;

namespace Fly.WebAPI.Extensions;

public static class ConfigureSwaggerExtension
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fly API",
                Version = "v1",
                Description = "Fly API",
                //TermsOfService = new Uri(""),
                Contact = new OpenApiContact
                {
                    Name = "Andrii Dukhno",
                    Email = "a.dukhno@outlook.com.com",
                    //Url = new Uri(""),
                },
                //License = new OpenApiLicense
                //{
                //    Name = "",
                //    Url = new Uri(""),
                //}
            });

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                    },
                    new List<string>()
                }
            });
        });
    }
}
