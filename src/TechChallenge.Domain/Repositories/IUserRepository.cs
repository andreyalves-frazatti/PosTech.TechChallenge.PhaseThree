using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);

    Task<User?> FindAsync(string username, string password, CancellationToken cancellationToken);

    Task<bool> AlreadyExistsAsync(User user, CancellationToken cancellationToken);
}