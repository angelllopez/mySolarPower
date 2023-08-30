using Microsoft.EntityFrameworkCore;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;
using MySolarPower.Data.Repositories;
using Serilog;
using System.Reflection;

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

// Register the Swagger generator, defining 1 or more Swagger documents.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MySolarPower.API", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
    // Set the comments path for the Swagger JSON and UI.
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    // enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c => 
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySolarPower.API v1"));
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
