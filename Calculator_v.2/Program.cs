using System;

namespace Calculator_v._2
{
    internal class Program
    {
        private static void Main()
        {
            var task = Console.ReadLine().Replace(" ", string.Empty);
            Calculator calculator = new Calculator(task);
            if(calculator.IsFindErrorInTask())return;
            calculator.ReplaceBinaryOperator();
            calculator.Simplify();

            Console.Clear();
            Console.WriteLine("{0} = {1}",task, calculator.Сondition);
            Console.ReadKey();
        }
    }
}