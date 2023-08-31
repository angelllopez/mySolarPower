using MySolarPower.Data.Models;

namespace MySolarPower.Data.Contracts;

public interface IProductionRepository : IDisposable
{
    Task<IEnumerable<SolarPower>> GetProductionDataAsync();
    Task<SolarPower?> GetProductionDataByDayAsync(DateTime date);
    Task<IEnumerable<SolarPower>> GetProductionDataByMonthAsync(DateTime date);
    Task<IEnumerable<SolarPower>> GetProductionDataByYearAsync(DateTime date);
}
