using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbtFileReading.NBT.Content
{
    internal class StringTagContent : TagContent<string>
    {
        public override TagType TagType => TagType.String;

        public static implicit operator string(StringTagContent tag) => tag.Data;
    }
}
