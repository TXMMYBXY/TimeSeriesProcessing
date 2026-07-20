namespace TimeSeriesProcessing.Api.Features.Value.Responses;

public class ValueItemResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
}