using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.Extensions
{
    internal static class BitArrayExtension
    {
        public static BitArray LeftShift(this BitArray bitArray)
        {
            bitArray.LeftShift(1);
            return bitArray;
        }

        public static BitArray RightShift(this BitArray bitArray)
        {
            bitArray.RightShift(1);
            return bitArray;
        }

        public static BitArray SetLastBit(this BitArray bitArray , bool value)
        {
            bitArray.Set(0 , value);
            return bitArray;
        }

        public static BitArray SetFirstBit(this BitArray bitArray , bool value)
        {
            bitArray.Set(bitArray.Length - 1 , value);
            return bitArray;
        }
    }
}
