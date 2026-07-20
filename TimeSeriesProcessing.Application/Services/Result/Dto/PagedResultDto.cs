namespace TimeSeriesProcessing.Application.Services.Result.Dto;

public class PagedResultDto
{
    public IReadOnlyList<ResultItemDto> Results { get; set; }
    
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}