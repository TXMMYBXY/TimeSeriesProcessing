namespace TimeSeriesProcessing.Application.Infrastructure.Value.Dto;

public class CsvRowDto
{
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
}