using TimeSeriesProcessing.Application.Services.Result;

namespace TimeSeriesProcessing.Api.Features.Result.Requests;

public static class GetResultsRequestExtensions
{
    public static ResultFilter MapToResultFilter(this GetResultsRequest request)
    {
        return new ResultFilter
        {
            OrderBy = request.OrderBy,
            Descending = request.Descending,
            FileName = request.FileName,
            AvgExecutionTimeFrom = request.AvgExecutionTimeFrom,
            AvgExecutionTimeTo = request.AvgExecutionTimeTo,
            AvgValueFrom = request.AvgValueFrom,
            AvgValueTo = request.AvgValueTo,
            DateTimeFrom = request.DateTimeFrom,
            DateTimeTo = request.DateTimeTo,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        };
    }
}