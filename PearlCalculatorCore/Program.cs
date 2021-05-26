using System;
using System.ComponentModel.DataAnnotations;
using PearlCalculatorLib;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System.Collections.Generic;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using System.Collections;

namespace PearlCalculatorCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] TNTConfigration = new int[10] { 1 , 2 , 4 , 8 , 16 , 32 , 64 , 128 , 256 ,512};
            Data.BlueTNTConfiguration.AddRange(TNTConfigration);
            Data.RedTNTConfiguration.AddRange(TNTConfigration);
            BitArray tntConfiguration = new BitArray(20);

            Calculation.CalculateTNTConfiguration(599 , 433 , out tntConfiguration);

            foreach(var temp in tntConfiguration)
            {
                Console.WriteLine(temp.ToString());
            }

            Console.ReadKey();
        }
    }
}
