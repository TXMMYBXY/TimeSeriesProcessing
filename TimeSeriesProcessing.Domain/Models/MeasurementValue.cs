namespace TimeSeriesProcessing.Domain.Models;

public class MeasurementValue
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
    
    public int ResultId { get; set; }
    public AggregatedResult Result { get; set; }
}
