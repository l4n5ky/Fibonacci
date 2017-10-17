using System.Collections.Generic;

namespace Fibonacci.Api.Repositories
{
    public class InMemoryFibRepository : IFibRepository
    {
        private readonly IDictionary<int, int> _results = new Dictionary<int, int>();

        public int? Get(int number)
        {
            int result;
            if (_results.TryGetValue(number, out result))
            {
                return result;
            }

            return null;
        }

        public void Insert(int number, int result)
        {
            _results[number] = result;
        }
    }
}
