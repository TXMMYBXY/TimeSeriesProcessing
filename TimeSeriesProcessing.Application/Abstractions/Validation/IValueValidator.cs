using TimeSeriesProcessing.Application.Abstractions.Parsing;

namespace TimeSeriesProcessing.Application.Abstractions.Validation;

public interface IValueValidator
{
    void ValidateDate(DateTime dateTime);
    void ValidateExecutionTime(int executionTime);
    void ValidateValue(double value);
    void ValidateCount(IReadOnlyList<CsvRow> valuesDto);
}