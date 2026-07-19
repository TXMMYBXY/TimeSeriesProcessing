using TimeSeriesProcessing.Application.Infrastructure.Value.Dto;

namespace TimeSeriesProcessing.Application.Infrastructure.Validation;

public interface IValidationService
{
    public void Validate(IReadOnlyList<CsvRowDto> rows);
}