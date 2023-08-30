using MySolarPower.Data.Models;

namespace MySolarPower.Data.Contracts;

public interface IProductionRepository : IDisposable
{
    Task<IEnumerable<SolarPower>> GetProductionDataAsync();
    Task<SolarPower?> GetProductionDataByDateAsync(DateTime date);
}
