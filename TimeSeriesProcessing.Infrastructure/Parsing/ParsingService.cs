using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using TimeSeriesProcessing.Application.Infrastructure.Parsing;
using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;
using TimeSeriesProcessing.Infrastructure.Parsing.MappingProfile;

namespace TimeSeriesProcessing.Infrastructure.Parsing;

public class ParsingService : IParsingService
{
    public async Task<List<CsvRow>> ParseCsvAsync(Stream stream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null,
            BadDataFound = null
        };
        
        using var reader = new StreamReader(stream);
        using var csvReader = new CsvReader(reader, config);

        csvReader.Context.RegisterClassMap<CsvRowMap>();
        
        var rows = new List<CsvRow>();

        await foreach (var record in csvReader.GetRecordsAsync<CsvRow>())
        {
            rows.Add(record);
        }

        return rows;
    }
}