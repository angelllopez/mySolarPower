using MySolarPower.Data.Models;

namespace mySolarPower.Services.Contracts
{
    public interface IProductionDataService
    {
        Task<IEnumerable<SolarPower>> GetAllProductionData();
        Task<SolarPower> GetProductionDataByDay(DateTime date);
        Task<IEnumerable<SolarPower>> GetProductionDataByMonth(DateTime date);
        Task<IEnumerable<SolarPower>> GetProductionDataByYear(DateTime date);
        Task<bool> DeleteProductionDataRecord(int id);
        Task<bool> AddProductionDataRecord(SolarPower productionRecord);
        Task<bool> UpdateProductionDataRecord(SolarPower productionRecord);
    }
}
