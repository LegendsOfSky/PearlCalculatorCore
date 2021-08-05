using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public abstract class Entity
    {
        public Space3D Motion;
        public Space3D Position;
        private AABBBox _aabb = new AABBBox();



        public abstract Space3D Size { get; }

        public virtual AABBBox AABB => _aabb.ReSize(Position , Position + Size);

        public abstract void Tick();

        public virtual Entity AddMotion(Space3D motion)
        {
            Motion += motion;
            return this;
        }

        public virtual Entity AddPosition(Space3D position)
        {
            Position += position;
            return this;
        }
        public virtual Entity AddPosition(Surface2D position)
        {
            Position += position.ToSpace3D();
            return this;
        }

        public Entity WithPosition(double x , double y , double z)
        {
            Position = new Space3D(x , y , z);
            return this;
        }

        public Entity WithMotion(double x , double y , double z)
        {
            Motion = new Space3D(x , y , z);
            return this;
        }
    }
}
