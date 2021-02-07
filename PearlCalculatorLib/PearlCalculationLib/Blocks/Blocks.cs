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
            _aabb.ReSize(pos, pos + Size);
        }
    }
}
