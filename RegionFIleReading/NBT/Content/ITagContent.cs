using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT.Content
{
    internal interface ITagContent
    {
        string? Name { get; set; }
    }

    internal static class ITagContentExtension
    {
        public static T? As<T>(this ITagContent tag) where T : class , ITagContent => tag as T;
    }
}
