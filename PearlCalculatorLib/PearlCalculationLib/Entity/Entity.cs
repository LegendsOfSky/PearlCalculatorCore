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
    }

    public static class EntityExtension
    {
        public static T WithPosition<T>(this T entity , double x , double y , double z) where T : Entity
        {
            entity.Position = new Space3D(x , y , z);
            return entity;
        }

        public static T WithMotion<T>(this T entity, double x, double y, double z) where T : Entity
        {
            entity.Motion = new Space3D(x, y, z);
            return entity;
        }

        public static T AddPosition<T>(this T entity, Space3D postion) where T : Entity
        {
            entity.Position += postion;
            return entity;
        }

        public static T AddPosition<T>(this T entity, Surface2D postion) where T : Entity
        {
            entity.Position += postion.ToSpace3D();
            return entity;
        }

        public static T AddMotion<T>(this T entity, Space3D motion) where T : Entity
        {
            entity.Motion += motion;
            return entity;
        }
    }
}
