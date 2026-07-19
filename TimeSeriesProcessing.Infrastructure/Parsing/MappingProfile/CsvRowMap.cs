using CsvHelper.Configuration;
using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;

namespace TimeSeriesProcessing.Infrastructure.Parsing.MappingProfile;

public sealed class CsvRowMap : ClassMap<CsvRow>
{
    public CsvRowMap()
    {
         Map(row => row.Date).Name("Date");
         Map(row => row.ExecutionTime).Name("ExecutionTime");
         Map(row => row.Value).Name("Value");
    }
}