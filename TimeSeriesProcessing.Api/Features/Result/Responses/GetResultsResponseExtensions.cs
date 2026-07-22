using TimeSeriesProcessing.Application.Services.Result.Dto;

namespace TimeSeriesProcessing.Api.Features.Result.Responses;

public static class GetResultsResponseExtensions
{
    public static GetResultsResponse MapToGetResultsResponse(this PagedResultDto pagedResultDto)
    {
        return new GetResultsResponse
        {
            Results = pagedResultDto.Results.Select(r => new ResultItemResponse
            {
                Id = r.Id,
                FileName = r.FileName,
                DeltaSeconds = r.DeltaSeconds,
                MedianValue = r.MedianValue,
                MinValue = r.MinValue,
                MaxValue = r.MaxValue,
                MinDate = r.MinDate,
                AvgValue = r.AvgValue,
                AvgExecutionTime = r.AvgExecutionTime,
            }).ToList(),
            TotalCount = pagedResultDto.TotalCount,
            PageSize = pagedResultDto.PageSize,
            CurrentPage = pagedResultDto.CurrentPage,
        };
    }
}