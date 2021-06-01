using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Text;
using System.Xml.Serialization;

namespace PearlCalculatorLib.PearlCalculationLib.World
{
    [Serializable]
    public class Surface2D
    {
        public double X;
        public double Z;
        public static readonly Surface2D zero = new Surface2D();

        public Surface2D()
        {

        }
        public Surface2D(double x , double z)
        {
            X = x;
            Z = z;
        }

        public Space3D ToSpace3D() => new Space3D(X , 0 , Z);

        public override string ToString() => $"Coordinate : {X} , {Z}";

        public bool IsNorth(Surface2D position2) => position2.X > X;

        public bool IsSouth(Surface2D position2) => position2.X < X;

        public bool IsEast(Surface2D position2) => position2.Z < Z;

        public bool IsWest(Surface2D position2) => position2.Z > Z;

        public double Angle(Surface2D position2) => Math.Atan((position2.Z - Z) / (position2.X - X)) / Math.PI * 180;

        public bool IsOrigin() => X == 0 && Z == 0;

        public bool IsInside(Surface2D SouthEastConer , Surface2D NorthWestConer)
        {
            return X < SouthEastConer.X && Z < SouthEastConer.Z && X > NorthWestConer.X && Z > NorthWestConer.Z;
        }

        public double AngleInRad(Surface2D position2) => Math.Atan(position2.Z - Z / position2.X - X);

        public static Space3D FromPolarCoordinate(double lenght , double Radinat)
        {
            Space3D result = new Space3D(0 , 0 , 0)
            {
                X = lenght * Math.Sin(Radinat) ,
                Z = lenght * Math.Cos(Radinat)
            };
            return result;
        }

        public bool AxialDistanceLessThan(double distance) => Math.Abs(X) < distance && Math.Abs(Z) < distance;

        public bool AxialDistanceLargerThan(double distance) => Math.Abs(X) > distance && Math.Abs(Z) > distance;

        public bool AxialDistanceEqualTo(double distance) => Math.Abs(X) == distance && Math.Abs(Z) == distance;

        public bool AxialDistanceLessOrEqualTo(double distance) => AxialDistanceLessThan(distance) || AxialDistanceEqualTo(distance);

        public bool AxialDistanceLargerOrEqualTo(double distance) => AxialDistanceLargerThan(distance) || AxialDistanceEqualTo(distance);

        public Chunk ToChunk() => new Chunk((int)Math.Floor(X / 16) , (int)Math.Floor(Z / 16));

        public Surface2D Round() => new Surface2D(Math.Round(X) , Math.Round(Z));

        public static Surface2D operator +(Surface2D left , Surface2D right) => new Surface2D(left.X + right.X , left.Z + right.Z);

        public static Surface2D operator -(Surface2D left , Surface2D right) => new Surface2D(left.X - right.X , left.Z - right.Z);

        public static bool operator <(Surface2D left , double right) => left.X < right && left.Z < right;

        public static bool operator >(Surface2D left , double right) => left.X > right && left.Z > right;

        public static bool operator <(Surface2D left , int right) => left.X < right && left.Z < right;

        public static bool operator >(Surface2D left , int right) => left.X > right && left.Z > right;

        public static bool operator <=(Surface2D left , double right) => left.X <= right && left.Z <= right;

        public static bool operator >=(Surface2D left , double right) => left.X >= right && left.Z >= right;

        public static bool operator <=(Surface2D left , int right) => left.X <= right && left.Z <= right;

        public static bool operator >=(Surface2D left , int right) => left.X >= right && left.Z >= right;


    }
}
