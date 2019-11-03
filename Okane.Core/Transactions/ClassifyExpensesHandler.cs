using System.Threading.Tasks;
using Okane.Dto;

namespace Okane.Core.Transactions
{
  public class ClassifyExpensesHandler : IHandler<ClassifyExpensesRequest, ClassifyExpensesResponse>
  {
    public Task<ClassifyExpensesResponse> Exec(ClassifyExpensesRequest response)
    {
      throw new System.NotImplementedException();
    }
  }
}