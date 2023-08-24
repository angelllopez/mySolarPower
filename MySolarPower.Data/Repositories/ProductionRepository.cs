using Microsoft.EntityFrameworkCore;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;

namespace MySolarPower.Data.Repositories;

public class ProductionRepository : IProductionRepository, IDisposable
{
    private readonly PowerUsageDbContext _context;

    public ProductionRepository(PowerUsageDbContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataAsync()
    {
        try
        {
            var results = await _context.SolarPowers.ToListAsync();
            return results;
        }
        catch (Exception ex)
        {
            //throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            return Enumerable.Empty<SolarPower>();
        }
    }
}
