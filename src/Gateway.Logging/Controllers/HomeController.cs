using Microsoft.AspNetCore.Mvc;

namespace Gateay.PiggyConnector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Gateway.Logging!");
    }
}
