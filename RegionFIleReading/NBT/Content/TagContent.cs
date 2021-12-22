using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT.Content
{
    internal abstract class TagContent<T> : ITagContent
    {
        internal string Name;
        internal T Data;
        internal abstract TagType TagType { get; }
    }
}
