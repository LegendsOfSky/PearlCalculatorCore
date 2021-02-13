using System;
using System.ComponentModel.DataAnnotations;
using PearlCalculatorLib;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.AttachedLLFTL;
using System.Collections.Generic;

namespace LLFTLAttachPositionCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Welcome to use LLFTL Attach Position Calculator");
            PearlCalculatorLib.AttachedLLFTL.Calculation.CalculateSuitableAttachLocation(80 , Direction.East , 12 , 256);
            foreach(var temp in PearlCalculatorLib.AttachedLLFTL.Data.LLFTLResult)
            {
                Console.WriteLine(temp.ToString());
            }
        }
    }
}
