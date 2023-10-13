using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TechChallenge.WebAPI.Swagger;

internal static class SwaggerExtensions
{
    public static void ConfigureSwaggerDoc(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "FIAP Pós Tech - Tech Challenge Phase 3",
            Description =
                "Solution for registering news. Using <b>ASP.NET Core 7</b>, <b>EF Core 7</b> and authorization via <b>Json Web Token</b> (JWT)"
        });
    }
}