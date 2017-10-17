using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Service.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
            => Content("Welcome in Fibonacci service.");
    }
}
