namespace TimeSeriesProcessing.Api.Features.Result.Responses;

public class GetResultsResponse
{
    public IReadOnlyList<ResultItemResponse> Results { get; set; }
    
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}