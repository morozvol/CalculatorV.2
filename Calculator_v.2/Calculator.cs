using System;
using System.Collections.Generic;


namespace Calculator_v._2
{
    class Calculator
    {
        public string Сondition { get; set; }

        public Calculator(string task)
        {
            Сondition = task;
        }


        public bool IsFindErrorInTask()
        {
            if (Сondition.Length < 1)
            { 
                Console.WriteLine("слишком короткое выражение");
                Console.ReadKey();
                return true;
            }
            if (Сondition.Split('(', ')').Length % 2 == 0)
            {
                Console.WriteLine("ошибка скобок");
                Console.ReadKey();
                return true;
            }

            try
            {
                Сondition = Сondition.Replace(",", ".");
                double.Parse("45.3");
            }
            catch (FormatException)
            {
                Сondition = Сondition.Replace(".", ",");
            }
            return false;
        }

        public void ReplaceBinaryOperator()
        {
            for (int i = 0; i < Сondition.Length; i++)
            {
                if (Сondition[i] == '-')
                {
                    if (i != 0 && Сondition[i - 1] != '(')
                        Сondition = Сondition.Remove(i, 1).Insert(i, '~'.ToString());
                }
            }
        }

        private bool IsSimple()
        {
            try
            {
                double.Parse(Сondition);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string CalculateTheExpression()
        {
            if (IsFindErrorInTask()) Environment.Exit(0);
            ReplaceBinaryOperator();
            return Simplify();
        }

        private string Simplify()
        {
            while (true)
            {
                Bracket bracket = FindTheActionIBrackets();
                if (bracket.lenthBracket == 0) break;
                {
                    var c2 = Сondition.Substring(bracket.openBracket, bracket.lenthBracket);
                    var calculator = new Calculator(c2.Substring(1, c2.Length - 2));
                    Сondition = Сondition.Replace(c2, calculator.Simplify());
                }
            }

            while (!IsSimple())
            {
                var operations = SplitIntoOperation();
                var main = Operation.ChooseTheMain(operations);
                Сondition = main.ChangeCondition(Сondition);
            }
            return Сondition;
        }

        private Bracket FindTheActionIBrackets()
        {
            if (!Сondition.Contains("(") || !Сondition.Contains(")"))
                return new Bracket(0, 0);

            byte openBracket = 128, lenthBracket;
            for (byte i = 0; i < Сondition.Length; i++)
            {
                if (Сondition[i] == '(') openBracket = i;
                if (Сondition[i] == ')')
                {
                    if (openBracket > i)
                    {
                        Console.WriteLine("ошибка скобок");
                        Console.ReadKey();
                        Environment.Exit(0);
                        return new Bracket(0, 0);
                    }

                    lenthBracket = (byte) (i - openBracket + 1);
                    return new Bracket(openBracket, lenthBracket);
                }
            }
            return new Bracket(0, 0);
        }

        private List<Operation> SplitIntoOperation()
        {
            var numbers    = Сondition.Split("/*~+".ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);
            var characters = Сondition.Split("0123456789.,-".ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            List<Operation> Operations = new List<Operation>();
            for (int j = 0; j < numbers.Length - 1; j++)
            {
                try
                {
                    Operations.Add(new Operation(double.Parse(numbers[j]),
                        double.Parse(numbers[j + 1]), characters[i][0]));
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
    }
}