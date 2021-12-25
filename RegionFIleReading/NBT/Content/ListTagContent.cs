using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.NBT.Content
{
    internal class ListTagContent<T> : TagContent<List<TagContent<T>>> where T : ITagContent
    {
        public override TagType TagType => TagType.List;
    }
}
