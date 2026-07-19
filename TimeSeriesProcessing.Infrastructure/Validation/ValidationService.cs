using TimeSeriesProcessing.Application.Exceptions;
using TimeSeriesProcessing.Application.Infrastructure.Validation;
using TimeSeriesProcessing.Application.Infrastructure.Value.Dto;

namespace TimeSeriesProcessing.Infrastructure.Validation;

public class ValidationService : IValidationService
{
    private readonly IValueValidator _valueValidator;

    public ValidationService(IValueValidator valueValidator)
    {
        _valueValidator = valueValidator;
    }
    
    public void Validate(IReadOnlyList<CsvRowDto> rows)
    {
        _valueValidator.ValidateCount(rows);
        
        for (int i = 0; i < rows.Count; i++)
        {
            ValidateRow(rows[i], i);
        }
    }

    private void ValidateRow(CsvRowDto row, int index)
    {
        try
        {
            _valueValidator.ValidateDate(row.Date);
            _valueValidator.ValidateExecutionTime(row.ExecutionTime);
            _valueValidator.ValidateValue(row.Value);
        }
        catch (ValidationException ex)
        {
            throw new CsvValidationException($"Row {index + 1}: {ex.Message} ", ex);
        }
    }
}