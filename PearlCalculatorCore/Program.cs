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
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.IO.Compression;
using PearlCalculatorLib.Manually;
using PearlCalculatorLib.Result;

namespace PearlCalculatorCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PearlEntity pearl = new PearlEntity
            {
                Position = new Space3D(0 , 0 , 0) ,
                Motion = Space3D.Zero
            };

            ManuallyData data = new ManuallyData(0 , 0 , new Space3D(-0.775 , 0 , -0.775) , new Space3D(-0.885 , 0 , -0.775) , new Surface2D(151.25 , 605) , pearl);
            //ManuallyData data = new ManuallyData(0 , 0 , new Space3D(-1.25 , 0 , 3) , new Space3D(-3.25 , 0 , 0) , new Surface2D(1 , 15) , pearl);
            //ManuallyData data = new ManuallyData(0 , 0 , new Space3D(-1.2 , 0 , 3) , new Space3D(-3.25 , 0 , 0) , new Surface2D(1 , 15) , pearl);
            PearlCalculatorLib.Manually.Calculation.CalculateTNTAmount(data , 10270 , 10 , out List<TNTCalculationResult> result);
        }
    }
}
