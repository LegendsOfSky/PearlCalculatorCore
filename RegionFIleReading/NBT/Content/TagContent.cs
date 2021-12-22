using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT.Content
{
    internal abstract class TagContent<T> : ITagContent
    {
        public T Data;
        public string Name { get; set; }
        public abstract TagType TagType { get; }
    }
}
