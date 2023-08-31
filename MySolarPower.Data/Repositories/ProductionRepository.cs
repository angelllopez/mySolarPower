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
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataAsync()
    {

        _logger?.LogInformation(
            "Started GetProductionData {@EntityName}, {@DateTimeUtc}",
            nameof(SolarPower),
            DateTime.UtcNow);

        List<SolarPower> results = new ();
        try
        {
            results = await _context.SolarPowers
                .AsNoTracking()
                .ToListAsync();

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

    public async Task<SolarPower?> GetProductionDataByDayAsync(DateTime date)
    {
        SolarPower? result = null;
        try
        {
            result = await _context.SolarPowers
                .Where(x => x.Date == date)
                .AsNoTracking()
                .SingleAsync();

            _logger?.LogInformation(
                "Completed GetProductionDataByDay {@EntityName}, {@DateTimeUtc}",
                nameof(SolarPower),
                DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Returned an empty object {@EntityName}, {@DateTimeUtc}",
                    nameof(SolarPower),
                    DateTime.UtcNow);
        }

        return result;
    }

    public async Task<IEnumerable<SolarPower>> GetProductionDataByMonthAsync(DateTime date)
    {
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
                throw new Exception("No data found for the month.");
            }


            _logger?.LogInformation(
                "Completed GetProductionDataByMonth {@EntityName}, {@DateTimeUtc}",
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

    public async Task<IEnumerable<SolarPower>> GetProductionDataByYearAsync(DateTime date)
    {
        IEnumerable<SolarPower> results = Enumerable.Empty<SolarPower>();
        try
        {
            // return a collection that matches year if not match throw an exception.
            results = await _context.SolarPowers
                .Where(x => x.Date.Value.Year == date.Year)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            if (!results.Any())
            {
                throw new Exception("No data found for the year.");
            }

            _logger?.LogInformation(
                "Completed GetProductionDataByYear {@EntityName}, {@DateTimeUtc}",
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
