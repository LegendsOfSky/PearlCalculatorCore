using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT
{
    internal class TagContent<T>
    {
        internal string? Name;
        internal T? Data;

        public TagContent()
        {

        }

        public TagContent(string name , T data)
        {
            Name = name;
            Data = data;
        }
    }
}
