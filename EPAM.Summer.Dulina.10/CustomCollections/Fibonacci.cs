using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    public static class Fibonacci
    {
        public static IEnumerable<int> GetFibonacciNumbers(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("Can't be less than 1", nameof(n));
            }

            if (n == 1)
            {
                yield return 1;
                yield break;
            }

            int[] fibonacci = new int[n];

            if (n >= 2)
            {
                fibonacci[0] = 1;
                fibonacci[1] = 1;
                for (int i = 0; i < 2; i++)
                {
                    yield return fibonacci[i];
                }
            }
            else
            {
                yield break;
            }

            for (int i = 2; i < n; i++)
            {
                fibonacci[i] = fibonacci[i - 2] + fibonacci[i - 1];
                yield return fibonacci[i];
            }
        }
    }
}
