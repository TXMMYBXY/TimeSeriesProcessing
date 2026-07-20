using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Application.Abstractions.Repositories.Result;
using TimeSeriesProcessing.Application.Services.Result.Dto;

namespace TimeSeriesProcessing.Application.Services.Result;

public interface IResultService
{
    Task InsertResultAsync(string fileName, IReadOnlyList<CsvRow> rows);
}