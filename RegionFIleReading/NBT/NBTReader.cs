using RegionFIleReading.NBT.Content;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT
{
    internal unsafe static class NBTReader
    {
        internal static CompoundTagContent ReadTag(ref byte* pointer)
        {
            CompoundTagContent compoundTagContent = new CompoundTagContent();
            TagType type = (TagType)(*pointer);
            while (type != TagType.Null)
            {
#nullable disable
                type = (TagType)(*pointer);
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
            pointer++;
            return compoundTagContent;
        }

        private static string ReadString(ref byte* pointer)
        {
            string name = new string((sbyte*)(pointer + 1) , 0 , *pointer , Encoding.ASCII);
            pointer += *pointer + 1;
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
            pointer++;
            content.Data = ReadString(ref pointer);
            return content;
        }

        private static CompoundTagContent ReadTagCompound(ref byte* pointer)
        {
            CompoundTagContent content = CreateTag<CompoundTagContent>(ref pointer);
            content.Data = ReadTag(ref pointer).Data;
            return content;
        }

        private static ListTagContent<ITagContent> ReadTagList(ref byte* pointer)
        {
            ListTagContent<ITagContent> contents = CreateTag<ListTagContent<ITagContent>>(ref pointer);
            TagType tagType = (TagType)(*pointer);
            pointer++;

            switch (tagType)
            {
                case TagType.Null:
                    int length = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
                    pointer += (length + 1) * 4;
                    break;
                case TagType.Compound:
                    contents.Data = GetCompoundTagContentsInTagList(ref pointer);
                    break;
                case TagType.List:
                    contents.Data = GetListTagContentsInTaglist(ref pointer);
                    break;

                case TagType.Byte:
                case TagType.Short:
                case TagType.Int:
                case TagType.Long:
                case TagType.Float:
                case TagType.Double:
                case TagType.ByteArray:
                case TagType.String:
                case TagType.IntArray:
                case TagType.LongArray:
                default:
                    throw new NotImplementedException();
            }

            return contents;
        }

        private static List<ITagContent> GetCompoundTagContentsInTagList(ref byte* pointer)
        {
            List<ITagContent> contents = new List<ITagContent>();
            int length = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
            pointer += 4;
            for (int i = 0; i < length; i++)
                contents.Add(ReadTag(ref pointer));
            return contents;
        }

        private static List<ITagContent> GetListTagContentsInTaglist(ref byte* pointer)
        {
            List<ITagContent> contents = new List<ITagContent>();
            ListTagContent<ITagContent> subContent = new ListTagContent<ITagContent>();
            int listLength = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
            pointer += 4;

            for (int i = 0; i < listLength; i++)
            {
                TagType tagType = (TagType)(*pointer);
                switch (tagType)
                {
                    case TagType.Null:
                        int length = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
                        pointer += (length + 1) * 4;
                        break;
                    case TagType.Compound:
                        subContent.Data = GetCompoundTagContentsInTagList(ref pointer);
                        break;
                        //WaitForTest : List in List
                    case TagType.List:
                        subContent.Data = GetListTagContentsInTaglist(ref pointer);
                        break;

                    case TagType.Byte:
                    case TagType.Short:
                    case TagType.Int:
                    case TagType.Long:
                    case TagType.Float:
                    case TagType.Double:
                    case TagType.ByteArray:
                    case TagType.String:
                    case TagType.IntArray:
                    case TagType.LongArray:
                    default:
                        throw new NotImplementedException();
                }
                contents.Add(subContent);
            }

            return contents;
        }

        private static IntArrayTagContent ReadTagIntArray(ref byte* pointer)
        {
            IntArrayTagContent content = CreateTag<IntArrayTagContent>(ref pointer);
            int length = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
            pointer += 4;
            content.Data = new int[length];
            for (int i = 0; i < length; i++)
            {
                content.Data[i] = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
                pointer += 4;
            }
            return content;
        }

        private static ByteArrayTagContent ReadTagByteArray(ref byte* pointer)
        {
            ByteArrayTagContent content = CreateTag<ByteArrayTagContent>(ref pointer);
            int length = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
            pointer += 4;
            content.Data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                content.Data[i] = BinaryPrimitives.ReverseEndianness(*pointer);
                pointer++;
            }
            return content;
        }

        private static LongArrayTagContent ReadTagLongArray(ref byte* pointer)
        {
            LongArrayTagContent content = CreateTag<LongArrayTagContent>(ref pointer);
            int length = BinaryPrimitives.ReverseEndianness(*(int*)pointer);
            pointer += 4;
            content.Data = new long[length];
            for (int i = 0; i < length; i++)
            {
                content.Data[i] = BinaryPrimitives.ReverseEndianness(*(long*)pointer);
                pointer += 8;
            }
            return content;
        }
    }
}
