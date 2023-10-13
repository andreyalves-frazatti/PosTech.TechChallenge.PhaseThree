using MediatR;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Commands.CreateNews;

public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, News>
{
    private readonly INewsRepository _newsRepository;

    public CreateNewsCommandHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<News> Handle(CreateNewsCommand command, CancellationToken cancellationToken)
    {
        var news = new News(command.Title, command.Content, command.Date, command.Author);

        await _newsRepository.AddAsync(news, cancellationToken);

        return news;
    }
}