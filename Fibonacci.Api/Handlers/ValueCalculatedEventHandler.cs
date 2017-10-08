using Fibonacci.Api.Repositories;
using Fibonacci.Messages.Events;
using System.Threading.Tasks;

namespace Fibonacci.Api.Handlers
{
    public class ValueCalculatedEventHandler : IEventHandler<ValueCalculated>
    {
        private readonly IFibRepository _repo;

        public ValueCalculatedEventHandler(IFibRepository repo)
        {
            _repo = repo;
        }

        public async Task HandleAsync(ValueCalculated @event)
        {
            _repo.Insert(number: @event.Number, result: @event.Result);
            await Task.CompletedTask;
        }
    }
}
