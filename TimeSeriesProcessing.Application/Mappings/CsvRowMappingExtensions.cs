using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Mappings;

public static class CsvRowMappingExtensions
{
    public static MeasurementValue ToEntity(this CsvRow row)
    {
        return new MeasurementValue
        {
            Date = DateTime.SpecifyKind(row.Date, DateTimeKind.Utc),
            ExecutionTime = row.ExecutionTime,
            Value = row.Value,
        };
    }
}