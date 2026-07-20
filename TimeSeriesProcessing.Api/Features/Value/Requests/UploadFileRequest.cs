using System.ComponentModel.DataAnnotations;

namespace TimeSeriesProcessing.Api.Features.Value.Requests;

public class UploadFileRequest
{
    [Required]
    public IFormFile File { get; set; }
}