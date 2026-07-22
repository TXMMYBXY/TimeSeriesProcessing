using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using TimeSeriesProcessing.Infrastructure.Parsing;
using TimeSeriesProcessing.Infrastructure.Validation;
using TimeSeriesProcessing.Application.Exceptions;

namespace TimeSeriesProcessing.Tests.Parsing;

public class ParsingServiceTests
{
    private static string GetSolutionRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "TimeSeriesProcessing.sln")))
        {
            dir = dir.Parent;
        }

        if (dir == null)
            throw new InvalidOperationException("Solution root not found. Ensure tests are run from repository checkout.");

        return dir.FullName;
    }

    private static string TestsFolder() => Path.Combine(GetSolutionRoot(), "tests");

    [Fact]
    public async Task RunAllTestCsvFiles()
    {
        var testsDir = TestsFolder();
        var csvFiles = Directory.GetFiles(testsDir, "*.csv").OrderBy(x => x).ToList();

        var parser = new ParsingService();
        var validator = new ValidationService(new ValueValidator());

        var failures = new List<string>();

        foreach (var path in csvFiles)
        {
            var name = Path.GetFileName(path).ToLowerInvariant();
            var markedInvalid = name.Contains("invalid") || name.Contains("missing") || name.Contains("empty");

            try
            {
                await using var stream = File.OpenRead(path);
                var rows = await parser.ParseCsvAsync(stream);

                validator.Validate(rows);

                if (markedInvalid)
                {
                    failures.Add($"File '{name}' was marked invalid but parsed and validated successfully.");
                }
            }
            catch (Exception ex)
            {
                var typeName = ex.GetType().Name;
                var isValidation = ex is CsvValidationException || string.Equals(typeName, "ValidationException", StringComparison.OrdinalIgnoreCase);

                if (isValidation)
                {
                    if (!markedInvalid)
                    {
                        failures.Add($"File '{name}' failed to parse or validate but was not marked invalid.");
                    }
                }
                else
                {
                    failures.Add($"File '{name}' threw unexpected exception: {ex.GetType().Name} - {ex.Message}");
                }
            }
        }

        Assert.Empty(failures);
    }
}
