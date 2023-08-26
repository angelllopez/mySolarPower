using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;

namespace MySolarPower.Data.Repositories;

public class ProductionRepository : IProductionRepository
{
    private readonly PowerUsageDbContext? _context;
    private readonly ILogger<ProductionRepository>? _logger;

    public ProductionRepository()
    {
    }

    public ProductionRepository(PowerUsageDbContext context, ILogger<ProductionRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataAsync()
    {

        _logger?.LogInformation(
                       "Started GetProductionData {@EntityName}, {@DateTimeUtc}",
                                  nameof(SolarPower),
                                             DateTime.UtcNow);

        List<SolarPower> results = new List<SolarPower>();
        try
        {
            results = await _context.SolarPowers.ToListAsync();

            _logger?.LogInformation(
                "Completed GetProductionData {@EntityName}, {@DateTimeUtc}",
                nameof(SolarPower),
                DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Returned an empty list {@EntityName}, {@DateTimeUtc}",
                    nameof(SolarPower),
                    DateTime.UtcNow);
        }

        return results;
    }
}
