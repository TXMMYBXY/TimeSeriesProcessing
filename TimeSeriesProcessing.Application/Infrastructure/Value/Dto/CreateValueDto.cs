namespace TimeSeriesProcessing.Application.Infrastructure.Value.Dto;

public class CreateValueDto
{
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
}