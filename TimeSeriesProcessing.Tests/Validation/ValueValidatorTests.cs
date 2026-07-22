using System;
using System.Collections.Generic;
using TimeSeriesProcessing.Infrastructure.Validation;
using TimeSeriesProcessing.Application.Exceptions;
using Xunit;

namespace TimeSeriesProcessing.Tests.Validation;

public class ValueValidatorTests
{
    private readonly ValueValidator _validator = new();

    [Fact]
    public void ValidateDate_AllowsValidDate()
    {
        var dt = DateTime.UtcNow;
        _validator.ValidateDate(dt);
    }

    [Fact]
    public void ValidateDate_ThrowsForTooOldDate()
    {
        var dt = new DateTime(1990,1,1, 0,0,0, DateTimeKind.Utc);
        var ex = Assert.Throws<ValidationException>(() => _validator.ValidateDate(dt));
        Assert.Equal("Date is invalid.", ex.Message);
    }

    [Fact]
    public void ValidateDate_ThrowsForFutureDate()
    {
        var dt = DateTime.UtcNow.AddHours(1);
        var ex = Assert.Throws<ValidationException>(() => _validator.ValidateDate(dt));
        Assert.Equal("Date is invalid.", ex.Message);
    }

    [Fact]
    public void ValidateExecutionTime_ValidAndInvalid()
    {
        _validator.ValidateExecutionTime(0);
        _validator.ValidateExecutionTime(100);
        var ex = Assert.Throws<ValidationException>(() => _validator.ValidateExecutionTime(-1));
        Assert.Equal("Execution time is invalid.", ex.Message);
    }

    [Fact]
    public void ValidateValue_ValidAndInvalid()
    {
        _validator.ValidateValue(0);
        _validator.ValidateValue(12.34);
        var ex = Assert.Throws<ValidationException>(() => _validator.ValidateValue(-0.1));
        Assert.Equal("Value is invalid.", ex.Message);
    }

    [Fact]
    public void ValidateCount_ThrowsForEmptyOrTooLarge()
    {
        var empty = new List<TimeSeriesProcessing.Application.Abstractions.Parsing.CsvRow>();
        var ex = Assert.Throws<ValidationException>(() => _validator.ValidateCount(empty));
        Assert.Equal("Invalid count of rows.", ex.Message);

        var big = new List<TimeSeriesProcessing.Application.Abstractions.Parsing.CsvRow>(new TimeSeriesProcessing.Application.Abstractions.Parsing.CsvRow[10001]);
        ex = Assert.Throws<ValidationException>(() => _validator.ValidateCount(big));
        Assert.Equal("Invalid count of rows.", ex.Message);
    }
}
