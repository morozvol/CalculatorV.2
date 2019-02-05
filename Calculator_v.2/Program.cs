using System;
using System.Collections.Generic;


namespace Calculator_v._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string task;
            task = Console.ReadLine();
            if (task.Split('(', ')').Length % 2 == 0)
            {
                Console.WriteLine("ошибка скобок");
                Console.ReadKey();
                return;
            }
            while(!is_simple(task))
            task = simplify(task);
            Console.WriteLine(task);
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

        public static string simplify(string task)
        {
            if (task.Contains("("))
            {
                byte OpenBracket = 0, CloseBracket = 0;
                for (byte i = 0; i < task.Length; i++)
                {
                    if (task[i] == '(') OpenBracket = (byte) (i + 1);
                    if (task[i] == ')')
                    {
                        CloseBracket = i;

                        string c2 = "(" + task.Substring(OpenBracket, CloseBracket - OpenBracket) + ")";
                        task=task.Replace(c2, simplify(c2.Substring(1, c2.Length - 2)));
                        Console.WriteLine(task);
                        

                        return task;
                    }
                }

            }
            else
            {
                var operations = SplitIntoOperation(task);
                var main = ChooseTheMain(operations);  
                task= main.ChangeCondition(task);
                
            }
            return task;
        }
        public static List<Operation> SplitIntoOperation(string task)
        {
            string task_Cpy = task;
           var Numbers = task.Split('+', '-', '/', '*');
           var Сharacters = task_Cpy.Split("0123456789.,".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
           int i = 0;

            List<Operation> Operations=new List<Operation>();
           for (int j = 0; j < Numbers.Length-1; j++)
           {
               try
               {
                   Operations.Add(new Operation(double.Parse(Numbers[j]), double.Parse(Numbers[j + 1]), Сharacters[i][0]));
                   i++;
               }
               catch (FormatException)
               {
                   j++;
                   Operations.Add(new Operation(double.Parse(Numbers[j])));
                   i++;
                }
           }
           
            return Operations;
        }

        public static Operation ChooseTheMain(List<Operation> Operations) //главное действие
        {
            foreach (var op in Operations)
            {
                if (op.Options == '/' || op.Options == '*') return op;
            }

            return Operations[0];
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
                    return  op.Number1 - op.Number2;
                   
                case '*':
                  return  op.Number1 * op.Number2;
                  
                case '/':
                    if (op.Number2 == 0)
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
        private double number1, number2;
        private char option;
        private string Res;

        public Char Options
        {
            get { return option; }
        }

        public double Number1
        {
            get { return number1; }
        }

        public double Number2
        {
            get { return number2; }
        }
        public string result
        {
            get { return Res; }
        }

        public string ChangeCondition(string task)
        {
            return task.Replace(String.Format("{0}{1}{2}", Number1, Options, Number2), result);
        }

        public Operation(double num1, double num2, char option)
        {
            number1 = num1;
            number2 = num2;
            this.option = option;
            Res = Calculator.Calculate(this).ToString();
        }
        public Operation(double num1)
        {
            number1 = num1;
            number2 = 1;
            this.option = '*';
            Res = number1.ToString();
        }
    }

}
