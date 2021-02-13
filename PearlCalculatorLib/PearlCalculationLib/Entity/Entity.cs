using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public abstract class Entity
    {
        public Space3D Motion;
        public Space3D Position;

        public abstract Space3D Size { get; }

        private AABBBox _aabb = new AABBBox();
        public virtual AABBBox AABB => _aabb.ReSize(Position, Position + Size);

        public abstract void Tick();
    }
}
