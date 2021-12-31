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
using PearlCalculatorIntermediateLib.Settings;
using System.Text.Json;
using System.Runtime.InteropServices;
using RegionFIleReading;
using System.Resources;
using System.Reflection;
using System.Text;

namespace PearlCalculatorCore
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            RegionFileHandler.Test();

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
