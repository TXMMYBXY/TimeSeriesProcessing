using Microsoft.OpenApi;
using TimeSeriesProcessing.Api.Middleware;
using TimeSeriesProcessing.Infrastructure;

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

app.Run();
