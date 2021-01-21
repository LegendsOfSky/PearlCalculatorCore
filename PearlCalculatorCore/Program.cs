using System;
using PearlCalculatorLib;
using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;

namespace PearlCalculatorCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Direction direction = new Direction("South");
            Calculation.CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            Console.WriteLine(redTNTVector.X);
            Console.WriteLine(redTNTVector.Z);
            Console.WriteLine(blueTNTVector.X);
            Console.WriteLine(blueTNTVector.Z);
            Console.ReadKey();
        }
    }
}
