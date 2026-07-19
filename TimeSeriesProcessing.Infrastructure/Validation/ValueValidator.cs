using TimeSeriesProcessing.Application.Infrastructure.Validation;
using TimeSeriesProcessing.Application.Infrastructure.Value.Dto;

namespace TimeSeriesProcessing.Infrastructure.Validation;

public class ValueValidator : IValueValidator
{
    public ValidationResult CheckDate(DateTime dateTime)
    {
        var isValid = dateTime >= IValueValidator.MinDate && dateTime <= DateTime.UtcNow;

        return new ValidationResult
        {
            IsValid = isValid,
            Message = isValid ? null : "Date is invalid."
        };
    }

    public ValidationResult CheckExecutionTime(int executionTime)
    {
        var isValid = executionTime > IValueValidator.MinExecutionTime;
        
        return new ValidationResult
        {
            IsValid = isValid,
            Message = isValid ? null : "Execution time is invalid."
        };
    }

    public ValidationResult CheckValue(double value)
    {
        var isValid = double.IsPositive(value);

        return new ValidationResult
        {
            IsValid = isValid,
            Message = isValid ? null : "Value is invalid."
        };
    }

    public ValidationResult CheckCount(IReadOnlyList<CreateValueDto> valuesDto)
    {
        var isValid = valuesDto.Count > IValueValidator.MinRows && valuesDto.Count < IValueValidator.MaxRows;

        return new ValidationResult
        {
            IsValid = isValid,
            Message = isValid ? null : "Count is invalid."
        };
    }
}