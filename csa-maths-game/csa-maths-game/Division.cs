﻿namespace csa_maths_game
{
    internal class Division : IOperation
    {
        public int Execute(int operandA, int operandB)
        {
            return operandA / operandB;
        }
        public string GetTextString()
        {
            return "Division";
        }
        public char GetSymbolChar()
        {
            return '/';
        }
        public bool IsValidOperands(int operandA, int operandB)
        {
            if (operandB == 0) return false;

            return (operandA % operandB) == 0;
        }
    }
}
