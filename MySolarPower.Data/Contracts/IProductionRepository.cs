using MySolarPower.Data.Models;

namespace MySolarPower.Data.Contracts;

public interface IProductionRepository : IDisposable
{
    Task<IEnumerable<SolarPower>> GetProductionDataAsync();
    Task<SolarPower> GetProductionDataByDayAsync(DateTime? date);
    Task<IEnumerable<SolarPower>> GetProductionDataByMonthAsync(DateTime date);
    Task<IEnumerable<SolarPower>> GetProductionDataByYearAsync(DateTime date);
    Task<bool> AddProductionDataAsync(SolarPower record);
    Task<bool> DeleteProductionDataAsync(int id);
    Task<bool> UpdateProductionDataAsync(SolarPower productionRecord);
}
