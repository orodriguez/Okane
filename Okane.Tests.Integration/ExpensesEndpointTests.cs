using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Okane.Contracts;
using Okane.WebApi.Controllers;

namespace Okane.Tests.Integration;

public class ExpensesEndpointTests : IClassFixture<WebApplicationFactory<ExpensesController>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    
    public ExpensesEndpointTests(WebApplicationFactory<ExpensesController> appFactory) => 
        _client = appFactory.CreateClient();

    public async Task InitializeAsync()
    {
        var userEmail = $"user{DateTime.Now.ToFileTime()}@mail.com";
        var password = "1234";
        
        await SignUp(userEmail, password);

        var token = await GetToken(userEmail, password)
            ;
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    }

    public Task DisposeAsync() => Task.CompletedTask;

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

    private async Task SignUp(string userEmail, string password)
    {
        var signUpResponse = await _client.PostAsync("/auth/signup",
            new StringContent(JsonSerializer.Serialize(new SignUpRequest
            {
                Email = userEmail,
                Password = password
            }), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, signUpResponse.StatusCode);
    }

    private async Task<string> GetToken(string userEmail, string password)
    {
        var tokenResponse = await _client.PostAsync("/auth/token",
            new StringContent(JsonSerializer.Serialize(new SignInRequest
            {
                Email = userEmail,
                Password = password
            }), Encoding.UTF8, "application/json"));

        var tokenDictionary =
            JsonSerializer.Deserialize<Dictionary<string, string>>(await tokenResponse.Content.ReadAsStringAsync());

        var token = tokenDictionary!["token"];
        return token;
    }
}