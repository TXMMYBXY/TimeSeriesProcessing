namespace TimeSeriesProcessing.Application.Services.Value;

public interface IValueService
{
    Task InsertValuesAsync(string fileName, Stream fileStream);
}