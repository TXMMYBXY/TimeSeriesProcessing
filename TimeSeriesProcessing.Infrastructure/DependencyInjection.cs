using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSeriesProcessing.Application.Abstractions.Parsing;
using TimeSeriesProcessing.Application.Abstractions.Repositories.Result;
using TimeSeriesProcessing.Application.Abstractions.Repositories.Value;
using TimeSeriesProcessing.Application.Abstractions.Validation;
using TimeSeriesProcessing.Application.Services.Result;
using TimeSeriesProcessing.Application.Services.Value;
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
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<IValueService, ValueService>();
        
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}