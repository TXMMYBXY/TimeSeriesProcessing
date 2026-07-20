namespace TimeSeriesProcessing.Application.Services.Result;

public class CalculateResultParams
{
    public double AvgExecutionTime { get; set; }
    public double AvgValue { get; set; }
    public int MinExecutionTime { get; set; } = int.MaxValue;
    public int MaxExecutionTime { get; set; } = int.MinValue;
    public double MinValue { get; set; } = double.MaxValue;
    public double MaxValue { get; set; } = double.MinValue;
    public DateTime MinDate { get; set; } = DateTime.MaxValue;
    public DateTime MaxDate { get; set; } = DateTime.MinValue;
}