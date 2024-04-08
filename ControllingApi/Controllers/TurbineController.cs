using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllingApi.Controllers
{
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

        [HttpGet("TurbineReport", Name = "GetTurbineState")]
        public async Task<TurbineReport> GetTurbineState()
        {
            return await _turbineManager.GetTurbineReportAsync();
        }


        [HttpPost("IncreaseCapacity", Name = "IncreaseCapacity")]
        public async Task<IActionResult> IncreaseCapacity(int amount)
        {
            await _turbineManager.IncreaseCapacity(amount);
            _logger.LogInformation("Increased capacity by {amount}", amount);
            return Ok();
        }

        [HttpPost("DecreaseCapacity", Name = "DecreaseCapacity")]
        public async Task<IActionResult> DecreaseCapacity(int amount)
        {
            await _turbineManager.DecreaseCapacity(amount);
            _logger.LogInformation("Decreased capacity by {amount}", amount);
            return Ok();
        }

        [HttpPost("SetMarketPrice", Name = "SetMarketPrice")]
        public async Task<IActionResult> SetMarketPrice(int price)
        {
            await _turbineManager.SetMarketPrice(price);
            _logger.LogInformation("Market price set to {price}", price);
            return Ok();
        }
    }
}