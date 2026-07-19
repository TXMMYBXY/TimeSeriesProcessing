using TimeSeriesProcessing.Application.Infrastructure.Value.Dto;

namespace TimeSeriesProcessing.Application.Infrastructure.Validation;

public interface IValueValidator
{
    const int MinRows = 1;
    const int MaxRows = 1000;
    const int MinExecutionTime = 0;
    static readonly DateTime MinDate = new (2000, 1, 1);
    
    ValidationResult CheckDate(DateTime dateTime);
    ValidationResult CheckExecutionTime(int executionTime);
    ValidationResult CheckValue(double value);
    ValidationResult CheckCount(IReadOnlyList<CreateValueDto> valuesDto);
}