using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Application.Abstractions.Repositories;
using TimeSeriesProcessing.Application.Exceptions;
using TimeSeriesProcessing.Application.Mappings;
using TimeSeriesProcessing.Application.Services.Result.Dto;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Application.Services.Result;

public class ResultService : IResultService
{
    private readonly IResultRepository _resultRepository;

    public ResultService(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }
    
    public async Task InsertResultAsync(string fileName, IReadOnlyList<CsvRow> rows)
    {
        if (rows == null || rows.Count == 0) 
        {
            throw new CsvValidationException("No rows were provided");
        }
        
        var args = CalculateResults(rows);
        var aggregatedResult = new AggregatedResult
        {
            FileName = fileName,
            AvgExecutionTime = args.AvgExecutionTime,
            AvgValue = args.AvgValue,
            DeltaSeconds = (int)(args.MaxDate - args.MinDate).TotalSeconds,
            MinDate = args.MinDate,
            MinValue = args.MinValue,
            MaxValue = args.MaxValue,
            MedianValue = MedianValue(rows),
            Values = rows.Select(r => r.ToEntity()).ToList()
        };
        
        var oldResult = await _resultRepository.GetByFileNameAsync(fileName);

        if (oldResult != null)
        {
            _resultRepository.DeleteResult(oldResult);
        }
        
        await _resultRepository.InsertResultAsync(aggregatedResult);
        await _resultRepository.SaveChangesAsync();
    }

    public async Task<PagedResultDto> GetResultsAsync(ResultFilter filter)
    {
        var resultsTuple = await _resultRepository.GetResultsAsync(filter);

        return new PagedResultDto
        {
            Results = resultsTuple.Results,
            TotalCount = resultsTuple.TotalCount,
            PageSize = filter.PageSize ?? resultsTuple.Results.Count,
            CurrentPage = filter.PageNumber ?? 1
        };
    }

    private static CalculateResultArgs CalculateResults(IReadOnlyList<CsvRow> rows)
    {
        var args = new CalculateResultArgs();
        var sumExecutionTime = 0.0;
        var sumValue = 0.0;
        var count = rows.Count;
            
        foreach (var row in rows)
        {
            if (args.MinExecutionTime > row.ExecutionTime)
            {
                args.MinExecutionTime = row.ExecutionTime;
            }

            if (args.MaxExecutionTime < row.ExecutionTime)
            {
                args.MaxExecutionTime = row.ExecutionTime;
            }
            
            if (args.MinDate > row.Date)
            {
                args.MinDate = row.Date;
            }

            if (args.MaxDate < row.Date)
            {
                args.MaxDate = row.Date;
            }

            if (args.MinValue > row.Value)
            {
                args.MinValue = row.Value;
            }

            if (args.MaxValue < row.Value)
            {
                args.MaxValue = row.Value;
            }

            sumValue += row.Value;
            sumExecutionTime += row.ExecutionTime;
        }
        
        args.AvgExecutionTime = sumExecutionTime / count;
        args.AvgValue = sumValue / count;

        return args;
    }

    private static double MedianValue(IReadOnlyList<CsvRow> rows)
    {
        var sortedValues = rows.Select(r => r.Value).OrderBy(v => v).ToList();
        var count = sortedValues.Count;
        
        if (count == 0)
        {
            return 0;
        }

        if (count % 2 != 0)
        {
            return sortedValues[count / 2];
        }
    
        return (sortedValues[(count / 2) - 1] + sortedValues[count / 2]) / 2.0;
    }
}