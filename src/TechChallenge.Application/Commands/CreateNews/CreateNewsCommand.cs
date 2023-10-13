using MediatR;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Commands.CreateNews;

public class CreateNewsCommand : IRequest<News>
{
    public CreateNewsCommand(string title, string content, DateTime date, string author)
    {
        Title = title;
        Content = content;
        Date = date;
        Author = author;
    }

    public string Title { get; init; }

    public string Content { get; init; }

    public DateTime Date { get; init; }

    public string Author { get; init; }
}