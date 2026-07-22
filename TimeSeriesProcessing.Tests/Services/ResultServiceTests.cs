using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSeriesProcessing.Application.Services.Result;
using TimeSeriesProcessing.Application.Services.Result.Dto;
using TimeSeriesProcessing.Application.Abstractions.Repositories;
using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Domain.Models;
using Xunit;

namespace TimeSeriesProcessing.Tests.Services;

public class ResultServiceTests
{
    private class FakeResultRepository : IResultRepository
    {
        public AggregatedResult? Inserted { get; private set; }
        public AggregatedResult? Deleted { get; private set; }
        public AggregatedResult? Existing { get; set; }
        public (IReadOnlyList<ResultItemDto> Results, int TotalCount) ResultsTuple { get; set; } = (new List<ResultItemDto>(), 0);

        public Task<(IReadOnlyList<ResultItemDto> Results, int TotalCount)> GetResultsAsync(ResultFilter filter)
        {
            return Task.FromResult(ResultsTuple);
        }

        public Task<AggregatedResult?> GetByFileNameAsync(string fileName)
        {
            return Task.FromResult(Existing);
        }

        public Task InsertResultAsync(AggregatedResult result)
        {
            Inserted = result;
            return Task.CompletedTask;
        }

        public void DeleteResult(AggregatedResult result)
        {
            Deleted = result;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task InsertResultAsync_ComputesAggregatesAndInserts()
    {
        var repo = new FakeResultRepository();
        var service = new ResultService(repo);

        var rows = new List<CsvRow>
        {
            new() { Date = new DateTime(2023,1,1,0,0,0, DateTimeKind.Utc), ExecutionTime = 10, Value = 1 },
            new() { Date = new DateTime(2023,1,1,0,1,0, DateTimeKind.Utc), ExecutionTime = 20, Value = 3 },
            new() { Date = new DateTime(2023,1,1,0,2,0, DateTimeKind.Utc), ExecutionTime = 30, Value = 2 }
        };

        await service.InsertResultAsync("file.csv", rows);

        Assert.NotNull(repo.Inserted);
        var inserted = repo.Inserted!;

        Assert.Equal("file.csv", inserted.FileName);
        Assert.Equal((int)(rows.Max(r => r.Date) - rows.Min(r => r.Date)).TotalSeconds, inserted.DeltaSeconds);
        Assert.Equal(rows.Min(r => r.Value), inserted.MinValue);
        Assert.Equal(rows.Max(r => r.Value), inserted.MaxValue);
        Assert.Equal(rows.Average(r => r.Value), inserted.AvgValue, 6);
        Assert.Equal(rows.Average(r => r.ExecutionTime), inserted.AvgExecutionTime, 6);
        // Median of [1,2,3] => 2
        Assert.Equal(2, inserted.MedianValue);
        Assert.Equal(rows.Count, inserted.Values.Count);
    }

    [Fact]
    public async Task InsertResultAsync_ReplacesExistingResult_WhenExists()
    {
        var repo = new FakeResultRepository();
        repo.Existing = new AggregatedResult { FileName = "file.csv" };
        var service = new ResultService(repo);

        var rows = new List<CsvRow>
        {
            new() { Date = DateTime.UtcNow, ExecutionTime = 1, Value = 5 }
        };

        await service.InsertResultAsync("file.csv", rows);

        Assert.NotNull(repo.Deleted);
        Assert.Equal("file.csv", repo.Deleted!.FileName);
        Assert.NotNull(repo.Inserted);
    }

    [Fact]
    public async Task GetResultsAsync_ReturnsPagedResultDto()
    {
        var repo = new FakeResultRepository();
        var items = new List<ResultItemDto>
        {
            new() { FileName = "a.csv", AvgValue = 1 },
            new() { FileName = "b.csv", AvgValue = 2 }
        };

        repo.ResultsTuple = (items, items.Count);

        var service = new ResultService(repo);

        var filter = new ResultFilter { PageNumber = 1, PageSize = 10 };
        var result = await service.GetResultsAsync(filter);

        Assert.Equal(items.Count, result.TotalCount);
        Assert.Equal(items.Count, result.Results.Count);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(1, result.CurrentPage);
    }
}
