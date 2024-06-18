using FluentValidation;
using ProductAdmin.API.Application;
using ProductAdmin.API.Filters;
using ProductAdmin.API.Middleware;
using ProductAdmin.API.ValidatorRules;
using ProductAdmin.Infrastructure.Cache;
using ProductAdmin.Infrastructure.HttpClients;
using ProductAdmin.Infrastructure.Persistency.Files;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.Filters.Add<LogActionFilterAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddCacheServices();
builder.Services.AddFilePersistence();
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddValidatorsFromAssemblyContaining<AddProductValidator>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
//Features
WebApplication app = null;

var serviceProvider = new Func<IServiceProvider>(() =>
{
    return app.Services;
});

builder.Services.ConfigurationSystem(serviceProvider);

app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();