namespace TimeSeriesProcessing.Application.Exceptions;

public class CsvValidationException : Exception
{
    public  CsvValidationException(string message)
        : base(message)
    { }
    
    public CsvValidationException(string message, Exception innerException)
        : base(message, innerException)
    { }
}