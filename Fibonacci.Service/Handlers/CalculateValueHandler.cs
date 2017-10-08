using Fibonacci.Messages.Commands;
using Fibonacci.Messages.Events;
using RawRabbit;
using System.Threading.Tasks;

namespace Fibonacci.Service.Handlers
{
    public class CalculateValueHandler : ICommandHandler<CalculateValue>
    {
        private readonly IBusClient _client;

        public CalculateValueHandler(IBusClient client)
        {
            _client = client;
        }

        public async Task HandleAsync(CalculateValue command)
        {
            int result = Fib(command.Number);

            await _client.PublishAsync(
                new ValueCalculated
                {
                    Number = command.Number,
                    Result = result
                });
        }

        private int Fib(int number)
        {
            switch (number)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                default:
                    return Fib(number - 2) + Fib(number - 1);
            }
        }
    }
}
