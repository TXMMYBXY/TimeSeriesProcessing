using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using TimeSeriesProcessing.Api.Middleware;
using TimeSeriesProcessing.Infrastructure;
using TimeSeriesProcessing.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TimeSeriesProcessing API",
        Version = "v1",
        Description = "The Time Series Processing API"
    });
});

var app = builder.Build();

app.UseErrorHandling();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/ping", () => "pong");

await ApplyMigrationsWithRetryAsync(app);

app.Run();

static async Task ApplyMigrationsWithRetryAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    const int maxRetries = 10;

    for (var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            logger.LogInformation("Applying migrations. Attempt {Attempt}/{MaxRetries}", attempt, maxRetries);
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrations applied successfully");
            return;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to apply migrations on attempt {Attempt}/{MaxRetries}", attempt, maxRetries);

            if (attempt == maxRetries)
                throw;

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}