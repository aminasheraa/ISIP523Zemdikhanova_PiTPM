using System;

namespace Fibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int result = Fibonacci(5);
            Console.WriteLine(result);
        }

        /// <summary>
        /// Вычисляет число Фибоначчи для заданной позиции n.
        /// </summary>
        /// <param name="n">Индекс числа в последовательности Фибоначчи.</param>
        /// <returns>Значение числа Фибоначчи.</returns>
        static int Fibonacci(int n)
        {
            Console.WriteLine("The output is: ");
            int n1 = 0;
            int n2 = 1;
            int sum;

            for (int i = 2; i <= n; i++)
            {
                sum = n1 + n2;
                n1 = n2;
                n2 = sum;
            }

            return n == 0 ? n1 : n2;
        }
    }
}
