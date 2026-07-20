namespace TimeSeriesProcessing.Application.Services.Result;

public class ResultFilter
{
    public ResultSortField? OrderBy { get; set; }
    public bool Descending { get; set; }
    
    public string? FileName { get; set; }
    public DateTime? DateTimeFrom { get; set; }
    public DateTime? DateTimeTo { get; set; }
    public double? AvgValueFrom { get; set; }
    public double? AvgValueTo { get; set; }
    public double? AvgExecutionTimeFrom { get; set; }
    public double? AvgExecutionTimeTo { get; set; }
    
    public int? PageSize { get; set;}
    public int? PageNumber { get; set; }
}