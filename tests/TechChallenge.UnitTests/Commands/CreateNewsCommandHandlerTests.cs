using AutoFixture;
using FluentAssertions;
using Moq;
using TechChallenge.Application.Commands.CreateNews;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.UnitTests.Commands;

public class CreateNewsCommandHandlerTests
{
    private readonly IFixture _autoFixture = new Fixture();
    private readonly Mock<INewsRepository> _newsRepository = new();

    [Fact]
    public async Task Should_CreateNews_When_CommandIsValid()
    {
        /* arrange */
        var command = _autoFixture.Create<CreateNewsCommand>();
        var cancellationToken = new CancellationToken();

        var commandHandler = new CreateNewsCommandHandler(_newsRepository.Object);

        /* act */
        var news = await commandHandler.Handle(command, cancellationToken);

        /* assert */
        news.Should().NotBeNull();
        news.Id.Should().NotBeEmpty();
        news.Title.Should().Be(command.Title);
        news.Content.Should().Be(command.Content);
        news.Date.Should().Be(command.Date);
        news.Author.Should().Be(command.Author);

        _newsRepository.Verify(c => c.AddAsync(news, cancellationToken), Times.Once);
    }
}