using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySolarPower.Data.Contracts;
using MySolarPower.Data.Models;

namespace MySolarPower.Data.Repositories;

public class ProductionRepository : IProductionRepository
{
    private readonly PowerUsageDbContext _context;
    private readonly ILogger<ProductionRepository>? _logger;

    public ProductionRepository(PowerUsageDbContext context, ILogger<ProductionRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);

        _logger?.LogInformation(
        "Completed Dispose at {@DateTimeUtc}.", DateTime.UtcNow);
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataAsync()
    {
        var methodName = nameof(GetProductionDataAsync);
        _logger?.LogInformation(
            "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        List<SolarPower> results = new ();
        try
        {
            results = await _context.SolarPowers
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(true);

            _logger?.LogInformation(
                "Completed {@MethodName} at {@DateTimeUtc} with result count of {@ResultCount} records.", 
                methodName, 
                DateTime.UtcNow,
                results.Count);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                    methodName,
                    DateTime.UtcNow);
        }

        return results;
    }

    public async Task<SolarPower?> GetProductionDataByDayAsync(DateTime date)
    {
        var methodName = nameof(GetProductionDataByDayAsync);
        _logger?.LogInformation(
            "Starting {@MethodName} at {@DateTimeUtc}.",
            methodName,
            DateTime.UtcNow);

        SolarPower? result = null;
        try
        {
            result = await _context.SolarPowers
                .Where(x => x.Date == date)
                .AsNoTracking()
                .SingleAsync()
                .ConfigureAwait(true);

            _logger?.LogInformation(
                "Completed {@MethodName} at {@DateTimeUtc} with result count of 1 record.",
                methodName, DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                    methodName, DateTime.UtcNow);
        }

        return result;
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataByMonthAsync(DateTime date)
    {
        var methodName = nameof(GetProductionDataByMonthAsync);
        _logger?.LogInformation(
            "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        IEnumerable<SolarPower> results = Enumerable.Empty<SolarPower>();
        try
        {
            // return a collection that matches year and month.
            results = await _context.SolarPowers
                .Where(x => x.Date.Value.Month == date.Month && x.Date.Value.Year == date.Year)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            if (!results.Any())
            {
                throw new Exception($"No data found for {date.Month}/{date.Year}.");
            }

            _logger?.LogInformation(
                "Completed {@MethodName} at {@DateTimeUtc} with result count of {@ResultCount} records.",
                methodName,
                DateTime.UtcNow,
                results.Count());
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                methodName,
                DateTime.UtcNow);
        }

        return results;
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataByYearAsync(DateTime date)
    {
        var methodName = nameof(GetProductionDataByYearAsync);
        _logger?.LogInformation(
            "Starting {@MethodName} at {@DateTimeUtc}.", methodName, DateTime.UtcNow);

        IEnumerable<SolarPower> results = Enumerable.Empty<SolarPower>();
        try
        {
            results = await _context.SolarPowers
                .Where(x => x.Date.Value.Year == date.Year)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            if (!results.Any())
            {
                throw new Exception($"No data found for the year {date.Year}.");
            }

            _logger?.LogInformation(
                "Completed {@MethodName} at {@DateTimeUtc} with result count of {@ResultCount} records.",
                methodName,
                DateTime.UtcNow,
                results.Count());
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Failed {@MethodName} at {@DateTimeUtc}.",
                methodName,
                DateTime.UtcNow);
        }

        return results;
    }
}
