using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSeriesProcessing.Application.Infrastructure.Parsing;
using TimeSeriesProcessing.Application.Infrastructure.Repositories.Result;
using TimeSeriesProcessing.Application.Infrastructure.Repositories.Value;
using TimeSeriesProcessing.Application.Infrastructure.Validation;
using TimeSeriesProcessing.Infrastructure.Data;
using TimeSeriesProcessing.Infrastructure.Parsing;
using TimeSeriesProcessing.Infrastructure.Repositories.Result;
using TimeSeriesProcessing.Infrastructure.Repositories.Value;
using TimeSeriesProcessing.Infrastructure.Validation;

namespace TimeSeriesProcessing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IValueRepository, ValueRepository>();
        services.AddScoped<IValueValidator, ValueValidator>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IParsingService, ParsingService>();
        
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}