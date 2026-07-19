using TimeSeriesProcessing.Application.Infrastructure.Result;
using TimeSeriesProcessing.Domain.Models;

namespace TimeSeriesProcessing.Infrastructure.Result;

public static class ResultQueryableExtensions
{
    public static IQueryable<AggregatedResult> ApplyFilter(this IQueryable<AggregatedResult> query, ResultFilter filter)
    {
        if(!string.IsNullOrEmpty(filter.FileName))
            query = query.Where(r => r.FileName.Contains(filter.FileName));
        
        if(filter.AvgExecutionTimeFrom.HasValue)
            query = query.Where(r => r.AvgExecutionTime >= filter.AvgExecutionTimeFrom.Value);
        
        if(filter.AvgExecutionTimeTo.HasValue)
            query = query.Where(r => r.AvgExecutionTime <= filter.AvgExecutionTimeTo.Value);
        
        if(filter.AvgValueFrom.HasValue)
            query = query.Where(r => r.AvgValue >= filter.AvgValueFrom.Value);
        
        if(filter.AvgValueTo.HasValue)
            query = query.Where(r => r.AvgValue <= filter.AvgValueTo.Value);
        
        if(filter.DateTimeFrom.HasValue)
            query = query.Where(r => r.MinDate >= filter.DateTimeFrom.Value);

        if (filter.DateTimeTo.HasValue)
            query = query.Where(r => r.MinDate<= filter.DateTimeTo.Value);
        
        return query;
    }

    public static IQueryable<AggregatedResult> ApplySorting(this IQueryable<AggregatedResult> query, 
        ResultFilter filter)
    {
        return filter.OrderBy.Value switch
        {
            ResultSortField.FileName =>
                filter.Descending
                    ? query.OrderByDescending(r => r.FileName)
                    : query.OrderBy(r => r.FileName),

            ResultSortField.DeltaSeconds =>
                filter.Descending
                    ? query.OrderByDescending(r => r.DeltaSeconds)
                    : query.OrderBy(r => r.DeltaSeconds),

            ResultSortField.MinDate =>
                filter.Descending
                    ? query.OrderByDescending(r => r.MinDate)
                    : query.OrderBy(r => r.MinDate),

            ResultSortField.AvgExecutionTime =>
                filter.Descending
                    ? query.OrderByDescending(r => r.AvgExecutionTime)
                    : query.OrderBy(r => r.AvgExecutionTime),

            ResultSortField.AvgValue =>
                filter.Descending
                    ? query.OrderByDescending(r => r.AvgValue)
                    : query.OrderBy(r => r.AvgValue),

            ResultSortField.MedianValue =>
                filter.Descending
                    ? query.OrderByDescending(r => r.MedianValue)
                    : query.OrderBy(r => r.MedianValue),

            ResultSortField.MinValue =>
                filter.Descending
                    ? query.OrderByDescending(r => r.MinValue)
                    : query.OrderBy(r => r.MinValue),

            ResultSortField.MaxValue =>
                filter.Descending
                    ? query.OrderByDescending(r => r.MaxValue)
                    : query.OrderBy(r => r.MaxValue),

            _ => filter.Descending
                ? query.OrderByDescending(r => r.Id)
                : query.OrderBy(r => r.Id)
        };
    }

    public static IQueryable<AggregatedResult> ApplyPagination(this IQueryable<AggregatedResult> query, 
        ResultFilter filter)
    {
        if (filter.PageSize.HasValue && filter.PageNumber.HasValue)
        {
            query = query
                .Skip((filter.PageNumber.Value - 1) * filter.PageSize.Value)
                .Take(filter.PageSize.Value);
        }
        
        return query;
    }
}