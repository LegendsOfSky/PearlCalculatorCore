using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace RTPearlCalculatorLib.Blocks
{
    public abstract class Block
    {

        private static class BlockUtils
        {
            public static HashSet<Type> BlockSizeBlackList = new HashSet<Type>();
            public static Dictionary<Type, Space3D> BlockSizeOfType = new Dictionary<Type, Space3D>();
        }

        public Space3D Position { get; protected set; }

        public abstract Space3D Size { get; }

        private AABBBox _aabb = new AABBBox();

        /// <summary>
        /// Get AABB Box in global space
        /// </summary>
        public virtual AABBBox AABB
        {
            get => _aabb;
            protected set => _aabb = value;
        }

        protected Block(Space3D pos)
        {
            this.Position = pos;
            if(!(this is IDisableSetAABB))
                InitAABB();
        }

        private void InitAABB()
        {
            Space3D min = Position + new Space3D(0.5 , 0 , 0.5) - new Space3D(0.5 * Size.X , Size.Y , 0.5 * Size.Z);
            Space3D max = Position + new Space3D(0.5 , 0 , 0.5) + new Space3D(0.5 * Size.X , Size.Y , 0.5 * Size.Z);
            _aabb.ReSize(min , max);
        }

        public static bool TryGetBlockSize(Type type, out Space3D size)
        {
            size = Space3D.zero;

            if (type == typeof(Block) || type.BaseType != typeof(Block) || BlockUtils.BlockSizeBlackList.Contains(type))
                return false;

            if (BlockUtils.BlockSizeOfType.TryGetValue(type, out var value))
            {
                size = value;
                return true;
            }

            var attr = type.GetCustomAttribute<DefaultBlockSizeAttribute>();
            var fieldName = attr is null ? "BlockSize" : attr.Name;

            var fieldInfo = type.GetField(fieldName, 
            BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetField);

            if (fieldInfo != null && fieldInfo.FieldType == typeof(Space3D))
            {
                size = (Space3D)fieldInfo.GetValue(null);
                BlockUtils.BlockSizeOfType.Add(type, size);
                return true;
            }

            BlockUtils.BlockSizeBlackList.Add(type);
            return false;
        }
    }
}
