using Microsoft.AspNetCore.Authorization;
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
    /// <param name="dataService">
    /// An IProductionDataService type that represents the dependency injected.
    /// Production repository interface
    /// </param>
    public ProductionController(IProductionDataService dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Asynchronously retrieves all the production data records from the repository and 
    /// returns an IActionResult type that represents two possible HTTP status codes: 200 
    /// or 404. If the production data is available, it returns Ok(productionData). If not,
    /// it returns NotFound().
    /// </summary>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
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
    /// Asynchronously retrives a single production data record from the repository and 
    /// returns an IActionResult type that represents two possible HTTP status codes: 200 or 404.
    /// If record property date matches the date parameter, it returns Ok(productionData). If not,
    /// it returns NotFound().
    /// </summary>
    /// <param name="date">
    /// A DateTime value that specifies the date to filter the production data records.
    /// </param>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
    /// </returns>
    [HttpGet]
    [Route("GetProductionDataByDay")]
    public async Task<IActionResult> GetProductionDataByDayAsync(DateTime date)
    {
        var productionData = await _dataService.GetProductionDataByDay(date);

        return productionData.Id < 1 ? NotFound() : Ok(productionData);
    }

    /// <summary>
    /// Asynchronously retrieves a collection of production data records from a repository and 
    /// returns an IActionResult type that represents two possible HTTP status codes: 200 or 404.
    /// If record properties Date.Month and Date.Year matches the date.month and date.year parameters, 
    /// it returns Ok(productionData). If not, it returns NotFound().
    /// or returns a 404 status code if no record is found.
    /// </summary>
    /// <param name="date">
    /// A DateTime value that specifies the month and year to filter the production data records.
    /// </param>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
    /// </returns>
    [HttpGet]
    [Route("GetProductionDataByMonth")]
    public async Task<IActionResult> GetProductionDataByMonthAsync(DateTime date)
    {
        var productionData = await _dataService.GetProductionDataByMonth(date);

        return !productionData.Any() ? NotFound() : Ok(productionData);
    }

    /// <summary>
    /// Asynchronously retrieves a collection of production data records from a repository and
    /// returns an IActionResult type that represents two possible HTTP status codes: 200 or 404.
    /// If record property Date.Year matches the date.year parameter, it returns Ok(productionData).
    /// If not, it returns NotFound().
    /// </summary>
    /// <param name="date">
    /// A DateTime value that specifies the year to filter the production data records.
    /// </param>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
    /// </returns>
    [HttpGet]
    [Route("GetProductionDataByYear")]
    public async Task<IActionResult> GetProductionDataByYearAsync(DateTime date)
    {
        var productionData = await _dataService.GetProductionDataByYear(date);

        return !productionData.Any() ? NotFound() : Ok(productionData);
    }

    /// <summary>
    /// Asynchronously adds a production data record to the repository and returns an IActionResult
    /// type that represents two possible HTTP status codes: 200 or 400. If the add operation is
    /// successful, it returns Ok(). If not, it returns BadRequest().
    /// </summary>
    /// <param name="productionRecord">
    /// A SolarPower object that contains the production data record to add to the repository.
    /// </param>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
    /// </returns>
    [HttpPost]
    [Route("AddProductionData")]
    public async Task<IActionResult> AddProductionDataAsync(SolarPower productionRecord)
    {
        var result = await _dataService.AddProductionDataRecord(productionRecord);

        return result is false ? BadRequest() : Ok();
    }

    /// <summary>
    /// Asynchronously deletes a production data record from the repository and returns an IActionResult
    /// type that represents two possible HTTP status codes: 200 or 400. If the delete operation is
    /// successful, it returns Ok(). If not, it returns BadRequest().
    /// </summary>
    /// <param name="id">
    /// An integer value that specifies the production data record to delete from the repository.
    /// </param>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
    /// </returns>
    [HttpDelete]
    [Route("DeleteProductionData")]
    public async Task<IActionResult> DeleteProductionDataAsync(int id)
    {
        var result = await _dataService.DeleteProductionDataRecord(id);

        return result is false ? BadRequest() : Ok();
    }

    /// <summary>
    /// Asynchronously updates a production data record in the repository and returns an IActionResult
    /// type that represents two possible HTTP status codes: 200 or 400. If the update operation is
    /// successful, it returns Ok(). If not, it returns BadRequest().
    /// </summary>
    /// <param name="productionRecord">
    /// A SolarPower object that contains the production data record to update in the repository.
    /// </param>
    /// <returns>
    /// A Task IActionResult object that represents the result of the asynchronous operation.
    /// </returns>
    [HttpPut]
    [Route("UpdateProductionData")]
    public async Task<IActionResult> UpdateProductionDataAsync(SolarPower productionRecord)
    {
        var result = await _dataService.UpdateProductionDataRecord(productionRecord);

        return result is false ? BadRequest() : Ok();
    }
}
