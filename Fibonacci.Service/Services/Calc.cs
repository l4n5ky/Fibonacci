using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fibonacci.Service.Services
{
    public class Calc : ICalc
    {
        public int Fib(int number)
        {
            int a = 0;
            int b = 1;

            for (int i = 0; i < number; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }

            return a;
        }
    }
}
