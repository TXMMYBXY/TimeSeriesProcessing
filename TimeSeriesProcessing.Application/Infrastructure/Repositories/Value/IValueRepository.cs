using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;

namespace TimeSeriesProcessing.Application.Infrastructure.Repositories.Value;

public interface IValueRepository
{
    Task<IReadOnlyList<ValueInfoDto>> GetValuesByFileNameAsync(string fileName);
}