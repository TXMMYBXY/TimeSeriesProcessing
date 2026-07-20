namespace TimeSeriesProcessing.Application.Services.Value.Dto;

public class GetValuesDto
{
    public IReadOnlyList<ValueItemDto> Values { get; set; }
}