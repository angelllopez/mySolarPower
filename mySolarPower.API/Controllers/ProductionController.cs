using Microsoft.AspNetCore.Mvc;
using MySolarPower.Data.Contracts;

namespace mySolarPower.API.Controllers
{
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
        /// Retrieves production data from a repository and returns it as a response.
        /// </summary>
        /// <returns>
        /// An IActionResult object that contains either a 404 status code
        /// and a message if the production data is not available, or a 200
        /// status code and the production data as the content if it is 
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
    }
}
