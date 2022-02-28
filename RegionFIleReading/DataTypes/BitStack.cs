using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionFIleReading.DataTypes
{
    /// <summary>
    /// A class use to store a lot of bits and pop at a specificed amount.
    /// <para/>
    /// Note : When bits got pop out, they don't get deleted in the stack.
    /// </summary>
    public class BitStack
    {
        private byte[] _data;
        private int _index;
        private int _bitIndex;

        public int Length => _data.Length - _index;

        public BitStack(byte[] data)
        {
            _data = data;
            _index = 0;
            _bitIndex = 0;
        }
        public BitStack(int[] data)
        {
            _data = new byte[data.Length * sizeof(int)];
            Buffer.BlockCopy(data , 0 , _data , 0 , _data.Length);
            _index = 0;
            _bitIndex = 0;
        }

        public BitStack(long[] data)
        {
            _data = new byte[data.Length * sizeof(long)];
            Buffer.BlockCopy(data , 0 , _data , 0 , _data.Length);
            _index = 0;
            _bitIndex = 0;
        }

        /// <summary>
        /// Pop certain amount of bits out of the stack.
        /// </summary>
        /// <param name="bitCount">Amount of bits need to be popped</param>
        /// <returns></returns>
        public int Pop(int bitCount)
        {
            if(_bitIndex + bitCount <= 8)
            {
                int filter = Convert.ToInt32(Math.Pow(2 , _bitIndex + bitCount) - Math.Pow(2 , _bitIndex));
                int result = (_data[_index] & filter) >> _bitIndex;
                _bitIndex += bitCount;
                return result;
            }
            else
            {
                if(_index + 1 == _data.Length)
                    return 0;

                int filter = Convert.ToInt32(256 - Math.Pow(2 , _bitIndex));
                int result = (_data[_index++] & filter) >> _bitIndex;

                int gotBitCount = 8 - _bitIndex;
                filter = Convert.ToInt32(Math.Pow(2 , bitCount - gotBitCount) - 1);
                result |= (_data[_index] & filter) << gotBitCount;
                _bitIndex = bitCount - gotBitCount;

                return result;
            }
        }

        /// <summary>
        /// Return the all the bits in the stack in a byte[].
        /// <para/>
        /// Note : Popped bits will stay on the stack and able to retrieve by this method.
        /// </summary>
        public byte[] GetWholeStack() => _data;
    }
}
