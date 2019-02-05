using System;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Calculator_v._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string task;
            task = Console.ReadLine();



            Console.ReadKey();
        }

        public static string simplify(string task)
        {

            if (task.Split('(', ')').Length % 2 == 0)
            {
                Console.WriteLine("ошибка скобок");
                return "Error";
            }
            else
            {
                foreach (var c in task)
                {
                    Console.WriteLine(1);
                }
                return task;
            }
        }
    }

    class Calculator
    {
        public static double Calculate(char option, double number1, double number2)
        {
            switch (option)
            {
                case '+':
                    return number1 + number2;
              
                case '-':
                    return number1 - number2;
                   
                case '*':
                   return number1 * number2;
                  
                case '/':
                    if (number2 == 0)
                    {
                        Console.WriteLine("делить на ноль нельзя");
                    }
                   break;
            }
            return number1 / number2;
        }

    }
}
