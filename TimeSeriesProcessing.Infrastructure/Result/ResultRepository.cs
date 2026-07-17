using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Application.Result;
using TimeSeriesProcessing.Application.Result.Dto;
using TimeSeriesProcessing.Application.Value.Dto;
using TimeSeriesProcessing.Domain.Models;
using TimeSeriesProcessing.Infrastructure.Data;
using TimeSeriesProcessing.Infrastructure.Repositories;

namespace TimeSeriesProcessing.Infrastructure.Result;

public class ResultRepository : BaseRepository<AggregatedResult>, IResultRepository
{
    public ResultRepository(ApplicationDbContext dbContext) : base(dbContext)
    { }
    
    public async Task<(IReadOnlyList<ResultItemDto> Results, int Count)> GetResultsAsync(ResultFilter filter)
    {
        var query = _dbContext.Results
            .AsNoTracking()
            .ApplyFilter(filter);

        var totalCount = await query.CountAsync();

        var result = await query
            .ApplySorting(filter)
            .ApplyPagination(filter)
            .Select(r => new ResultItemDto
            {
                Id = r.Id,
                FileName = r.FileName,
                AvgExecutionTime = r.AvgExecutionTime,
                AvgValue = r.AvgValue,
                DeltaSeconds = r.DeltaSeconds,
                MinDate = r.MinDate,
                MedianValue = r.MedianValue,
                MinValue = r.MinValue,
                MaxValue = r.MaxValue,
            }).ToListAsync();
        
        return (result, totalCount);
    }
}