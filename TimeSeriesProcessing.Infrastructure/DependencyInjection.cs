using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSeriesProcessing.Application.Infrastructure.Result;
using TimeSeriesProcessing.Application.Infrastructure.Value;
using TimeSeriesProcessing.Infrastructure.Data;
using TimeSeriesProcessing.Infrastructure.Result;
using TimeSeriesProcessing.Infrastructure.Value;

namespace TimeSeriesProcessing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IValueRepository, ValueRepository>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}