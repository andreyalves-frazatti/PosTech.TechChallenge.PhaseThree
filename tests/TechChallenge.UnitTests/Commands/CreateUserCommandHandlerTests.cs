using AutoFixture;
using FluentAssertions;
using Moq;
using TechChallenge.Application.Commands.CreateUser;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.UnitTests.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly IFixture _autoFixture = new Fixture();
    private readonly Mock<IUserRepository> _userRepository = new();

    [Fact]
    public async Task Should_CreateUser_When_UserNotExists()
    {
        /* arrange */
        var command = _autoFixture.Create<CreateUserCommand>();
        var cancellationToken = new CancellationToken();

        _userRepository
            .Setup(c => c.AlreadyExistsAsync(
                It.Is<User>(u
                    => u.Id != Guid.Empty
                       && u.Username == command.Username
                       && u.Password == command.Password),
                cancellationToken))
            .ReturnsAsync(false);

        var commandHandler = new CreateUserCommandHandler(_userRepository.Object);

        /* act */
        var user = await commandHandler.Handle(command, cancellationToken);

        /* assert */
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Username.Should().Be(command.Username);
        user.Password.Should().Be(command.Password);

        _userRepository.Verify(c => c.AddAsync(user, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserAlreadyExists()
    {
        /* arrange */
        var command = _autoFixture.Create<CreateUserCommand>();
        var cancellationToken = new CancellationToken();

        _userRepository
            .Setup(c => c.AlreadyExistsAsync(It.IsAny<User>(), cancellationToken))
            .ReturnsAsync(true);

        var commandHandler = new CreateUserCommandHandler(_userRepository.Object);

        /* act */
        Func<Task> act = () => commandHandler.Handle(command, cancellationToken);

        /* assert */
        await act.Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("This user already exists.");

        _userRepository.Verify(c => c.AddAsync(It.IsAny<User>(), cancellationToken), Times.Never);
    }
}