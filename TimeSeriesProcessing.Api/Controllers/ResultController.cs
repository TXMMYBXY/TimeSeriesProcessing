using Microsoft.AspNetCore.Mvc;
using TimeSeriesProcessing.Api.Features.Result.Requests;
using TimeSeriesProcessing.Api.Features.Result.Responses;
using TimeSeriesProcessing.Application.Services.Result;

namespace TimeSeriesProcessing.Api.Controllers;

[ApiController]
[Route("api/results")]
public class ResultController : ControllerBase
{
    private readonly IResultService _resultService;

    public ResultController(IResultService resultService)
    {
        _resultService = resultService;
    }
    
    [HttpGet]
    public async Task<ActionResult<GetResultsResponse>> GetResults([FromQuery] GetResultsRequest request)
    {
        var resultDto = await _resultService.GetResultsAsync(request.MapToResultFilter());
        var result = resultDto.MapToGetResultsResponse();
        
        return Ok(result);
    }
}