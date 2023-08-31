using Microsoft.Extensions.DependencyInjection;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static void AddOkaneEntityFrameworkStorage(
        this IServiceCollection services)
    {
        services.AddDbContext<OkaneDbContext>();
        services.AddTransient<IExpensesRepository, ExpensesRepository>();
        services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        services.AddTransient<IUsersRepository, UsersRepository>();
    }
}