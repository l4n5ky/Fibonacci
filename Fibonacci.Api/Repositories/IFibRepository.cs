namespace Fibonacci.Api.Repositories
{
    public interface IFibRepository
    {
        int? Get(int number);
        void Insert(int number, int result);
    }
}
