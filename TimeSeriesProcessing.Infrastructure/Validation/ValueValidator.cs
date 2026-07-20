using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Application.Abstractions.Validation;
using TimeSeriesProcessing.Application.Exceptions;

namespace TimeSeriesProcessing.Infrastructure.Validation;

public class ValueValidator : IValueValidator
{
    private const int MinRows = 1;
    private const int MaxRows = 10000;
    private const int MinExecutionTime = 0;
    private static readonly DateTime MinDate = 
        new (2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    
    public void ValidateDate(DateTime dateTime)
    {
        var isValid = dateTime >= MinDate && dateTime <= DateTime.UtcNow;
        
        if (!isValid)
        {
            throw new ValidationException("Date is invalid.");
        }
    }

    public void ValidateExecutionTime(int executionTime)
    {
        var isValid = executionTime >= MinExecutionTime;
        
        if (!isValid)
        {
            throw new ValidationException("Execution time is invalid.");
        }
    }

    public void ValidateValue(double value)
    {
        var isValid = value >= 0;
        
        if (!isValid)
        {
            throw new ValidationException("Value is invalid.");
        }
    }

    public void ValidateCount(IReadOnlyList<CsvRow> valuesDto)
    {
        var isValid = valuesDto.Count >= MinRows && valuesDto.Count <= MaxRows;
        
        if (!isValid)
        {
            throw new ValidationException("Invalid count of rows.");
        }
    }
}