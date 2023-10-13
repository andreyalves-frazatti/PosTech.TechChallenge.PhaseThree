using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using TechChallenge.Application.Commands.CreateUser;
using TechChallenge.IntegrationTests.Fixtures;

namespace TechChallenge.IntegrationTests.Controllers;

[Collection("WebApplicationFactory")]
public class UserControllerTests
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly IFixture _autoFixture = new Fixture();

    public UserControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_ReturnStatusCode201Created_When_PostCreateUserEndpointSuccess()
    {
        /* arrange */
        const string postCreateUserUrl = "api/users";
        const string role = "admin";

        var command = new CreateUserCommand(
            username: _autoFixture.Create<string>(),
            password: _autoFixture.Create<string>(),
            role);

        var content = JsonContent.Create(command);

        var client = _factory.CreateClient();

        /* act */
        var response = await client.PostAsync(postCreateUserUrl, content, CancellationToken.None);

        /* assert */
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        command.ExistsOnDatabase(_factory).Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnStatusCode500InternalServerError_When_PostCreateUserEndpointFail()
    {
        /* arrange */
        const string postCreateUserUrl = "api/users";
        const string role = "admin";

        var content = JsonContent.Create(new
        {
            Username = _autoFixture.Create<string>(),
            Password = _autoFixture.Create<string>(),
            Role = role
        });

        var client = _factory.CreateClient();

        /* creating the same user */
        await client.PostAsync(postCreateUserUrl, content, CancellationToken.None);

        /* act */
        var response = await client.PostAsync(postCreateUserUrl, content, CancellationToken.None);

        /* assert */
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}