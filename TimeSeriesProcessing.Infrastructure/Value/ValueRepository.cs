using Microsoft.EntityFrameworkCore;
using TimeSeriesProcessing.Application.Value;
using TimeSeriesProcessing.Application.Value.Dto;
using TimeSeriesProcessing.Domain.Models;
using TimeSeriesProcessing.Infrastructure.Data;
using TimeSeriesProcessing.Infrastructure.Repositories;

namespace TimeSeriesProcessing.Infrastructure.Value;

public class ValueRepository : BaseRepository<MeasurementValue>, IValueRepository
{
    public ValueRepository(ApplicationDbContext dbContext) : base(dbContext)
    { }

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