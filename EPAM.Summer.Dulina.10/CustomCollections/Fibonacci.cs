using System;
using System.Collections.Generic;

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

        public static IEnumerable<int> GetFibonacciNumber(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("Can't be less than 1", nameof(n));
            }

            int beforePrevious = 1;
            int previous = 1;

            yield return beforePrevious;

            int next = 1;
            for (int i = 0; i < n - 1; i++)
            {
                yield return next;
                next = beforePrevious + previous;
                beforePrevious = previous;
                previous = next;
            }
        }
    }
}
