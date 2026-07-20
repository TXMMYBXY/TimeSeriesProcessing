namespace TimeSeriesProcessing.Application.Services.Value.Dto;

public class ValueItemDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
}