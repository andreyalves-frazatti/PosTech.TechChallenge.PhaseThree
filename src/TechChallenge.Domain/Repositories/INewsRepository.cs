using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repositories;

public interface INewsRepository
{
    Task AddAsync(News news, CancellationToken cancellationToken);

    Task<IEnumerable<News>> GetAllAsync(CancellationToken cancellationToken);

    Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}