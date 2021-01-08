using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.CalculationLib
{
    [Serializable]
    public struct Space3D
    {
        public double X;
        public double Y;
        public double Z;

        public static Space3D Zero = new Space3D();

        public Space3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Space3D(Space3D space3D)  :  this(space3D.X , space3D.Y , space3D.Z) { }


        public double Angle(Space3D position2)
        {
            Space3D Distance = new Space3D
            {
                X = position2.X - X,
                Z = position2.Z - Z
            };

            double Phi;

            if (Distance.X == 0 && Distance.Z == 0)
                return 370;

            Phi = Math.Atan(Math.Abs(Distance.X / Distance.Z));

            if (Distance.X > 0)
            {
                if (Distance.Z < 0)
                    return Phi * 180 / Math.PI - 180;
                if (Distance.Z > 0)
                    return -1 * Phi * 180 / Math.PI;
                if (Distance.Z == 0)
                    return 0;
            }

            if (Distance.X < 0)
            {
                if (Distance.Z > 0)
                    return Phi * 180 / Math.PI;
                if (Distance.Z < 0)
                    return 180 - Phi * 180 / Math.PI;
                if (Distance.Z == 0)
                    return 180;
            }

            throw new ArgumentOutOfRangeException();
        }

        public string Direction(double angle)
        {
            if (angle > -45 && angle <= 45)
                return "South";
            if (angle > 45 && angle <= 135)
                return "West";
            if (angle > 135 && angle <= 180)
                return "North";
            if (angle >= -180 && angle <= -135)
                return "North";
            if (angle > -135 && angle <= -45)
                return "East";
            return "Error";
        }

        public double Distance2D(Space3D position2) => Math.Sqrt(Math.Pow(position2.X - X, 2) + Math.Pow(position2.Z - Z, 2));

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
    }
}
