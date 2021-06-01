using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace PearlCalculatorLib.PearlCalculationLib.World
{
    [Serializable]
    public struct Space3D : IEquatable<Space3D>
    {
        public static readonly Space3D zero = new Space3D();
        public static readonly Space3D one = new Space3D(1 , 1 , 1);

        public double X;
        public double Y;
        public double Z;



        public Space3D(double x , double y , double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Space3D(Space3D space3D) : this(space3D.X , space3D.Y , space3D.Z) { }

        public override string ToString() => $"Coordinate : {X} , {Y} , {Z}";

        public double WorldAngle(Space3D position2)
        {
            Space3D distance = new Space3D
            {
                X = position2.X - X ,
                Z = position2.Z - Z
            };
            if(distance.X == 0 && distance.Z == 0)
                return 370;
            if(distance.X > 0)
            {
                if(distance.Z > 0)
                    return -(Math.Atan(distance.X / distance.Z) * 180 / Math.PI);
                else if(distance.Z == 0)
                    return -90;
                else
                    return -(180 - Math.Atan(-distance.X / distance.Z) * 180 / Math.PI);
            }
            else if(distance.X < 0)
            {
                if(distance.Z > 0)
                    return Math.Atan(-distance.X / distance.Z) * 180 / Math.PI;
                else if(distance.Z == 0)
                    return 90;
                else
                    return 180 - Math.Atan(distance.X / distance.Z) * 180 / Math.PI;
            }
            else
            {
                if(distance.Z > 0)
                    return 0;
                else
                    return 180;
            }
            throw new ArgumentOutOfRangeException();
        }

        public Direction Direction(double angle)
        {
            Direction direction = World.Direction.None;

            if(angle > -135 && angle <= -45)
                direction = World.Direction.East;
            else if(angle > -45 && angle <= 45)
                direction = World.Direction.South;
            else if(angle > 45 && angle <= 135)
                direction = World.Direction.West;
            else if((angle > 135 && angle <= 180) || (angle > -180 && angle <= -135))
                direction = World.Direction.North;
            return direction;
        }

        public double Distance2D(Space3D position2) => Math.Sqrt(Math.Pow(position2.X - X , 2) + Math.Pow(position2.Z - Z , 2));

        public double DistanceSq() => X * X + Y * Y + Z * Z;

        public double Distance(Space3D other)
        {
            var dis3 = this - other;
            dis3.X = Math.Abs(dis3.X);
            dis3.Y = Math.Abs(dis3.Y);
            dis3.Z = Math.Abs(dis3.Z);
            return Math.Sqrt(dis3.DistanceSq());
        }


        public Surface2D ToSurface2D() => new Surface2D(X , Z);

        public bool IsNorth(Space3D position2) => position2.X > X;

        public bool IsSouth(Space3D position2) => position2.X < X;

        public bool IsEast(Space3D position2) => position2.Z < Z;

        public bool IsWest(Space3D position2) => position2.Z > Z;

        public double AngleElevation(Space3D position2) => Math.Atan((position2.Y - Y) / (position2.X - X)) / Math.PI * 180;

        public bool IsOrigin() => X == 0 && Y == 0 && Z == 0;

        public double AngleInRad(Space3D position2) => Math.Atan((position2.Z - Z) / (position2.X - X));

        public Space3D Rotate(double degree)
        {
            Space3D result;
            double distance = Math.Sqrt(X * X + Z * Z);
            double angle = new Space3D(0 , 0 , 0).AngleInRad(this) + MathHelper.DegreeToRadiant(degree);
            result = FromPolarCoordinate(new Space3D(0 , 0 , 0).Distance2D(this) , angle);
            result.X = distance * Math.Sin(angle);
            result.Z = distance * Math.Cos(angle);
            return result;
        }

        public Space3D Mirror(bool onXAxis , bool onZAxis)
        {
            Space3D result = new Space3D(0 , Y , 0);
            if(onXAxis)
                result.X = -X;
            if(onZAxis)
                result.Z = -Z;
            return result;
        }

        public static Space3D FromPolarCoordinate(double lenght , double Radinat)
        {
            Space3D result = new Space3D(0 , 0 , 0)
            {
                X = lenght * Math.Sin(Radinat) ,
                Z = lenght * Math.Cos(Radinat)
            };
            return result;
        }

        public Chunk ToChunk() => new Chunk((int)Math.Floor(X / 16) , (int)Math.Floor(Z / 16));

        public Space3D Round() => new Space3D(Math.Round(X) , Math.Round(Y) , Math.Round(Z));

        public override bool Equals(object obj) => obj is Space3D s && Equals(s);

        public override int GetHashCode() => base.GetHashCode();

        public bool Equals(Space3D other) => X == other.X && Y == other.Y && Z == other.Z;
        public static Space3D operator +(Space3D @this , Space3D other) => new Space3D()
        {
            X = @this.X + other.X ,
            Y = @this.Y + other.Y ,
            Z = @this.Z + other.Z
        };

        public static Space3D operator -(Space3D @this , Space3D other) => new Space3D()
        {
            X = @this.X - other.X ,
            Y = @this.Y - other.Y ,
            Z = @this.Z - other.Z
        };

        public static Space3D operator *(Space3D @this , double mutiplier) => new Space3D()
        {
            X = @this.X * mutiplier ,
            Y = @this.Y * mutiplier ,
            Z = @this.Z * mutiplier
        };

        public static Space3D operator *(double mutiplier , Space3D @this) => new Space3D()
        {
            X = @this.X * mutiplier ,
            Y = @this.Y * mutiplier ,
            Z = @this.Z * mutiplier
        };

        public static Space3D operator *(int mutiplier , Space3D @this) => new Space3D()
        {
            X = @this.X * mutiplier ,
            Y = @this.Y * mutiplier ,
            Z = @this.Z * mutiplier
        };

        public static Space3D operator *(Space3D @this , int mutiplier) => new Space3D()
        {
            X = @this.X * mutiplier ,
            Y = @this.Y * mutiplier ,
            Z = @this.Z * mutiplier
        };

        public static Space3D operator /(int mutiplier , Space3D @this) => new Space3D()
        {
            X = @this.X / mutiplier ,
            Y = @this.Y / mutiplier ,
            Z = @this.Z / mutiplier
        };

        public static Space3D operator /(Space3D @this , int divider) => new Space3D()
        {
            X = @this.X / divider ,
            Y = @this.Y / divider ,
            Z = @this.Z / divider
        };

        public static Space3D operator /(double divider , Space3D @this) => new Space3D()
        {
            X = @this.X / divider ,
            Y = @this.Y / divider ,
            Z = @this.Z / divider
        };

        public static Space3D operator /(Space3D @this , double divider) => new Space3D()
        {
            X = @this.X / divider ,
            Y = @this.Y / divider ,
            Z = @this.Z / divider
        };

        public static bool operator ==(Space3D left , Space3D right) => left.Equals(right);

        public static bool operator >(Space3D left , double right) => left.X > right && left.Y > right && left.Z > right;

        public static bool operator <(Space3D left , double right) => left.X < right && left.Y < right && left.Z < right;

        public static bool operator !=(Space3D left , Space3D right) => !left.Equals(right);
    }
}