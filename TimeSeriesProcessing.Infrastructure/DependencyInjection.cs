using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSeriesProcessing.Application.Infrastructure;
using TimeSeriesProcessing.Application.Result;
using TimeSeriesProcessing.Infrastructure.Data;
using TimeSeriesProcessing.Infrastructure.Repositories;
using TimeSeriesProcessing.Infrastructure.Result;

namespace TimeSeriesProcessing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IResultRepository, ResultRepository>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}