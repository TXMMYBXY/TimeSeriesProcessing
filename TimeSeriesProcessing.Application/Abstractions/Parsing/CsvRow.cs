namespace TimeSeriesProcessing.Application.Abstractions.Parsing;

public class CsvRow
{
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
}