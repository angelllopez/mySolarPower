using Microsoft.Extensions.Logging;
using mySolarPower.Services.Contracts;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;
using System.Reflection;

namespace mySolarPower.Services;

public  class ProductionDataService : IProductionDataService
{
    private readonly IProductionRepository _repository;
    private readonly ILogger<IProductionDataService>? _logger;

    public ProductionDataService(IProductionRepository repository, ILogger<IProductionDataService>? logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> AddProductionDataRecord(SolarPower productionRecord)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
                       "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        try
        {
            if (productionRecord.Date < new DateTime(2021, 1, 1) || productionRecord.Date > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(productionRecord.Date), "Date is out of range.");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                    "Failed {@MethodName} at {@DateTimeUtc}.",
                        methodName,
                        DateTime.UtcNow);

            return false;
        }

        _logger?.LogInformation(
               "Completed {@MethodName} at {@DateTimeUtc}.",
                          methodName, DateTime.UtcNow);

        return await _repository.AddProductionDataAsync(productionRecord);
    }

    public async Task<bool> DeleteProductionDataRecord(int id)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
                       "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        try
        {
            if (id < 1)
            {
                throw new ArgumentException(string.Format("{0} is not a natural number.", id), nameof(id));
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                methodName,
                DateTime.UtcNow);

            return false;
        }

        _logger?.LogInformation(
                       "Completed {@MethodName} at {@DateTimeUtc}.",
                                  methodName, DateTime.UtcNow);

        return await _repository.DeleteProductionDataAsync(id);
    }

    public async Task<IEnumerable<SolarPower>> GetAllProductionData()
    {
        return await _repository.GetProductionDataAsync();
    }

    public async Task<SolarPower> GetProductionDataByDay(DateTime date)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation("Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        try
        {
            if (date < new DateTime(2021, 1, 1) || date > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(date), "Date is out of range.");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                    methodName, DateTime.UtcNow);

            return new SolarPower();
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}.",
                                                methodName, DateTime.UtcNow);

        return await _repository.GetProductionDataByDayAsync(date);
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataByMonth(DateTime date)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
               "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        try
        {
            if (date < new DateTime(2021, 1, 1) || date > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(date), "Date is out of range.");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                    methodName, DateTime.UtcNow);

            return Enumerable.Empty<SolarPower>();
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}.",
                                        methodName, DateTime.UtcNow);

        return await _repository.GetProductionDataByMonthAsync(date);
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataByYear(DateTime date)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
               "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        try
        {
            if (date < new DateTime(2021, 1, 1) || date > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(date), "Date is out of range.");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                    methodName, DateTime.UtcNow);

            return Enumerable.Empty<SolarPower>();
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}.",
                                        methodName, DateTime.UtcNow);

        return await _repository.GetProductionDataByYearAsync(date);
    }

    public async Task<bool> UpdateProductionDataRecord(SolarPower productionRecord)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
               "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        try
        {
            if (productionRecord.Date < new DateTime(2021, 1, 1) || productionRecord.Date > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(productionRecord.Date), "Date is out of range.");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                    methodName, DateTime.UtcNow);

            return false;
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}.",
                                        methodName, DateTime.UtcNow);

        return await _repository.UpdateProductionDataAsync(productionRecord);
    }
}
