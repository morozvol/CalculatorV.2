using System;

namespace Calculator_v._2
{
    internal class Program
    {
        private static void Main()
        {
            var task = Console.ReadLine().Replace(" ", string.Empty);
            Calculator calculator = new Calculator(task);
            calculator.CalculateTheExpression();

            Console.Clear();
            Console.WriteLine("{0} = {1}",task, calculator.Сondition);
            Console.ReadKey();
        }
    }
}