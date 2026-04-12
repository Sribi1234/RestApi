using Microsoft.EntityFrameworkCore;

using RestApi.IService;
using RestApi.Data;
using RestApi.ServiceImplimentation;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logFolder = Path.Combine(builder.Environment.ContentRootPath, "Data");
Directory.CreateDirectory(logFolder);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(logFolder, "myapp-.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Tell .NET to use Serilog instead of the built-in logger
builder.Host.UseSerilog();

// Register DbContext for SQL Server
builder.Services.AddDbContext<CreditDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
