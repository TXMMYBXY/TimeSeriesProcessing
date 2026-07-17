using TimeSeriesProcessing.Application.Infrastructure;
using TimeSeriesProcessing.Application.Result.Dto;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Result;

public interface IResultRepository : IBaseRepository<AggregatedResult>
{
    Task<(IReadOnlyList<ResultItemDto> Results, int Count)> GetResultsAsync(ResultFilter filter);
}