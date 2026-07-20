using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TimeSeriesProcessing.Application.Exceptions;
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
            MissingFieldFound = args =>
                throw new CsvValidationException(
                    $"Row {args.Context.Parser.Row}: Missing field '{args.HeaderNames?.ElementAtOrDefault(args.Index)}'"),
            TrimOptions = TrimOptions.Trim,
            BadDataFound = args =>
                throw new CsvValidationException(
                    $"Row {args.Context.Parser.Row}: Bad data")
        };
        try
        {
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
        catch (TypeConverterException ex)
        {
            throw new CsvValidationException($"Row {ex.Context.Parser.Row}: Invalid data format", ex);
        }
        catch (HeaderValidationException ex)
        {
            throw new CsvValidationException("Invalid CSV header", ex);
        }
    }
}