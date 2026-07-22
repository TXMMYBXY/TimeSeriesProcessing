namespace TimeSeriesProcessing.Api.Features.Value.Responses;

public class GetValuesResponse
{
    public IReadOnlyList<ValueItemResponse> Values { get; set; }
}
