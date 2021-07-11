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

namespace PearlCalculatorCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string filePath = @"G:\r.0.0.mca";

            Console.WriteLine("File Exists : " + File.Exists(filePath).ToString());

            

            Console.WriteLine("End");
            Console.ReadKey();
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using(FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using(FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using(GZipStream decompressionStream = new GZipStream(originalFileStream , CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine($"Decompressed: {fileToDecompress.Name}");
                    }
                }
            }
        }
    }
}
