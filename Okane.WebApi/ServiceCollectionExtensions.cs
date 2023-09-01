using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Okane.Core.Security;

namespace Okane.WebApi;

public static class ServiceCollectionExtensions
{
    public static void AddOkaneWebApi(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<IUserSession, HttpContextUserSession>();
        services.AddHttpContextAccessor();

        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        if (jwtSettings == null)
            throw new Exception("Unable to read JwtSettings from config");

        // Authentication
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey))
                };
            });
    }
}