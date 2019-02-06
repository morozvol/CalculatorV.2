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

            return operations[0];
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