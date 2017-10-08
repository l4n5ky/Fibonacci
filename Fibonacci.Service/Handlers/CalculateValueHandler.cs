using Fibonacci.Messages.Commands;
using Fibonacci.Messages.Events;
using Fibonacci.Service.Services;
using RawRabbit;
using System.Threading.Tasks;

namespace Fibonacci.Service.Handlers
{
    public class CalculateValueHandler : ICommandHandler<CalculateValue>
    {
        private readonly IBusClient _client;
        private readonly ICalc _calc;

        public CalculateValueHandler(IBusClient client, ICalc calc)
        {
            _client = client;
            _calc = calc;
        }

        public async Task HandleAsync(CalculateValue command)
        {
            int result = _calc.Fib(command.Number);

            await _client.PublishAsync(
                new ValueCalculated
                {
                    Number = command.Number,
                    Result = result
                });
        }
    }
}
