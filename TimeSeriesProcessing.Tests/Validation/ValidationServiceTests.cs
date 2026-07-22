using System;
using System.Collections.Generic;
using Xunit;
using TimeSeriesProcessing.Infrastructure.Validation;
using TimeSeriesProcessing.Application.Abstractions.Validation;
using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Application.Exceptions;

namespace TimeSeriesProcessing.Tests.Validation;

public class ValidationServiceTests
{
    private class SpyValueValidator : IValueValidator
    {
        public int CountValidateDateCalls { get; private set; }
        public int CountValidateExecutionTimeCalls { get; private set; }
        public int CountValidateValueCalls { get; private set; }
        public int CountValidateCountCalls { get; private set; }

        public void ValidateDate(DateTime dateTime)
        {
            CountValidateDateCalls++;
        }

        public void ValidateExecutionTime(int executionTime)
        {
            CountValidateExecutionTimeCalls++;
        }

        public void ValidateValue(double value)
        {
            CountValidateValueCalls++;
        }

        public void ValidateCount(IReadOnlyList<CsvRow> valuesDto)
        {
            CountValidateCountCalls++;
        }
    }

    [Fact]
    public void Validate_CallsAllValidatorsPerRow()
    {
        var spy = new SpyValueValidator();
        var service = new ValidationService(spy);

        var rows = new List<CsvRow>
        {
            new CsvRow { Date = DateTime.UtcNow, ExecutionTime = 1, Value = 1 },
            new CsvRow { Date = DateTime.UtcNow, ExecutionTime = 2, Value = 2 },
            new CsvRow { Date = DateTime.UtcNow, ExecutionTime = 3, Value = 3 }
        };

        service.Validate(rows);

        Assert.Equal(1, spy.CountValidateCountCalls);
        Assert.Equal(rows.Count, spy.CountValidateDateCalls);
        Assert.Equal(rows.Count, spy.CountValidateExecutionTimeCalls);
        Assert.Equal(rows.Count, spy.CountValidateValueCalls);
    }

    [Fact]
    public void Validate_WrapsValidationExceptionIntoCsvValidationException_WithRowNumber()
    {
        // Use real ValueValidator to trigger ValidationException on invalid date
        var validator = new ValueValidator();
        var service = new ValidationService(validator);

        var rows = new List<CsvRow>
        {
            new CsvRow { Date = DateTime.UtcNow, ExecutionTime = 1, Value = 1 },
            new CsvRow { Date = new DateTime(1990,1,1, 0,0,0, DateTimeKind.Utc), ExecutionTime = 2, Value = 2 }
        };

        var ex = Assert.Throws<CsvValidationException>(() => service.Validate(rows));
        Assert.Contains("Row 2:", ex.Message);
        Assert.NotNull(ex.InnerException);
        Assert.IsType<ValidationException>(ex.InnerException);
    }

    [Fact]
    public void Validate_PropagatesCountValidationException()
    {
        var validator = new ValueValidator();
        var service = new ValidationService(validator);

        var rows = new List<CsvRow>(); // empty

        var ex = Assert.Throws<ValidationException>(() => service.Validate(rows));
        Assert.Equal("Invalid count of rows.", ex.Message);
    }
}
