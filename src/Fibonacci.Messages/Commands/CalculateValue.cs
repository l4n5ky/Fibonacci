namespace Fibonacci.Messages.Commands
{
    public class CalculateValue : ICommand
    {
        public int Number { get; set; }
    }
}
