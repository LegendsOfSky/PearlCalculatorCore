using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    public abstract class Entity
    {
        public Space3D Momemtum;
        public Space3D Position;

        public abstract Space3D Size { get; }

        private AABBBox _aabb = new AABBBox();
        public virtual AABBBox AABB => _aabb.ReSize(Position, Position + Size);

        public abstract void Tick();
    }
}
