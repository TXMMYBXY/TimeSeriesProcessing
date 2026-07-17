namespace TimeSeriesProcessing.Application.Infrastructure;

public interface IBaseRepository<in T> where T : class
{
    Task CreateAsync(T entity);
    Task<int> DeleteByIdAsync(int id);
    Task<int> BulkDeleteAsync(IReadOnlyCollection<int> ids);
    Task SaveChangesAsync();
}