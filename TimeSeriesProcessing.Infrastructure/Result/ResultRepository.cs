using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Application.Infrastructure.Result;
using TimeSeriesProcessing.Application.Infrastructure.Result.Dto;
using TimeSeriesProcessing.Domain.Models;
using TimeSeriesProcessing.Infrastructure.Data;

namespace TimeSeriesProcessing.Infrastructure.Result;

public class ResultRepository : IResultRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ResultRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
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

    public async Task<AggregatedResult?> GetResultEntityByIdAsync(int id)
    {
        return await _dbContext.Results
            .AsNoTracking()
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task InsertResultAsync(AggregatedResult result)
    {
        await _dbContext.Results.AddAsync(result);
    }

    public void DeleteResultAsync(AggregatedResult result)
    {
        _dbContext.Results.Remove(result);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}