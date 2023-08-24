using Microsoft.AspNetCore.Mvc;
using MySolarPower.Data.Contracts;

namespace mySolarPower.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionRepository _repository;

        public ProductionController(IProductionRepository repository)
        {
            _repository = repository;
        }

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
