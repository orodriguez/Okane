using System.Linq;
using System.Threading.Tasks;
using Okane.Core.Transactions;
using Okane.Dto.Transactions.Classify;
using Xunit;

namespace Okane.Core.Tests
{
  public class ClassifyExpensesTest : Test
  {
    [Fact]
    public async Task File()
    {
      var result = (ClassifyExpensesSuccessResponse) await ClassifyExpenses("sample1.csv");

      Assert.Equal(50, result.Records.Count());
    }
  }
}