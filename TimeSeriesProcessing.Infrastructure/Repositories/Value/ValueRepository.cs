using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value;
using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;
using TimeSeriesProcessing.Infrastructure.Data;

namespace TimeSeriesProcessing.Infrastructure.Repositories.Value;

public class ValueRepository : IValueRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ValueRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ValueInfoDto>> GetValuesByFileNameAsync(string fileName)
    {
        return await _dbContext.Values
            .AsNoTracking()
            .Where(v => v.Result.FileName.Equals(fileName))
            .OrderByDescending(v => v.Date)
            .Take(10)
            .Select(v => new ValueInfoDto
            {
                Id = v.Id,
                Date = v.Date,
                ExecutionTime = v.ExecutionTime,
                Value = v.Value
            })
            .OrderBy(v => v.Date)
            .ToListAsync();
    }
}