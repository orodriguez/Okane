using System.Threading.Tasks;
using Okane.Dto;

namespace Okane.Core.Transactions
{
  public interface IHandler<in TRequest, TResponse>
    where TRequest : AbstractRequest
    where TResponse : AbstractResponse
  {
    Task<TResponse> Exec(TRequest response);
  }
}