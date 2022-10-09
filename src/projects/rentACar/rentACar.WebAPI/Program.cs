using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Filters;
using Core.CrossCuttingConcerns.Logging.DbLog;
using Core.Security;
using Microsoft.Extensions.Options;
using Prometheus;
using rentACar.Application;
using rentACar.Persistence;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddSecurityServices();
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<LogDatabaseSettings>(builder.Configuration.GetSection("LogDatabaseSettings"));
builder.Services.AddSingleton<ILogDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<LogDatabaseSettings>>().Value;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//if (app.Environment.IsProduction())
app.UseConfigureCustomExceptionMiddleware();


app.UseMetricServer();
app.UseHttpMetrics();
app.UseRequestResponseLoggingMiddleware();
app.MapMetrics();
app.MapControllers();

app.Run();
