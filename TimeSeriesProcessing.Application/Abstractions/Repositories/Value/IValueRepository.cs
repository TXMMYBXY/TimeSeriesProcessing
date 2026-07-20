using TimeSeriesProcessing.Application.Services.Value.Dto;

namespace TimeSeriesProcessing.Application.Abstractions.Repositories.Value;

public interface IValueRepository
{
    Task<IReadOnlyList<ValueItemDto>> GetValuesByFileNameAsync(string fileName);
}