namespace TimeSeriesProcessing.Domain.Models;

public class AggregatedResult
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    
    public int DeltaDate { get; set; }
    public DateTime MinDate { get; set; }
    public double AvgExecutionTime { get; set; }
    public double AvgValue { get; set; }
    public double MedianValue { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }

    public List<MeasurementValue> Values { get; set; } = new();
}