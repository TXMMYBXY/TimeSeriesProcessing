using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Mappings;

public static class CsvRowMappingExtensions
{
    public static MeasurementValue ToEntity(this CsvRow row)
    {
        return new MeasurementValue
        {
            Date = row.Date,
            ExecutionTime = row.ExecutionTime,
            Value = row.Value,
        };
    }
}