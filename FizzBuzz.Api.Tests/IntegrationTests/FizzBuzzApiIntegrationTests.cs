using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using FizzBuzz.Api.Models;

namespace FizzBuzz.Api.Tests.IntegrationTests;

[TestFixture]
public class FizzBuzzApiIntegrationTests
{
    private WebApplicationFactory<FizzBuzz.Api.Program> _factory;
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<FizzBuzz.Api.Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [TestCase(1, "1")]
    [TestCase(3, "Fizz")]
    [TestCase(5, "Buzz")]
    [TestCase(15, "FizzBuzz")]
    [Test]
    public async Task GET_Valid_Number_Returns_Correct_Result(int number, string expected)
    {
        var response = await _client.GetAsync($"/api/fizzbuzz/{number}");

        var result = await response.Content.ReadFromJsonAsync<FizzBuzzResponse>();
        Assert.That(result!.Result, Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(101)]
    [Test]
    public async Task GET_Invalid_Number_Returns_400(int number)
    {
        var response = await _client.GetAsync($"/api/fizzbuzz/{number}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task POST_Valid_Batch_Returns_Results()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 3, 5, 15, 30 } };

        var response = await _client.PostAsJsonAsync("/api/fizzbuzz", request);

        var result = await response.Content.ReadFromJsonAsync<FizzBuzzBatchResponse>();
        Assert.That(result!.Results, Has.Count.EqualTo(5));
        Assert.That(result.Results[0].Result, Is.EqualTo("1"));
        Assert.That(result.Results[3].Result, Is.EqualTo("FizzBuzz"));
    }

    [Test]
    public async Task POST_Empty_Array_Returns_400()
    {
        var request = new FizzBuzzBatchRequest { Numbers = Array.Empty<int>() };

        var response = await _client.PostAsJsonAsync("/api/fizzbuzz", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task POST_Wrong_Count_Returns_400()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3 } };

        var response = await _client.PostAsJsonAsync("/api/fizzbuzz", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task POST_OutOfRange_Returns_400()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3, 4, 150 } };

        var response = await _client.PostAsJsonAsync("/api/fizzbuzz", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
