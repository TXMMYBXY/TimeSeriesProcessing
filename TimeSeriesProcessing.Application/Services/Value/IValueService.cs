using TimeSeriesProcessing.Application.Services.Value.Dto;

namespace TimeSeriesProcessing.Application.Services.Value;

public interface IValueService
{
    Task InsertValuesAsync(string fileName, Stream fileStream);
    Task<GetValuesDto> GetValuesByFileNameAsync(string fileName);
}