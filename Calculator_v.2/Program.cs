using System;

namespace Calculator_v._2
{
    internal class Program
    {
        private static void Main()
        {
            var task = Console.ReadLine();
           
            Calculator calculator = new Calculator(task);
            if(calculator.IsFindErrorInTask())return;
            calculator.ReplaceBinaryOperator();
            calculator.Simplify();

            Console.WriteLine("= {0}", calculator.Сondition);
            Console.ReadKey();
        }
    }
}