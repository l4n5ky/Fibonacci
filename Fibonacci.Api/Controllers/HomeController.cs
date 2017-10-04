using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
            => Content("Welcome in Fibonacci API.");
    }
}
