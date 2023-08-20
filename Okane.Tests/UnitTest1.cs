using Okane.Core;

namespace Okane.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Class1 c = new Class1();
        var x = 1;
        Assert.Equal(1, x);
    }
}