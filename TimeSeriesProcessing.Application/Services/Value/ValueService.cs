using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Application.Abstractions.Repositories.Value;
using TimeSeriesProcessing.Application.Abstractions.Validation;
using TimeSeriesProcessing.Application.Services.Result;

namespace TimeSeriesProcessing.Application.Services.Value;

public class ValueService : IValueService
{
    private readonly IValidationService _validationService;
    private readonly IParsingService _parsingService;
    private readonly IResultService _resultService;
    private readonly IValueRepository _valueRepository;

    public ValueService(
        IValidationService validationService,
        IParsingService parsingService,
        IResultService resultService,
        IValueRepository valueRepository)
    {
        _validationService = validationService;
        _parsingService = parsingService;
        _resultService = resultService;
        _valueRepository = valueRepository;
    }
    
    public async Task InsertValuesAsync(string fileName, Stream fileStream)
    {
        var rows = await _parsingService.ParseCsvAsync(fileStream);

        _validationService.Validate(rows);
        
        await _resultService.InsertResultAsync(fileName, rows);
    }
}