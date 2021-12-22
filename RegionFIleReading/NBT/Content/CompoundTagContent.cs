using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT.Content
{
    internal class CompoundTagContent : TagContent<ITagContent>
    {
        internal override TagType TagType => TagType.Compound;
    }
}
