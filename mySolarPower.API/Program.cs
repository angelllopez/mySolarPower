using Microsoft.EntityFrameworkCore;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;
using MySolarPower.Data.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => 
loggerConfiguration
    .ReadFrom.Configuration(context.Configuration));


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<PowerUsageDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductionRepository, ProductionRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
