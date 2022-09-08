using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Filters;
using Prometheus;
using rentACar.Application;
using rentACar.Persistence;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

if (app.Environment.IsProduction())
    app.UseConfigureCustomExceptionMiddleware();


app.UseMetricServer();
app.UseHttpMetrics();
app.MapMetrics();
app.MapControllers();

app.Run();
