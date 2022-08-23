using Core.CrossCuttingConcerns.Exceptions;
using rentACar.Application;
using rentACar.Persistence;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
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
app.UseConfigureCustomExceptionMiddleware();
app.MapControllers();

app.Run();
