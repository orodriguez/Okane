using Microsoft.Extensions.DependencyInjection;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public static class ServiceCollectionExtensions
{
    public static void AddOkaneInMemoryStorage(this IServiceCollection services)
    {
        services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();
        services.AddSingleton<IExpensesRepository, InMemoryExpensesRepository>();
        services.AddSingleton<ICategoriesRepository, InMemoryCategoriesRepository>();
    }
}