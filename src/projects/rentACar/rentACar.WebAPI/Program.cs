using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Filters;
using Core.CrossCuttingConcerns.Logging.DbLog.Mongo;
using Core.Security;
using Core.Security.ApplicationSecurity.Filters;
using Core.Security.Encryption;
using Core.Security.JWT;
using Core.Utilities.Messages;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using rentACar.Application;
using rentACar.Infrastructure;
using rentACar.Persistence;
using rentACar.Persistence.Modules;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(new RequestLimitAttribute("RequestLimit")))
 .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
 .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddSecurityServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();


builder.Services.Configure<LogDatabaseSettings>(builder.Configuration.GetSection("LogDatabaseSettings"));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepositoryModule()));

builder.Services.AddSingleton<ILogDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<LogDatabaseSettings>>().Value;
});

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

builder.Services.AddSwaggerGen(opt =>
{
    opt.CustomSchemaIds(type => type.ToString());
    opt.SwaggerDoc(SwaggerMessages.Version, new OpenApiInfo
    {
        Title = SwaggerMessages.Title,
        Version = SwaggerMessages.Version,
        Description = SwaggerMessages.Description,
    });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(x =>
    {
        x.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
        options.DefaultModelExpandDepth(-1);
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture v1");
    });
}

//if (app.Environment.IsProduction())
app.UseConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/job", new DashboardOptions
{
    DashboardTitle = "Clean Architecture Hangfire DashBoard",
    AppPath = "/Home/HangfireAbout",

});

app.UseHangfireServer(new BackgroundJobServerOptions
{
    SchedulePollingInterval = TimeSpan.FromSeconds(30),

    WorkerCount = Environment.ProcessorCount * 5
});
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 7 });


app.UseMetricServer();
app.UseHttpMetrics();
app.MapMetrics();
app.MapControllers();


app.Run();