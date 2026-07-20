using Microsoft.AspNetCore.Mvc;
using TimeSeriesProcessing.Api.Features.Value.Requests;
using TimeSeriesProcessing.Api.Features.Value.Responses;
using TimeSeriesProcessing.Application.Services.Value;

namespace TimeSeriesProcessing.Api.Controllers;

[ApiController]
[Route("api/values")]
public class ValueController : ControllerBase
{
    private readonly IValueService _valueService;

    public ValueController(IValueService valueService)
    {
        _valueService = valueService;
    }

    [HttpGet("{fileName}")]
    public async Task<ActionResult<GetValuesResponse>> GetValues([FromRoute] string fileName)
    {
        var resultDto = await _valueService.GetValuesByFileNameAsync(fileName);
        var result = resultDto.MapToGetValuesResponse();
        
        return Ok(result);
    }
    
    [HttpPost("upload")]
    public async Task<ActionResult> UploadFile([FromForm] UploadFileRequest request)
    {
        await using var stream = request.File.OpenReadStream();
        await _valueService.InsertValuesAsync(request.File.FileName, stream);
        
        return CreatedAtAction(nameof(GetValues),new { fileName = request.File.FileName }, null);
    }
}