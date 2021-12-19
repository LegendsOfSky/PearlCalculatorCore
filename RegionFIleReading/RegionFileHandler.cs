using RegionFIleReading.NBT;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading
{
    public static class RegionFileHandler
    {
        public unsafe static void Resolve(string filePath)
        {
            Stream reader = new FileStream(filePath , FileMode.Open);
            IntPtr streamAddress = Marshal.AllocHGlobal(sizeof(byte) * (int)reader.Length);
            IntPtr stackAddress = Marshal.AllocHGlobal(sizeof(byte) * 1050000);
            Span<byte> rawData = new Span<byte>((byte*)streamAddress , (int)reader.Length);
            byte* stackPointer = (byte*)stackAddress;
            reader.Read(rawData);
            for (int i = 0; i < 1024; i++)
            {
                int offset = BinaryPrimitives.ReverseEndianness(*(int*)((byte*)streamAddress + 4 * i)) >> 8;
                if (offset < 2)
                    continue;
                byte sectorCount = BinaryPrimitives.ReverseEndianness(*((byte*)(streamAddress + 4 * i) + 3));

                byte* address = (byte*)streamAddress + offset * 4096;

                byte compressType = *(address + 4);
                int sectorLength = BinaryPrimitives.ReverseEndianness(*(int*)address);
                byte* endAddress = address + 4 + sectorLength;

                while (true)
                {
                    if (*(int*)endAddress == 0)
                        break;
                    if ((int)endAddress > (int)(address + sectorCount * 4096))
                    {
                        endAddress = address + sectorCount * 4096;
                        break;
                    }
                    endAddress += 4;
                }

                byte[] compressedData = new byte[(int)endAddress - (int)address - 5];
                Span<byte> spanResultByte = new Span<byte>((byte*)stackAddress , 1048576);

                Marshal.Copy((IntPtr)(address + 5) , compressedData , 0 , compressedData.Length);

                if (compressType == 2)
                {
                    using MemoryStream memory = new MemoryStream(compressedData , 2 , compressedData.Length - 6);
                    new DeflateStream(memory , CompressionMode.Decompress).Read(spanResultByte);
                }
                else if (compressType == 1)
                {
                    using MemoryStream memory = new MemoryStream(compressedData);
                    new GZipStream(memory , CompressionMode.Decompress).Read(spanResultByte);
                }
                else
                {
                    using MemoryStream memory = new MemoryStream(compressedData);
                    memory.Read(spanResultByte);
                }
                
                File.WriteAllBytes("M:\\ChunkData\\ChunkData" + i , spanResultByte.ToArray());
            }
        }
    }
}
