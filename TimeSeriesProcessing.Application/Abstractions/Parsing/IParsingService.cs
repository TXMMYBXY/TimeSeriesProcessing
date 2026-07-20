namespace TimeSeriesProcessing.Application.Abstractions.Parsing;

public interface IParsingService
{
    Task<List<CsvRow>> ParseCsvAsync(Stream stream);
}