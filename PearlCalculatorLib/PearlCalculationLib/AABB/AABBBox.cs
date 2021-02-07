using System;
using PearlCalculatorLib.CalculationLib;

namespace PearlCalculatorLib.PearlCalculationLib.AABB
{
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
            Min = Space3D.zero;
            Max = new Space3D(x2 , y2 , z2);
        }

        public AABBBox(double x1 , double y1 , double z1 , double x2 , double y2, double z2)
        {
            Min = new Space3D(x1 , y1 , z1);
            Max = new Space3D(x2 , y2 , z2);
        }

        public AABBBox(Space3D min, Space3D max)
        {
            Min = min;
            Max = max;
        }

        public AABBBox(Space3D max) : this(Space3D.zero, max) { }

        public AABBBox ReSize(Space3D min, Space3D max)
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

        public override bool Equals(object obj)
        {
            return obj is AABBBox s && Equals(s);
        }

        public bool Equals(AABBBox other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return this.Min == other.Min && this.Max == other.Max;
        }

        public static bool operator==(AABBBox left, AABBBox right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(AABBBox left, AABBBox right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return Min.GetHashCode() ^ Max.GetHashCode();
        }
    }
}
