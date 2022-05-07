using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbtFileReading.NBT.Content
{
    internal class ListTagContent<T> : TagContent<List<T>>, IEnumerable<T>, IReadOnlyList<T> where T : ITagContent
    {
        public T this[int index] => Data[index];

        public override TagType TagType => TagType.List;

        public int Count => Data.Count;

        public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
