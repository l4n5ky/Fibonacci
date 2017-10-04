using System.Threading.Tasks;

namespace Fibonacci.Messages.Events
{
    public interface IEventHandler <in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}
