using System;
using System.Collections.Generic;


namespace Calculator_v._2
{
    class Calculator
    {
        public Calculator(string task)
        {
            Сondition = task;
        }

        private string _condition;
        private bool _isPositiveValue = true;

        public void ChangeSignInResponse()
        {
            _isPositiveValue = !_isPositiveValue;
        }

        public string Сondition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public bool IsPositiveValue
        {
            get { return _isPositiveValue; }
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

        private int CountWords()
        {
            int count = (Сondition.Length - Сondition.Replace("*-", "").Length) / 2;
            count += (Сondition.Length - Сondition.Replace("/-", "").Length) / 2;
            return count;
        }

        public string Simplify()
        {
            while (true)
            {
                Bracket bracket = FindTheActionIBrackets();
                if (bracket.lenthBracket == 0) break;
                {
                    var c2 = Сondition.Substring(bracket.openBracket, bracket.lenthBracket);
                    var calculator = new Calculator(c2.Substring(1, c2.Length - 2));
                    Сondition = Сondition.Replace(c2, calculator.Simplify());


                    if (CountWords() % 2 != 0) ChangeSignInResponse();

                    Сondition = Сondition.Replace("+-", "-");
                    Сondition = Сondition.Replace("-+", "-");
                    Сondition = Сondition.Replace("--", "+");
                    Сondition = Сondition.Replace("/-", "/");
                    Сondition = Сondition.Replace("*-", "*");
                }
            }

            while (!IsSimple())
            {
                var operations = SplitIntoOperation();
                var main = Operation.ChooseTheMain(operations);
                Сondition = main.ChangeCondition(Сondition);
            }

            if (!IsPositiveValue)
                Сondition = new Operation(double.Parse(Сondition), -1, '*').ChangeCondition(Сondition);

            return Сondition;
        }

        private Bracket FindTheActionIBrackets()
        {
            if (!Сondition.Contains("(") || !Сondition.Contains(")")) return new Bracket(0, 0);
            byte openBracket = 128, lenthBracket;
            for (byte i = 0; i < Сondition.Length; i++)
            {
                if (Сondition[i] == '(') openBracket = i;
                if (Сondition[i] == ')')
                {
                    if (openBracket > i)
                    {
                        //TODO: обработка ошибки
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
            string taskCpy = Сondition;
            var numbers = Сondition.Split("/*-+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
                    //Console.WriteLine(_condition);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            return Operations;
        }
    }
}