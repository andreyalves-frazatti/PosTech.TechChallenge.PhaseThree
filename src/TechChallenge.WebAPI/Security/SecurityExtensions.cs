using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TechChallenge.WebAPI.Security;

public static class SecurityExtensions
{
    public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var securityOptions = new SecurityOptions();

        configuration
            .GetSection(SecurityOptions.OptionSection)
            .Bind(securityOptions);

        services.AddScoped(_ => securityOptions);
        services.AddScoped<SecurityService>();

        var secretKey = Encoding.ASCII.GetBytes(securityOptions.SecretKey);

        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}