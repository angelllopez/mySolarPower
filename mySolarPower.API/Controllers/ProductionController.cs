using Microsoft.AspNetCore.Mvc;
using mySolarPower.Services.Contracts;
using MySolarPower.Data.Models;

namespace mySolarPower.API.Controllers;

/// <summary>
/// Controller for production data.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductionController : ControllerBase
{
    private readonly IProductionDataService _dataService;

    /// <summary>
    /// Initializes a new instance of the ProductionController class.
    /// </summary>
    /// <param name="dataService">Production repository interface</param>
    public ProductionController(IProductionDataService dataService)
    {
        _dataService = dataService;
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
        var productionData = await _dataService.GetAllProductionData();

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
        var productionData = await _dataService.GetProductionDataByDay(date);

        return productionData.Id < 1 ? NotFound() : Ok(productionData);
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
        var productionData = await _dataService.GetProductionDataByMonth(date);

        return !productionData.Any() ? NotFound() : Ok(productionData);
    }

    [HttpGet]
    [Route("GetProductionDataByYear")]
    public async Task<IActionResult> GetProductionDataByYearAsync(DateTime date)
    {
        var productionData = await _dataService.GetProductionDataByYear(date);

        return !productionData.Any() ? NotFound() : Ok(productionData);
    }

    [HttpPost]
    [Route("AddProductionData")]
    public async Task<IActionResult> AddProductionDataAsync(SolarPower productionRecord)
    {
        var result = await _dataService.AddProductionDataRecord(productionRecord);

        return result is false ? BadRequest() : Ok();
    }

    [HttpDelete]
    [Route("DeleteProductionData")]
    public async Task<IActionResult> DeleteProductionDataAsync(int id)
    {
        var result = await _dataService.DeleteProductionDataRecord(id);

        return result is false ? BadRequest() : Ok();
    }

    [HttpPut]
    [Route("UpdateProductionData")]
    public async Task<IActionResult> UpdateProductionDataAsync(SolarPower productionRecord)
    {
        var result = await _dataService.UpdateProductionDataRecord(productionRecord);

        return result is false ? BadRequest() : Ok();
    }
}
