using Microsoft.Extensions.DependencyInjection;
using Okane.Core.Transactions;
using Okane.Dto;

namespace Okane.Core
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddCore(this IServiceCollection services) => 
      services.AddTransient<IHandler<ClassifyExpensesRequest, ClassifyExpensesResponse>, ClassifyExpensesHandler>();
  }

}