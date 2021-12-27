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

namespace PearlCalculatorCore
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Span<byte> data = new Span<byte>(File.ReadAllBytes(@"M:\ChunkData\ChunkData0"));
            IntPtr pointer = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data.ToArray() , 0 , pointer , data.Length);
            RegionFileHandler.Read((byte*)pointer);
            Console.ReadKey();
        }
    }
}
