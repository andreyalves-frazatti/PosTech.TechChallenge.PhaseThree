using AutoFixture;
using FluentAssertions;
using TechChallenge.Domain.Entities;

namespace TechChallenge.UnitTests.Entities;

public class NewsTests
{
    private readonly IFixture _autoFixture = new Fixture();

    [Fact]
    public void Should_CreateNewsInstance_When_AllFieldsAreValid()
    {
        /* Arrange */
        var title = _autoFixture.Create<string>();
        var content = _autoFixture.Create<string>();
        var date = DateTime.Today;
        var author = _autoFixture.Create<string>();

        /* Act */
        News news = new(title, content, date, author);

        /* Assert */
        news.Should().NotBeNull();
        news.Id.Should().NotBeEmpty();
        news.Title.Should().Be(title);
        news.Content.Should().Be(content);
        news.Date.Should().Be(date);
        news.Author.Should().Be(author);
    }
}