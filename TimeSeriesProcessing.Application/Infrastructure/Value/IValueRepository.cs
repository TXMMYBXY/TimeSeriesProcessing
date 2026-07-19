using TimeSeriesProcessing.Application.Infrastructure.Value.Dto;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Infrastructure.Value;

public interface IValueRepository
{
    Task<IReadOnlyList<ValueInfoDto>> GetValuesByFileNameAsync(string fileName);
}