namespace TimeSeriesProcessing.Application.Infrastructure.Result.Dto;

public class ResultItemDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public int DeltaSeconds { get; set; }
    public DateTime MinDate { get; set; }
    public double AvgExecutionTime { get; set; }
    public double AvgValue { get; set; }
    public double MedianValue { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
}