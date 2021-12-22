using RegionFIleReading.NBT.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT
{
    internal unsafe static class NBTReader
    {
        internal static void ReadTag(ref byte* pointer)
        {
            TagType type = (TagType)(*pointer);
            switch (type)
            {
                case TagType.Null:
                    break;
                case TagType.Int:
                    break;
                case TagType.Short:
                    break;
                case TagType.Byte:
                    break;
                case TagType.Long:
                    break;
                case TagType.Float:
                    break;
                case TagType.Double:
                    break;
                case TagType.String:
                    break;
                case TagType.Compound:
                    break;
                case TagType.List:
                    break;
                case TagType.IntArray:
                    break;
                case TagType.ByteArray:
                    break;
                case TagType.LongArray:
                    break;
                default:
                    break;
            }
        }

        private static string ReadString(ref byte* pointer)
        {
            string name = new string((char*)(pointer + 1) , 0 , *pointer);
            pointer += *pointer;
            return name;
        }

        private static BasicTagContent<int> ReadTagInt(ref byte* pointer)
        {
            BasicTagContent<int> content = new BasicTagContent<int>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data = *(int*)pointer;
            pointer += 4;
            return content;
        }

        private static BasicTagContent<short> ReadTagShort(ref byte* pointer)
        {
            BasicTagContent<short> content = new BasicTagContent<short>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data = *(short*)pointer;
            pointer += 2;
            return content;
        }

        private static BasicTagContent<byte> ReadTagByte(ref byte* pointer)
        {
            BasicTagContent<byte> content = new BasicTagContent<byte>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data = *pointer;
            pointer++;
            return content;
        }

        private static BasicTagContent<long> ReadTagLong(ref byte* pointer)
        {
            BasicTagContent<long> content = new BasicTagContent<long>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data= *(long*)pointer;
            pointer += 8;
            return content;
        }

        private static BasicTagContent<float> ReadTagFloat(ref byte* pointer)
        {
            BasicTagContent<float> content = new BasicTagContent<float>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data = *(float*)pointer;
            pointer += 4;
            return content;
        }

        private static BasicTagContent<double> ReadTagDouble(ref byte* pointer)
        {
            BasicTagContent<double> content = new BasicTagContent<double>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data = *(double*)pointer;
            pointer += 8;
            return content;
        }

        private static BasicTagContent<string> ReadTagString(ref byte* pointer)
        {
            BasicTagContent<string> content = new BasicTagContent<string>();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            content.Data = ReadString(ref pointer);
            pointer += content.Data.Length;
            return content;
        }

        
    }
}
