using TimeSeriesProcessing.Application.Infrastructure.Result.Dto;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Infrastructure.Result;

public interface IResultRepository
{
    Task<(IReadOnlyList<ResultItemDto> Results, int Count)> GetResultsAsync(ResultFilter filter);
    Task<AggregatedResult?> GetResultEntityByIdAsync(int id);
    Task InsertResultAsync(AggregatedResult result);
    void DeleteResultAsync(AggregatedResult result);
    /// <summary>
    /// Saves all changes in context
    /// </summary>
    Task SaveChangesAsync();
}