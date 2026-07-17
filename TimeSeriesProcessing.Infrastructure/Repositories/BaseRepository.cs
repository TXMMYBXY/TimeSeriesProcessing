using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Application.Infrastructure;
using TimeSeriesProcessing.Infrastructure.Data;

namespace TimeSeriesProcessing.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        return await _dbContext.Set<T>()
            .Where(e => EF.Property<int>(e, "Id") == id     )
            .ExecuteDeleteAsync();
    }

    public async Task<int> BulkDeleteAsync(IReadOnlyCollection<int> ids)
    {
        return await _dbContext.Set<T>()
            .Where(e => ids.Contains(EF.Property<int>(e,"Id")))
            .ExecuteDeleteAsync();
    }
    
    /// <summary>
    /// Saves all changes in context
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}