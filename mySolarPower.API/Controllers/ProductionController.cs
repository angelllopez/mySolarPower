using Microsoft.AspNetCore.Mvc;
using MySolarPower.Data.Contracts;

namespace mySolarPower.API.Controllers;

/// <summary>
/// Controller for production data.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductionController : ControllerBase
{
    private readonly IProductionRepository _repository;

    /// <summary>
    /// Initializes a new instance of the ProductionController class.
    /// </summary>
    /// <param name="repository">Production repository interface</param>
    public ProductionController(IProductionRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves collection of production data records from a repository and returns it as a response.
    /// </summary>
    /// <returns>
    /// An IActionResult object that contains either a 404 status code
    /// and a message if the production data is not available, or a 200
    /// status code and a collection production data records as the content if it is 
    /// available.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetProductionDataAsync()
    {
        var productionData = await _repository.GetProductionDataAsync();
        if (!productionData.Any())
        {
            return NotFound();
        }

        return Ok(productionData);
    }

    /// <summary>
    /// Retrives a single production data record from a repository if record property
    /// date matches the date parameter or returns a 404 status code if no record is found.
    /// </summary>
    /// <param name="date">Solar production's date</param>
    /// <returns>
    /// An IActionResult object that contains either a 404 status code
    /// and a message if the production data is not available, or a 200
    /// status code and the production data record as the content if it is 
    /// available.
    /// </returns>
    [HttpGet]
    [Route("GetProductionDataByDay")]
    public async Task<IActionResult> GetProductionDataByDayAsync(DateTime date)
    {
        var productionData = await _repository.GetProductionDataByDayAsync(date);

        if (productionData is null)
        {
            return NotFound();
        }

        return Ok(productionData);
    }

    /// <summary>
    /// Retrieves a collection of production data records from a repository if record property
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetProductionDataByMonth")]
    public async Task<IActionResult> GetProductionDataByMonthAsync(DateTime date)
    {
        var productionData = await _repository.GetProductionDataByMonthAsync(date);

        if (!productionData.Any())
        {
            return NotFound();
        }

        return Ok(productionData);
    }

    [HttpGet]
    [Route("GetProductionDataByYear")]
    public async Task<IActionResult> GetProductionDataByYearAsync(DateTime date)
    {
        var productionData = await _repository.GetProductionDataByYearAsync(date);

        if (!productionData.Any())
        {
            return NotFound();
        }

        return Ok(productionData);
    }
}
