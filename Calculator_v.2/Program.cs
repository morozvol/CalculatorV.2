using System;

namespace Calculator_v._2
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var task = "";
            task = Console.ReadLine();
            if (task.Split('(', ')').Length % 2 == 0)
            {
                Console.WriteLine("ошибка скобок");
                Console.ReadKey();
                return;
            }

            Calculator calculator = new Calculator(task);
                calculator.Simplify();

                Console.WriteLine("= {0}", calculator.Сondition);
                Console.ReadKey();
        }
    }
}