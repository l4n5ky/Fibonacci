using Fibonacci.Api.Repositories;
using Fibonacci.Messages.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System.Threading.Tasks;

namespace Fibonacci.Api.Controllers
{
    [Route("[controller]")]
    public class FibonacciController : Controller
    {
        private readonly IBusClient _client;
        private readonly IFibRepository _repo;

        public FibonacciController(IBusClient client, IFibRepository repo)
        {
            _client = client;
            _repo = repo;
        }

        [HttpGet("{number}")]
        public IActionResult Get(int number)
        {
            int? result = _repo.Get(number);
            if (result.HasValue)
            {
                return Content(result.ToString());
            }

            return Content("Result not ready or not found...");
        }

        [HttpPost("{number}")]
        public async Task<IActionResult> Post(int number)
        {
            int? result = _repo.Get(number);
            if (!result.HasValue)
            {
                await _client.PublishAsync(
                    new CalculateValue
                    {
                        Number = number
                    });
            }

            return Accepted($"fibonacci/{number}", null);
        }
    }
}
