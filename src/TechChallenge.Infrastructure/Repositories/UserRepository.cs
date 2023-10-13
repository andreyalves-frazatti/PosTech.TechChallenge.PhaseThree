using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users!.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<User?> FindAsync(string username, string password, CancellationToken cancellationToken)
    {
        return _context.Users!
            .FirstOrDefaultAsync(c => c.Username == username && c.Password == password, cancellationToken);
    }

    public async Task<bool> AlreadyExistsAsync(User user, CancellationToken cancellationToken)
    {
        return (await _context.Users!.FirstOrDefaultAsync(c => c.Username == user.Username, cancellationToken)) != null;
    }
}