using TimeSeriesProcessing.Application.Services.Value.Dto;

namespace TimeSeriesProcessing.Api.Features.Value.Responses;

public static class GetValuesResponseExtensions
{
    public static GetValuesResponse MapToGetValuesResponse(this GetValuesDto valuesDto)
    {
        return new GetValuesResponse
        {
            Values = valuesDto.Values.Select(v => new ValueItemResponse
            {
                Id = v.Id,
                Date = v.Date,
                ExecutionTime = v.ExecutionTime,
                Value = v.Value,
            }).ToList()
        };
    }
}