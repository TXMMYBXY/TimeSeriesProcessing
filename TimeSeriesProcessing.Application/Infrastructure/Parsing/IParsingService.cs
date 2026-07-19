using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;

namespace TimeSeriesProcessing.Application.Infrastructure.Parsing;

public interface IParsingService
{
    Task<List<CsvRow>> ParseCsvAsync(Stream stream);
}