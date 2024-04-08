using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllingApi.Controllers
{
    /// <summary>
    /// Controller for managing turbine operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TurbineController : ControllerBase
    {
        private readonly ILogger<TurbineController> _logger;
        private readonly ITurbineManager _turbineManager;

        public TurbineController(ILogger<TurbineController> logger, ITurbineManager turbineManager)
        {
            _logger = logger;
            _turbineManager = turbineManager;
        }

        /// <summary>
        /// Get the current state of the turbines in the wind farm.
        /// </summary>
        /// <returns>The current state of the turbines.</returns>
        /// <response code="200">The current state of the turbines.</response>
        /// <response code="500">An error occurred while getting the turbine state.</response>
        [HttpGet("TurbineReport", Name = "GetTurbineState")]
        public async Task<TurbineReport> GetTurbineState()
        {
            return await _turbineManager.GetTurbineReportAsync();
        }

        
        /// <summary>
        /// Increase the capacity of the wind farm.
        /// </summary>
        /// <param name="amount">The amount to increase the capacity by.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        /// <response code="200">The capacity was increased successfully.</response>
        /// <response code="500">An error occurred while increasing the capacity.</response>
        /// <remarks>
        /// Sample request:
        /// amount: 10
        /// </remarks>
        [HttpPost("IncreaseCapacity", Name = "IncreaseCapacity")]
        public async Task<IActionResult> IncreaseCapacity(int amount)
        {
            await _turbineManager.IncreaseCapacity(amount);
            _logger.LogInformation("Increased capacity by {amount}", amount);
            return Ok();
        }

        /// <summary>
        /// Decrease the capacity of the wind farm.
        /// </summary>
        /// <param name="amount">The amount to decrease the capacity by.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        /// <response code="200">The capacity was decreased successfully.</response>
        /// <response code="500">An error occurred while decreasing the capacity.</response>
        /// <remarks>
        /// Sample request:
        /// amount: 10
        /// </remarks>
        [HttpPost("DecreaseCapacity", Name = "DecreaseCapacity")]
        public async Task<IActionResult> DecreaseCapacity(int amount)
        {
            await _turbineManager.DecreaseCapacity(amount);
            _logger.LogInformation("Decreased capacity by {amount}", amount);
            return Ok();
        }


        /// <summary>
        /// Set the market price for the wind farm.
        /// </summary>
        /// <param name="price">The price to set for the wind farm.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        /// <response code="200">The market price was set successfully.</response>
        /// <response code="500">An error occurred while setting the market price.</response>
        /// <remarks>
        /// Sample request:
        /// price: 100
        /// </remarks>
        [HttpPost("SetMarketPrice", Name = "SetMarketPrice")]
        public async Task<IActionResult> SetMarketPrice(int price)
        {
            await _turbineManager.SetMarketPrice(price);
            _logger.LogInformation("Market price set to {price}", price);
            return Ok();
        }
    }
}