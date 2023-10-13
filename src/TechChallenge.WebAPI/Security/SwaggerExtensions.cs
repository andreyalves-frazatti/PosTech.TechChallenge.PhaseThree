using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TechChallenge.WebAPI.Security;

public static class SwaggerExtensions
{
    public static void ConfigureSwaggerSecurityScheme(this SwaggerGenOptions swaggerGenOptions)
    {
        var description = new StringBuilder();
        description.Append("<p>JWT Authorization header using the Bearer scheme. ");
        description.Append("Enter Bearer [space] and then your token in the text input below. ");
        description.Append("<b>Example:</b> Bearer [your token here]</p>");

        swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = description.ToString(),
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    }
}