using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Okane.Contracts;
using Okane.Core.Security;
using Okane.Core.Services;
using Okane.Core.Validations;

namespace Okane.Core;

public static class ServiceCollectionExtensions
{
    public static void AddOkaneCore(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IExpensesService, ExpensesService>();
        services.AddTransient<IValidator<CreateExpenseRequest>, DataAnnotationsValidator<CreateExpenseRequest>>();
        services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();
        services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
        services.AddTransient<Func<DateTime>>(provider => () => DateTime.Now);
        
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    }
}