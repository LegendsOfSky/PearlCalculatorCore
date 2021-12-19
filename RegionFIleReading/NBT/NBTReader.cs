using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT
{
    internal unsafe static class NBTReader
    {
        internal static void Read(ref byte* pointer)
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

        static string ReadName(ref byte* pointer)
        {
            string name = new string((char*)(pointer + 1) , 0 , *pointer);
            pointer += *pointer;
            return name;
        }

        static TagContent<int> ReadTagInt(ref byte* pointer)
        {
            TagContent<int> content = new TagContent<int>();
            pointer += 2;
            content.Name = ReadName(ref pointer);
            content.Data = *(int*)pointer;
            pointer += 4;
            return content;
        }

        static TagContent<byte> ReadTagByte(ref byte* pointer)
        {
            TagContent<byte> content = new TagContent<byte>();
            pointer += 2;
            content.Name = ReadName(ref pointer);
            content.Data = *pointer;
            pointer++;
            return content;
        }

        static TagContent<short> ReadTagShort(ref byte* pointer)
        {
            TagContent<short> content = new TagContent<short>();
            pointer += 2;
            content.Name = ReadName(ref pointer);
            content.Data = *(short*)pointer;
            pointer += 2;
            return content;
        }
    }
}
