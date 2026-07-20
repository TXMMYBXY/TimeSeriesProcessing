using TimeSeriesProcessing.Application.Services.Result;
using TimeSeriesProcessing.Application.Services.Result.Dto;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Abstractions.Repositories;

public interface IResultRepository
{
    Task<(IReadOnlyList<ResultItemDto> Results, int TotalCount)> GetResultsAsync(ResultFilter filter);
    Task<AggregatedResult?> GetByFileNameAsync(string fileName);
    Task InsertResultAsync(AggregatedResult result);
    void DeleteResult(AggregatedResult result);
    /// <summary>
    /// Saves all changes in context
    /// </summary>
    Task SaveChangesAsync();
}