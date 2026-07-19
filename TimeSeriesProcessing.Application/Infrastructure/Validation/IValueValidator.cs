using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;

namespace TimeSeriesProcessing.Application.Infrastructure.Validation;

public interface IValueValidator
{
    void ValidateDate(DateTime dateTime);
    void ValidateExecutionTime(int executionTime);
    void ValidateValue(double value);
    void ValidateCount(IReadOnlyList<CsvRow> valuesDto);
}