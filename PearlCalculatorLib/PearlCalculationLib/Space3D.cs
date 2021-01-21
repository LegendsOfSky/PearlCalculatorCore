using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace PearlCalculatorLib.CalculationLib
{
    [Serializable]
    public struct Space3D
    {
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

        public double WorldAngle(Space3D position2)
        {
            Space3D distance = new Space3D();
            distance.X = position2.X - X;
            distance.Z = position2.Z - Z;
            double phi;
            if(distance.X == 0 && distance.Z == 0)
                return 370;
            phi = Math.Atan(Math.Abs(distance.X / distance.Z));
            if(distance.X > 0)
            {
                if(distance.Z > 0)
                    return -Math.Atan(distance.X * distance.Z) * 180 / Math.PI;
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
            Direction direction = PearlCalculationLib.Direction.None;

            if (angle > -135 && angle <= -45)
                direction = PearlCalculationLib.Direction.East;
            else if (angle > -45 && angle <= 45)
                direction = PearlCalculationLib.Direction.South;
            else if (angle > 45 && angle <= 135)
                direction = PearlCalculationLib.Direction.West;
            else if ((angle > 135 && angle <= 180) || (angle > -180 && angle <= -135))
                direction = PearlCalculationLib.Direction.North;
            return direction;
        }

        public double Distance2D(Space3D position2) => Math.Sqrt(Math.Pow(position2.X - X , 2) + Math.Pow(position2.Z - Z , 2));


        public double DistanceSq() => X * X + Y * Y + Z * Z;
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

        public bool IsNorth(Space3D position2)
        {
            if(position2.X > X)
                return true;
            else
                return false;
        }

        public bool IsSouth(Space3D position2)
        {
            if(position2.X < X)
                return true;
            else
                return false;
        }

        public bool IsEast(Space3D position2)
        {
            if(position2.Z < Z)
                return true;
            else
                return false;
        }

        public bool IsWest(Space3D position2)
        {
            if(position2.Z > Z)
                return true;
            else
                return false;
        }

        public double Angle2D(Space3D position2)
        {
            return Math.Atan(position2.Z - Z / position2.X - X) / Math.PI * 180;
        }

        public double AngleElevation(Space3D position2)
        {
            return Math.Atan(position2.Y - Y / position2.X - X) / Math.PI * 180;
        }

        public bool IsOrigin()
        {
            if(X == 0 && Y == 0 && Z == 0)
                return true;
            else
                return false;
        }

        public double AngleInRad(Space3D position2)
        {
            return Math.Atan(position2.Z - Z / position2.X - X);
        }

        public Space3D Rotate(double degree)
        {
            Space3D result;
            double distance = Math.Sqrt(X * X + Z * Z);
            double angle = new Space3D(0 , 0 , 0).AngleInRad(this) + MathHelper.DegreeToRadiant(degree);
            result = PolarCoordinateToSpace3D(new Space3D(0 , 0 , 0).Distance2D(this) , angle);
            result.X = distance * Math.Sin(angle);
            result.Z = distance * Math.Cos(angle);
            return result;
        }

        public Space3D Mirror(bool onXAxis, bool onZAxis)
        {
            Space3D result = new Space3D(0 , Y , 0);
            if(onXAxis)
                result.X = -X;
            if(onZAxis)
                result.Z = -Z;
            return result;
        }
        public Space3D PolarCoordinateToSpace3D(double lenght, double Radinat)
        {
            Space3D result = new Space3D(0 , 0 , 0);
            result.X = lenght * Math.Sin(Radinat);
            result.Z = lenght * Math.Cos(Radinat);
            return result;
        }
    }
}