using TimeSeriesProcessing.Application.Infrastructure;
using TimeSeriesProcessing.Application.Value.Dto;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Value;

public interface IValueRepository : IBaseRepository<MeasurementValue>
{
    Task<IReadOnlyList<ValueInfoDto>> GetValuesByFileNameAsync(string fileName);
}