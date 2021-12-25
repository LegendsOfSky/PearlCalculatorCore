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
        private static CompoundTagContent ReadTag(ref byte* pointer)
        {
            CompoundTagContent compoundTagContent = new CompoundTagContent();
            
            TagType type = (TagType)(*pointer);
            while (type != TagType.Null)
            {
#nullable disable
                switch (type)
                {
                    case TagType.Null:
                        break;
                    case TagType.Int:
                        compoundTagContent.Data.Add(ReadTagInt(ref pointer));
                        break;
                    case TagType.Short:
                        compoundTagContent.Data.Add(ReadTagShort(ref pointer));
                        break;
                    case TagType.Byte:
                        compoundTagContent.Data.Add(ReadTagByte(ref pointer));
                        break;
                    case TagType.Long:
                        compoundTagContent.Data.Add(ReadTagLong(ref pointer));
                        break;
                    case TagType.Float:
                        compoundTagContent.Data.Add(ReadTagFloat(ref pointer));
                        break;
                    case TagType.Double:
                        compoundTagContent.Data.Add(ReadTagDouble(ref pointer));
                        break;
                    case TagType.String:
                        compoundTagContent.Data.Add(ReadTagString(ref pointer));
                        break;
                    case TagType.Compound:
                        compoundTagContent.Data.Add(ReadTagCompound(ref pointer));
                        break;
                    case TagType.List:
                        compoundTagContent.Data.Add(ReadTagList(ref pointer));
                        break;
                    case TagType.IntArray:
                        compoundTagContent.Data.Add(ReadTagIntArray(ref pointer));
                        break;
                    case TagType.ByteArray:
                        compoundTagContent.Data.Add(ReadTagByteArray(ref pointer));
                        break;
                    case TagType.LongArray:
                        compoundTagContent.Data.Add(ReadTagLongArray(ref pointer));
                        break;
                }
#nullable enable
            }
            return compoundTagContent;
        }

        private static string ReadString(ref byte* pointer)
        {
            string name = new string((char*)(pointer + 1) , 0 , *pointer);
            pointer += *pointer;
            return name;
        }

        private static T CreateTag<T>(ref byte* pointer) where T : ITagContent , new()
        {
            T content = new T();
            pointer += 2;
            content.Name = ReadString(ref pointer);
            return content;
        }

        private static IntTagContent ReadTagInt(ref byte* pointer)
        {
            IntTagContent content = CreateTag<IntTagContent>(ref pointer);
            content.Data = *(int*)pointer;
            pointer += 4;
            return content;
        }

        private static ShortTagContent ReadTagShort(ref byte* pointer)
        {
            ShortTagContent content = CreateTag<ShortTagContent>(ref pointer);
            content.Data = *(short*)pointer;
            pointer += 2;
            return content;
        }

        private static ByteTagContent ReadTagByte(ref byte* pointer)
        {
            ByteTagContent content = CreateTag<ByteTagContent>(ref pointer);
            content.Data = *pointer;
            pointer++;
            return content;
        }

        private static LongTagContent ReadTagLong(ref byte* pointer)
        {
            LongTagContent content = CreateTag<LongTagContent>(ref pointer);
            content.Data= *(long*)pointer;
            pointer += 8;
            return content;
        }

        private static FloatTagContent ReadTagFloat(ref byte* pointer)
        {
            FloatTagContent content = CreateTag<FloatTagContent>(ref pointer);
            content.Data = *(float*)pointer;
            pointer += 4;
            return content;
        }

        private static DoubleTagContent ReadTagDouble(ref byte* pointer)
        {
            DoubleTagContent content = CreateTag<DoubleTagContent>(ref pointer);
            content.Data = *(double*)pointer;
            pointer += 8;
            return content;
        }

        private static StringTagContent ReadTagString(ref byte* pointer)
        {
            StringTagContent content = CreateTag<StringTagContent>(ref pointer);
            content.Data = ReadString(ref pointer);
            pointer += content.Data.Length;
            return content;
        }

        private static CompoundTagContent ReadTagCompound(ref byte* pointer)
        {
            CompoundTagContent content = CreateTag<CompoundTagContent>(ref pointer);
            content.Data = ReadTag(ref pointer).Data;
            // WaitForTest : pointer++???
            return content;
        }

        private static ListTagContent<ITagContent> ReadTagList(ref byte* pointer)
        {
            ListTagContent<ITagContent> content = CreateTag<ListTagContent<ITagContent>>(ref pointer);
            // Unfinish : Read List Data
            return content;
        }

        private static IntArrayTagContent ReadTagIntArray(ref byte* pointer)
        {
            IntArrayTagContent content = CreateTag<IntArrayTagContent>(ref pointer);
            // Unfinish : Read IntArray Data
            return content;
        }

        private static ByteArrayTagContent ReadTagByteArray(ref byte* pointer)
        {
            ByteArrayTagContent content = CreateTag<ByteArrayTagContent>(ref pointer);
            //Unfinish : Read ByteArray Data
            return content;
        }

        private static LongArrayTagContent ReadTagLongArray(ref byte* pointer)
        {
            LongArrayTagContent content = CreateTag<LongArrayTagContent>(ref pointer);
            //Unfinish : ReadByteArray Data
            return content;
        }
    }
}
