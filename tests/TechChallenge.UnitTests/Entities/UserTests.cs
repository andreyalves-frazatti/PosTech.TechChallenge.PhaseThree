using AutoFixture;
using FluentAssertions;
using TechChallenge.Domain.Entities;

namespace TechChallenge.UnitTests.Entities;

public class UserTests
{
    private readonly IFixture _autoFixture = new Fixture();

    [Fact]
    public void Should_CreateUserInstance_When_AllFieldsAreValid()
    {
        /* Arrange */
        var username = _autoFixture.Create<string>();
        var password = _autoFixture.Create<string>();
        var role = _autoFixture.Create<string>();

        /* Act */
        User user = new(username, password, role);

        /* Assert */
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Username.Should().Be(username);
        user.Password.Should().Be(password);
        user.Role.Should().Be(role);
    }
}