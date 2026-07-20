using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Application.Abstractions.Repositories;
using TimeSeriesProcessing.Application.Services.Result;
using TimeSeriesProcessing.Application.Services.Result.Dto;
using TimeSeriesProcessing.Domain.Models;
using TimeSeriesProcessing.Infrastructure.Data;

namespace TimeSeriesProcessing.Infrastructure.Repositories.Result;

public class ResultRepository : IResultRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ResultRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<(IReadOnlyList<ResultItemDto> Results, int TotalCount)> GetResultsAsync(ResultFilter filter)
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

    public async Task<AggregatedResult?> GetByFileNameAsync(string fileName)
    {
        return await _dbContext.Results
            .Where(r => r.FileName == fileName)
            .FirstOrDefaultAsync();
    }

    public async Task InsertResultAsync(AggregatedResult result)
    {
        await _dbContext.Results.AddAsync(result);
    }

    public void DeleteResult(AggregatedResult result)
    {
        _dbContext.Results.Remove(result);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}