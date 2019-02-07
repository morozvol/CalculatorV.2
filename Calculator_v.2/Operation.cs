using System;
using System.Collections.Generic;

namespace Calculator_v._2
{
    class Operation
    {
        public char Options { get; }
        public double Number1 { get; }
        public double Number2 { get; }
        public string Result { get; }

        public Operation(double num1, double num2, char option)
        {
            Number1 = num1;
            Number2 = num2;
            this.Options = option;

            Result = Calculate().ToString();
        }


        public string ChangeCondition(string task)
        {
            return task.Replace(String.Format("{0}{1}{2}", Number1, Options, Number2), Result);
        }

        public static Operation ChooseTheMain(List<Operation> operations)
        {
            foreach (var op in operations)
            {
                if (op.Options == '/' || op.Options == '*') return op;
            }

            try
            {
                return operations[0];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Вырожение введено неверно!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return operations[0];
        }

        public  double Calculate()
        {
            switch (Options)
            {
                case '+':
                    return Number1 + Number2;
                case '~':
                    return Number1 - Number2;
                case '*':
                    return Number1 * Number2;
                case '/':
                    if (Number2.Equals(0))
                    {
                        Console.WriteLine("делить на ноль нельзя");
                    }
                    break;
                default:
                    break;

            }
            return Number1 / Number2;
        }

    }
}