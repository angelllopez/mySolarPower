using Microsoft.EntityFrameworkCore;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;

namespace MySolarPower.Data.Repositories;

public class ProductionRepository : IProductionRepository
{
    private readonly PowerUsageDbContext? _context;

    public ProductionRepository()
    {
    }

    public ProductionRepository(PowerUsageDbContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataAsync()
    {
        List<SolarPower> results = new List<SolarPower>();
        try
        {
            if (_context != null)
            {
                results = await _context.SolarPowers.ToListAsync();
            }

        }
        catch (Exception ex)
        {
            //throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            //return Enumerable.Empty<SolarPower>();
        }

        return results;
    }
}
