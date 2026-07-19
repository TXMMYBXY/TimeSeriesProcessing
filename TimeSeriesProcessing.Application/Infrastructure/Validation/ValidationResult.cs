namespace TimeSeriesProcessing.Application.Infrastructure.Validation;

public class ValidationResult
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
}