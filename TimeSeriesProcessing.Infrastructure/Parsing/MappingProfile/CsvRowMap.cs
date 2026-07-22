using CsvHelper.Configuration;
using TimeSeriesProcessing.Application.Abstractions.Parsing;

namespace TimeSeriesProcessing.Infrastructure.Parsing.MappingProfile;

public sealed class CsvRowMap : ClassMap<CsvRow>
{
    public CsvRowMap()
    {
         Map(row => row.Date).Name("Date").TypeConverter<CustomDateTimeConverter>();
         Map(row => row.ExecutionTime).Name("ExecutionTime");
         Map(row => row.Value).Name("Value");
    }
}