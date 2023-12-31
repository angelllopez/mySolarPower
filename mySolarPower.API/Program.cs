using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using mySolarPower.Services;
using mySolarPower.Services.Contracts;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;
using MySolarPower.Data.Repositories;
using Serilog;
using System.Reflection;

var allowSpecificOrigins = "_myAllowSpecificOrigins";
var allowAnyOrigin = "_myAllowAnyOrigin";

var builder = WebApplication.CreateBuilder(args);


/*
 * Same-origin policy: https://developer.mozilla.org/en-US/docs/Web/Security/Same-origin_policy
 * Cross-Origin Resource Sharing (CORS): https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
 */
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7258/", "https://localhost:7148/");
                      });

    options.AddPolicy(name: allowAnyOrigin,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                      });
});


builder.Services.AddScoped<IProductionRepository, ProductionRepository>();
builder.Services.AddScoped<IProductionDataService, ProductionDataService>();

builder.Host.UseSerilog((context, loggerConfiguration) =>
loggerConfiguration
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

builder.Services.AddDbContext<PowerUsageDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

    app.UseCors(allowAnyOrigin);
} else
{
    app.UseCors(allowSpecificOrigins);
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
