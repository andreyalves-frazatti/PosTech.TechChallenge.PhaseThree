using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Infrastructure.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly DatabaseContext _context;

    public NewsRepository(DatabaseContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public async Task AddAsync(News news, CancellationToken cancellationToken)
    {
        await _context.News!.AddAsync(news, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<News>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.News!.ToListAsync(cancellationToken);
    }

    public Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.News!.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}