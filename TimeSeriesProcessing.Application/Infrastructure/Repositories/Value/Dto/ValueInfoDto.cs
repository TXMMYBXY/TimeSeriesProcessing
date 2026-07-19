namespace TimeSeriesProcessing.Application.Infrastructure.Repositories.Value.Dto;

public class ValueInfoDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public double Value { get; set; }
}