#nullable disable

using RegionFIleReading.DataType;
using RegionFIleReading.Extensions;
using RegionFIleReading.NBT;
using RegionFIleReading.NBT.Content;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading
{
    public static class RegionFileHandler
    {
        static RegionFileHandler()
        {
            InitializeSolidBlock();
        }

        public unsafe static void Resolve(string filePath)
        {
            Stream reader = new FileStream(filePath , FileMode.Open);
            IntPtr streamAddress = Marshal.AllocHGlobal(sizeof(byte) * (int)reader.Length);
            IntPtr stackAddress = Marshal.AllocHGlobal(sizeof(byte) * 1050000);
            Span<byte> rawData = new Span<byte>((byte*)streamAddress , (int)reader.Length);
            byte* stackPointer = (byte*)stackAddress;
            reader.Read(rawData);
            for(int i = 0; i < 1024; i++)
            {
                int offset = BinaryPrimitives.ReverseEndianness(*(int*)((byte*)streamAddress + 4 * i)) >> 8;
                if(offset < 2)
                    continue;
                byte sectorCount = BinaryPrimitives.ReverseEndianness(*((byte*)(streamAddress + 4 * i) + 3));

                byte* address = (byte*)streamAddress + offset * 4096;

                byte compressType = *(address + 4);
                int sectorLength = BinaryPrimitives.ReverseEndianness(*(int*)address);
                byte* endAddress = address + 4 + sectorLength;

                while(true)
                {
                    if(*(int*)endAddress == 0)
                        break;
                    if((int)endAddress > (int)(address + sectorCount * 4096))
                    {
                        endAddress = address + sectorCount * 4096;
                        break;
                    }
                    endAddress += 4;
                }

                byte[] compressedData = new byte[(int)endAddress - (int)address - 5];
                Span<byte> spanResultByte = new Span<byte>((byte*)stackAddress , 1048576);

                Marshal.Copy((IntPtr)(address + 5) , compressedData , 0 , compressedData.Length);

                if(compressType == 2)
                {
                    using MemoryStream memory = new MemoryStream(compressedData , 2 , compressedData.Length - 6);
                    new DeflateStream(memory , CompressionMode.Decompress).Read(spanResultByte);
                }
                else if(compressType == 1)
                {
                    using MemoryStream memory = new MemoryStream(compressedData);
                    new GZipStream(memory , CompressionMode.Decompress).Read(spanResultByte);
                }
                else
                {
                    using MemoryStream memory = new MemoryStream(compressedData);
                    memory.Read(spanResultByte);
                }
                byte* pointer = (byte*)stackAddress;
                CompoundTagContent content = NBTReader.ReadTag(ref pointer);

                File.WriteAllBytes("M:\\ChunkData\\ChunkData" + i , spanResultByte.ToArray());
            }
        }

        private static void Analyze(CompoundTagContent regionContent)
        {
            CompoundTagContent level = regionContent[0].As<CompoundTagContent>()[0]
                                                       .As<CompoundTagContent>();
            ListTagContent<ITagContent> sections = level.First(i => i.Name == "Sections") as ListTagContent<ITagContent>;

            foreach(var section in sections)
            {
                if(((CompoundTagContent)section).Count > 1)
                {
                    long[] blockStates = null;
                    ListTagContent<ITagContent> palettes = null;

                    CompoundTagContent subChunk = (CompoundTagContent)section;
                    for(int i = 0; i < subChunk.Count; i++)
                        if(subChunk[i].Name == "BlockStates")
                            blockStates = (LongArrayTagContent)subChunk[i];
                        else if(subChunk[i].Name == "Palette")
                            palettes = (ListTagContent<ITagContent>)subChunk[i];

                    if(palettes == null)
                        continue;

                    bool[] solidBlock = new bool[palettes.Count];
                    for(int i = 0; i < palettes.Count; i++)
                    {
                        foreach(var block in (CompoundTagContent)palettes[i])
                            if(block.Name == "Name")
                                solidBlock[i] = SolidBlock.BlockDictionary[(StringTagContent)block];
                    }

                    byte[] statesArray = new byte[blockStates.Length * 8];
                    for(int i = 0; i < blockStates.Length; i++)
                    {
                        byte[] temp = BitConverter.IsLittleEndian ? BitConverter.GetBytes(BinaryPrimitives.ReverseEndianness(blockStates[i]))
                                                                  : BitConverter.GetBytes(blockStates[i]);
                        for(int j = 0; j < temp.Length; j++)
                            statesArray[i * 8 + j] = temp[j];
                    }

                    int bitCount = 0;
                    for(int i = 4; i < 32; i++)
                    {
                        if(solidBlock.Length >> (i + 1) == 0)
                        {
                            bitCount = i;
                            break;
                        }
                    }

                    //Order : Negative X -> Positive Z -> Positive Y
                    BitStack blockIndexes = new BitStack(statesArray);
                    BitArray subSolidBlockArray = new BitArray(16);
                    BitArray solidBlockArray = new BitArray(4096);

                    for(int i = 0; i < 16; i++) //Y
                    {
                        for(int j = 0; j < 16; j++) //Z
                        {
                            for(int k = 0; k < 16; k++) //X
                                subSolidBlockArray
                                    .SetFirstBit(solidBlock[blockIndexes.Pop(bitCount)])
                                    .RightShift(); //Reverse
                            solidBlockArray.Or(subSolidBlockArray);
                            solidBlockArray.LeftShift(16);
                        }
                    }



                }
            }
        }

        private static void InitializeSolidBlock()
        {
            if(SolidBlock.BlockDictionary != null)
                return;
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] files = assembly.GetManifestResourceNames();

            List<string> list = new List<string>();
            SolidBlock.BlockDictionary = new Dictionary<string , bool>();

            foreach(var file in files)
            {
                using(Stream stream = assembly.GetManifestResourceStream(file))
                {
                    if(stream != null)
                    {
                        byte[] data = new byte[stream.Length];
                        stream.Read(data , 0 , (int)stream.Length);
                        string[] names = Encoding.Default.GetString(data).Split("\r\n");
                        if(file == "RegionFIleReading.Resources.NonSolidBlockList.txt")
                            foreach(var name in names)
                                SolidBlock.BlockDictionary.Add("minecraft:" + name , false);
                        else if(file == "RegionFIleReading.Resources.SolidBlockList.txt")
                            foreach(var name in names)
                                SolidBlock.BlockDictionary.Add("minecraft:" + name , true);
                    }
                }
            }
        }

        public static unsafe void Read(byte* pointer)
        {
            CompoundTagContent content = new CompoundTagContent();
            content = NBTReader.ReadTag(ref pointer);
        }

        public static unsafe void Test(byte* pointer)
        {
            Analyze(NBTReader.ReadTag(ref pointer));
        }
    }
}
