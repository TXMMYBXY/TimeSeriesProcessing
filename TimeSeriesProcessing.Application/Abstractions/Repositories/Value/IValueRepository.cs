using TimeSeriesProcessing.Application.Services.Value.Dto;

namespace TimeSeriesProcessing.Application.Abstractions.Repositories.Value;

public interface IValueRepository
{
    Task<IReadOnlyList<ValueInfoDto>> GetValuesByFileNameAsync(string fileName);
}