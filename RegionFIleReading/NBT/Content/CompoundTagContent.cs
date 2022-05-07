using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbtFileReading.NBT.Content
{
    internal class CompoundTagContent : TagContent<List<ITagContent>>, IEnumerable<ITagContent>, IReadOnlyList<ITagContent>
    {
        public override TagType TagType => TagType.Compound;

        public int Count => Data.Count;

        public ITagContent this[int index] => Data[index];

        public CompoundTagContent() => Data = new List<ITagContent>();

        public IEnumerator<ITagContent> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
