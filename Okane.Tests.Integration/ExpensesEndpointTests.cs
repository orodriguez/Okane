using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Okane.WebApi.Controllers;

namespace Okane.Tests.Integration;

public class ExpensesEndpointTests : IClassFixture<WebApplicationFactory<ExpensesController>>
{
    private readonly HttpClient _client;

    public ExpensesEndpointTests(WebApplicationFactory<ExpensesController> appFactory) => 
        _client = appFactory.CreateClient();

    [Fact]
    public async Task GetExpenses()
    {
        var response = await _client.GetAsync("/expenses");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetExpensesByCategory()
    {
        var response = await _client.GetAsync("/expenses?Category=Food");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}