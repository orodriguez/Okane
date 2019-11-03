using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Okane.Core.Transactions;
using Okane.Dto;

namespace Okane.Core.Tests
{
  public class Test
  {
    protected async Task<ClassifyExpensesResponse> ClassifyExpenses(string file)
    {
      var services = new ServiceCollection();

      services.AddCore();

      var provider = services.BuildServiceProvider();

      return await provider.GetRequiredService<IHandler<ClassifyExpensesRequest, ClassifyExpensesResponse>>()
        .Exec(new ClassifyExpensesRequest
        {
          File = file
        });
    }
  }
}