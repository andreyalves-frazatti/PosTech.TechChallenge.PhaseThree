using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using TechChallenge.WebAPI.Models;

namespace TechChallenge.IntegrationTests.Controllers;

[Collection("WebApplicationFactory")]
public class LoginControllerTests
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly IFixture _autoFixture = new Fixture();

    public LoginControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private static async Task CreateUserAsync(CredentialsModel credentials, HttpClient client)
    {
        const string postCreateUserUrl = "api/users";
        const string role = "admin";

        var content = JsonContent.Create(new
        {
            credentials.Username,
            credentials.Password,
            Role = role
        });

        var response = await client.PostAsync(postCreateUserUrl, content, CancellationToken.None);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    [Fact]
    public async Task Should_ReturnStatusCode200OkWithToken_When_PostAuthenticateEndpointSuccess()
    {
        /* arrange */
        const string postLoginUrl = "api/login";

        var credentialsModel = new CredentialsModel
        {
            Username = _autoFixture.Create<string>(),
            Password = _autoFixture.Create<string>(),
        };

        var content = JsonContent.Create(credentialsModel);
        var client = _factory.CreateClient();

        /* Creating a valid user */
        await CreateUserAsync(credentialsModel, client);

        /* act */
        var response = await client.PostAsync(postLoginUrl, content, CancellationToken.None);

        /* assert */
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_ReturnStatusCode401Unauthorized_When_PostAuthenticateEndpointFail()
    {
        /* arrange */
        const string postLoginUrl = "api/login";

        var credentialsModel = new CredentialsModel
        {
            Username = _autoFixture.Create<string>(),
            Password = _autoFixture.Create<string>(),
        };

        var content = JsonContent.Create(credentialsModel);
        var client = _factory.CreateClient();

        /* act */
        var response = await client.PostAsync(postLoginUrl, content, CancellationToken.None);

        /* assert */
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}