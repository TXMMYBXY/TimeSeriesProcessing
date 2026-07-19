using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;

namespace TimeSeriesProcessing.Application.Infrastructure.Validation;

public interface IValidationService
{
    public void Validate(IReadOnlyList<CsvRow> rows);
}