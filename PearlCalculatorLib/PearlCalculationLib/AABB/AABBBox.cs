using System;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorLib.PearlCalculationLib.AABB
{
    [Serializable]
    public class AABBBox : IEquatable<AABBBox>
    {
        public Space3D Min;
        public Space3D Max;



        public Space3D Extents => Max - Min;

        public Space3D Center => (Min + Max) * 0.5;

        public AABBBox()
        {

        }

        public AABBBox(double x2 , double y2 , double z2)
        {
            Min = Space3D.Zero;
            Max = new Space3D(x2 , y2 , z2);
        }

        public AABBBox(double x1 , double y1 , double z1 , double x2 , double y2 , double z2)
        {
            Min = new Space3D(x1 , y1 , z1);
            Max = new Space3D(x2 , y2 , z2);
        }

        public AABBBox(Space3D min , Space3D max)
        {
            Min = min;
            Max = max;
        }

        public AABBBox(Space3D max) : this(Space3D.Zero , max) { }

        public AABBBox ReSize(Space3D min , Space3D max)
        {
            Min = min;
            Max = max;
            return this;
        }

        public bool Contains(Space3D point)
        {
            return MathHelper.IsInside(Min.X , Max.X , point.X) &&
                MathHelper.IsInside(Min.Y , Max.Y , point.Y) &&
                MathHelper.IsInside(Min.Z , Max.Z , point.Z);
        }

        public bool Intersects(AABBBox bounds)
        {
            return Equals(bounds) || (
                Max.X >= bounds.Min.X &&
                Max.Y >= bounds.Min.Y &&
                Max.Z >= bounds.Min.Z &&
                Min.X <= bounds.Max.X &&
                Min.Y <= bounds.Max.Y &&
                Min.Z <= bounds.Max.Z);
        }

        public override bool Equals(object obj) => obj is AABBBox s && Equals(s);

        public bool Equals(AABBBox other)
        {
            if(other is null)
                return false;
            if(ReferenceEquals(this , other))
                return true;
            return Min == other.Min && Max == other.Max;
        }

        public static bool operator ==(AABBBox left , AABBBox right) => left is { } && left.Equals(right);

        public static bool operator !=(AABBBox left , AABBBox right) => left is { } && !left.Equals(right);

        public override int GetHashCode() => Min.GetHashCode() ^ Max.GetHashCode();
    }
}
