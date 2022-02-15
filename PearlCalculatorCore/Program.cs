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
using System.Threading;
using System.Linq;
using System.Reflection.Metadata;
using RegionFIleReading.DataType;

namespace PearlCalculatorCore
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            //RegionFileHandler.Resolve(@"K:\minecraft setting\modded\1.16.4\General Purpose\saves\Trenchless World Eater\region\r.0.0.mca");

            Span<byte> data = new Span<byte>(File.ReadAllBytes(@"M:\ChunkData\ChunkData0"));
            IntPtr ptr = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data.ToArray() , 0 , ptr , data.Length);
            RegionFileHandler.Test((byte*)ptr);

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
