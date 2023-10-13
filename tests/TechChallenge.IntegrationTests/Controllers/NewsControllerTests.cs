using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using TechChallenge.IntegrationTests.Fixtures;
using TechChallenge.WebAPI.Models;

namespace TechChallenge.IntegrationTests.Controllers;

[Collection("WebApplicationFactory")]
public class NewsControllerTests
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly IFixture _autoFixture = new Fixture();

    public NewsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private async Task<string> GetTokenAsync(HttpClient client)
    {
        const string postCreateUserUrl = "api/users";
        const string role = "admin";

        var credentialsModel = new CredentialsModel
        {
            Username = _autoFixture.Create<string>(),
            Password = _autoFixture.Create<string>(),
        };

        var createUserContent = JsonContent.Create(new
        {
            credentialsModel.Username,
            credentialsModel.Password,
            Role = role
        });

        var createUserResponse = await client.PostAsync(postCreateUserUrl, createUserContent, CancellationToken.None);

        if (!createUserResponse.IsSuccessStatusCode)
            throw new InvalidOperationException("Fail to create user.");

        const string postLoginUrl = "api/login";

        var loginContent = JsonContent.Create(credentialsModel);

        var loginResponse = await client.PostAsync(postLoginUrl, loginContent, CancellationToken.None);

        if (!loginResponse.IsSuccessStatusCode)
            throw new InvalidOperationException("Fail to login user.");

        return await loginResponse.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task Should_ReturnStatusCode200Ok_When_PostAddNewsEndpointSuccess()
    {
        /* arrange */
        const string addNewsUrl = "api/news";

        var createNewsModel = new CreateNewsModel()
        {
            Title = _autoFixture.Create<string>(),
            Content = _autoFixture.Create<string>(),
            Author = _autoFixture.Create<string>()
        };

        var createNewsContent = JsonContent.Create(createNewsModel);

        var client = _factory.CreateClient();

        var token = await GetTokenAsync(client);

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        /* act */
        var response = await client.PostAsync(addNewsUrl, createNewsContent, CancellationToken.None);
        
        /* assert */
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNull();
        createNewsModel.ExistsOnDatabase(_factory).Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnStatusCodeSuccessOkWithNewsList_When_GetAllNewsEndpointSuccess()
    {
        /* arrange */
        const string getAllNewsUrl = "api/news";

        var client = _factory.CreateClient();

        var token = await GetTokenAsync(client);

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        /* act */
        var response = await client.GetAsync(getAllNewsUrl, CancellationToken.None);

        /* assert */
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Should().NotBeNull();
    }
}