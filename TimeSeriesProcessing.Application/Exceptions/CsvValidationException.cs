namespace TimeSeriesProcessing.Application.Exceptions;

public class CsvValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }
    
    public CsvValidationException(IReadOnlyList<string> errors)
        : base($"CSV validation failed: {errors.Count} error(s)")
    {
        Errors = errors;
    }
    
    public CsvValidationException(string error)
        : this(new List<string> { error })
    {
    }
}