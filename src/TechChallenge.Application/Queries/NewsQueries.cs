using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Queries;

public class NewsQueries
{
    private readonly INewsRepository _newsRepository;

    public NewsQueries(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public Task<IEnumerable<News>> GetAllAsync(CancellationToken cancellationToken)
        => _newsRepository.GetAllAsync(cancellationToken);
}