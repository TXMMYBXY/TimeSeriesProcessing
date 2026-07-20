using TimeSeriesProcessing.Application.Abstractions.Parsing;

namespace TimeSeriesProcessing.Application.Abstractions.Validation;

public interface IValidationService
{
    public void Validate(IReadOnlyList<CsvRow> rows);
}