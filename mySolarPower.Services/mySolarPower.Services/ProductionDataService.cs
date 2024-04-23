using Microsoft.Extensions.Logging;
using mySolarPower.Services.Contracts;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;
using System.Reflection;

namespace mySolarPower.Services;

/// <summary>
/// Production data service.
/// </summary>
public  class ProductionDataService : IProductionDataService
{
    private readonly IProductionRepository _repository;
    private readonly ILogger<IProductionDataService>? _logger;

    /// <summary>
    /// Initializes a new instance of the ProductionDataService class.
    /// </summary>
    /// <param name="repository">
    /// An IProductionRepository type that represents the dependency injected.
    /// </param>
    /// <param name="logger">
    /// An ILogger type that represents the dependency injected.
    /// </param>
    public ProductionDataService(IProductionRepository repository, ILogger<IProductionDataService>? logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Validates the production data record againts the business rules. If the record is valid, 
    /// it passes it to the repository's add operation to be asynchronously added to the 
    /// database. The result of the operation is then returned. If the record is invalid, it throws 
    /// an exception.
    /// </summary>
    /// <param name="productionRecord">
    /// A SolarPower type that represents the production data record to be added to the database.
    /// </param>
    /// <returns>
    /// A Task bool type that represents the result of the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An ArgumentOutOfRangeException is thrown if the production data record date is out of range.
    /// </exception>
    public async Task<bool> AddProductionDataRecord(SolarPower productionRecord)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
                       "Starting {@MethodName} at {@DateTimeUtc}", methodName, DateTime.UtcNow);

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
                    "Failed {@MethodName} at {@DateTimeUtc}",
                        methodName,
                        DateTime.UtcNow);

            return false;
        }

        _logger?.LogInformation(
               "Completed {@MethodName} at {@DateTimeUtc}",
                          methodName, DateTime.UtcNow);

        return await _repository.AddProductionDataAsync(productionRecord);
    }

    /// <summary>
    /// Validates the production data record id againts the business rules. If the id is valid,
    /// it passes it to the repository's delete operation to be asynchronously deleted from the
    /// database. The result of the operation is then returned. If the id is invalid, it throws
    /// an exception.
    /// </summary>
    /// <param name="id">
    /// An integer value that represents the production data record id to be deleted from the
    /// database.
    /// </param>
    /// <returns>
    /// A Task bool type that represents the result of the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// An ArgumentException is thrown if the production data record id is not a natural number.
    /// </exception>
    public async Task<bool> DeleteProductionDataRecord(int id)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
                       "Starting {@MethodName} at {@DateTimeUtc}", methodName, DateTime.UtcNow);

        try
        {
            // Check if the id is a natural number.
            if(id <= 0)
            {
                throw new ArgumentException(string.Format("{0} is not a valid input.", id), nameof(id));
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}",
                methodName,
                DateTime.UtcNow);

            return false;
        }

        _logger?.LogInformation(
                       "Completed {@MethodName} at {@DateTimeUtc}",
                                  methodName, DateTime.UtcNow);

        return await _repository.DeleteProductionDataAsync(id);
    }

    /// <summary>
    /// Asynchronously returns all production data records from the repository.
    /// This method is a pass-through method to the repository's GetAllProductionDataAsync()
    /// </summary>
    /// <returns>
    /// A Task IEnumerable SolarPower type that represents the result of the asynchronous operation.
    /// </returns>
    public async Task<IEnumerable<SolarPower>> GetAllProductionData()
    {
        return await _repository.GetProductionDataAsync();
    }

    /// <summary>
    /// Validates the production data record date againts the business rules. If the date is valid,
    /// it passes it to the repository's get operation to be asynchronously retrieved from the
    /// database. The result of the operation is then returned. If the date is invalid, it throws
    /// an exception.
    /// </summary>
    /// <param name="date">
    /// A DateTime value that represents the production data record date to be retrieved from the
    /// database.
    /// </param>
    /// <returns>
    /// A Task SolarPower type that represents the result of the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An ArgumentOutOfRangeException is thrown if the production data record date is out of range.
    /// </exception>
    public async Task<SolarPower> GetProductionDataByDay(DateTime? date)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation("Starting {@MethodName} at {@DateTimeUtc}", methodName, DateTime.UtcNow);

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
                "Failed {@MethodName} at {@DateTimeUtc}",
                    methodName, DateTime.UtcNow);

            return new SolarPower();
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}",
                                                methodName, DateTime.UtcNow);

        return await _repository.GetProductionDataByDayAsync(date);
    }

    /// <summary>
    /// Validates the date value againts the business rules. If the date is valid,
    /// it passes it to the repository's get operation to asynchronously retrieved matching 
    /// records from the database. The result of the operation is then returned. If the date.month 
    /// is invalid, it throws an exception.
    /// </summary>
    /// <param name="date">
    /// A DateTime value that represents the production data records date.month to be retrieved from 
    /// the database.
    /// </param>
    /// <returns>
    /// A Task IEnumerable SolarPower type that represents the result of the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An ArgumentOutOfRangeException is thrown if the production data record date is out of range.
    /// </exception>
    public async Task<IEnumerable<SolarPower>> GetProductionDataByMonth(DateTime date)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
               "Starting {@MethodName} at {@DateTimeUtc}", methodName, DateTime.UtcNow);

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
                "Failed {@MethodName} at {@DateTimeUtc}",
                    methodName, DateTime.UtcNow);

            return Enumerable.Empty<SolarPower>();
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}",
                                        methodName, DateTime.UtcNow);

        return await _repository.GetProductionDataByMonthAsync(date);
    }

    /// <summary>
    /// Validates the date value againts the business rules. If the date is valid,
    /// it passes it to the repository's get operation to asynchronously retrieved matching
    /// records from the database. The result of the operation is then returned. If the date
    /// is invalid, it throws an exception.
    /// </summary>
    /// <param name="date">
    /// A DateTime value that represents the production data records date to be retrieved from
    /// </param>
    /// <returns>
    /// An IEnumerable SolarPower type that represents the result of the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public async Task<IEnumerable<SolarPower>> GetProductionDataByYear(DateTime date)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
               "Starting {@MethodName} at {@DateTimeUtc}", methodName, DateTime.UtcNow);

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
                "Failed {@MethodName} at {@DateTimeUtc}",
                    methodName, DateTime.UtcNow);

            return Enumerable.Empty<SolarPower>();
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}",
                                        methodName, DateTime.UtcNow);

        return await _repository.GetProductionDataByYearAsync(date);
    }

    /// <summary>
    /// Validates the production data record againts the business rules. If the record is valid,
    /// it passes it to the repository's update operation to be asynchronously updated in the 
    /// database. The result of the operation is then returned. If the record is invalid, it throws
    /// an exception.
    /// </summary>
    /// <param name="productionRecord">
    /// A SolarPower type that represents the production data record to be updated in the database.
    /// </param>
    /// <returns>
    /// A Task bool type that represents the result of the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An ArgumentOutOfRangeException is thrown if the production data record date is out of range.
    /// </exception>
    public async Task<bool> UpdateProductionDataRecord(SolarPower productionRecord)
    {
        var methodName = MethodBase.GetCurrentMethod()?.Name;

        _logger?.LogInformation(
               "Starting {@MethodName} at {@DateTimeUtc}", methodName, DateTime.UtcNow);

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
                "Failed {@MethodName} at {@DateTimeUtc}",
                    methodName, DateTime.UtcNow);

            return false;
        }

        _logger?.LogInformation("Completed {@MethodName} at {@DateTimeUtc}",
                                        methodName, DateTime.UtcNow);

        return await _repository.UpdateProductionDataAsync(productionRecord);
    }
}
