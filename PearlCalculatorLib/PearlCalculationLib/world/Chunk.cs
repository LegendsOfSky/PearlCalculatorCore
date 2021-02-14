using PearlCalculatorLib.PearlCalculationLib.MathLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.World
{
    public class Chunk
    {
        public int X;
        public int Z;

        public Chunk()
        {

        }

        public Chunk(int x , int z)
        {
            X = x;
            Z = z;
        }

        public Chunk(Chunk chunk)
        {
            X = chunk.X;
            Z = chunk.Z;
        }

        public Space3D ToSpace3D() => new Space3D(16 * X , 0 , 16 * Z);

        public Surface2D ToSurface2D() => new Surface2D(16 * X , 16 * Z);

        public static Chunk operator +(Chunk @this , Chunk other) => new Chunk()
        {
            X = @this.X + other.X ,
            Z = @this.Z + other.Z
        };

        public static Chunk operator -(Chunk @this , Chunk other) => new Chunk()
        {
            X = @this.X - other.X ,
            Z = @this.Z - other.Z
        };

        public static Chunk operator *(Chunk @this , int mutiplier) => new Chunk()
        {
            X = @this.X * mutiplier ,
            Z = @this.Z * mutiplier
        };

        public static Chunk operator *(int mutiplier , Chunk @this) => new Chunk()
        {
            X = @this.X * mutiplier ,
            Z = @this.Z * mutiplier
        };

        public static Chunk operator /(Chunk @this , int mutiplier) => new Chunk()
        {
            X = @this.X / mutiplier ,
            Z = @this.Z / mutiplier
        };
    }
}
