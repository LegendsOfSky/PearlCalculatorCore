using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public abstract class Block
    {
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
    }
}
