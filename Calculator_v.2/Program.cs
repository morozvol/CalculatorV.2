using System;
using System.Collections.Generic;


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

            while (!is_simple(task))
                task = Simplify(task);
            Console.WriteLine("= {}",task);
            Console.ReadKey();
        }

        public static bool is_simple(string task)
        {
            try
            {
                double.Parse(task);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static string Simplify(string task)
        {
            if (task.Contains("("))
            {
                byte openBracket = 0, closeBracket;
                for (byte i = 0; i < task.Length; i++)
                {
                    if (task[i] == '(') openBracket = (byte) (i + 1);
                    if (task[i] == ')')
                    {
                        closeBracket = i;
                        var c2 = "(" + task.Substring(openBracket, closeBracket - openBracket) + ")";
                        task = task.Replace(c2, Simplify(c2.Substring(1, c2.Length - 2)));
                        task = task.Replace("+-", "-");
                        task = task.Replace("-+", "-");
                        task = task.Replace("--", "+");
                        //Console.WriteLine(task);
                        return task;
                    }
                }
            }
            else
            {
                while (!is_simple(task))
                {
                    var operations = SplitIntoOperation(task);
                    var main = ChooseTheMain(operations);
                    task = main.ChangeCondition(task);
                }
            }
            return task;
        }

        public static List<Operation> SplitIntoOperation(string task)
        {
            //  Console.WriteLine(task);
            string taskCpy = task;
            var numbers = task.Split("/*-+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var characters = taskCpy.Split("0123456789.,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            List<Operation> Operations = new List<Operation>();
            for (int j = 0; j < numbers.Length - 1; j++)
            {
                try
                {
                    Operations.Add(new Operation(double.Parse(numbers[j]), double.Parse(numbers[j + 1]),
                        characters[i][0]));
                    i++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вырожение введено неверно!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            return Operations;
        }

        public static Operation ChooseTheMain(List<Operation> operations)
        {
            foreach (var op in operations)
            {
                if (op.Options == '/' || op.Options == '*') return op;
            }

            return operations[0];
        }
    }

    class Calculator
    {
        public static double Calculate(Operation op)
        {
            switch (op.Options)
            {
                case '+':
                    return op.Number1 + op.Number2;
                case '-':
                    return op.Number1 - op.Number2;

                case '*':
                    return op.Number1 * op.Number2;

                case '/':
                    if (op.Number2.Equals(0))
                    {
                        Console.WriteLine("делить на ноль нельзя");
                    }

                    break;
            }

            return op.Number1 / op.Number2;
        }
    }

    class Operation
    {
        public char Options { get; }

        public double Number1 { get; }

        public double Number2 { get; }

        public string Result { get; }

        public string ChangeCondition(string task)
        {
            return task.Replace(String.Format("{0}{1}{2}", Number1, Options, Number2), Result);
        }

        public Operation(double num1, double num2, char option)
        {
            Number1 = num1;
            Number2 = num2;
            this.Options = option;
            Result = Calculator.Calculate(this).ToString();
        }
    }
}