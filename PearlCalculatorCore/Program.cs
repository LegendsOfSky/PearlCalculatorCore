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
            Space3D vector = VectorCalculation.CalculateMotion(new Space3D(0 , 170.34722638929408 , 0) , new Space3D(-0.8 , 170.5 , -0.8));
            Console.WriteLine(vector.X);
            Console.WriteLine(vector.Y);
            Console.WriteLine(vector.Z);
            Console.ReadKey();
        }
    }
}
