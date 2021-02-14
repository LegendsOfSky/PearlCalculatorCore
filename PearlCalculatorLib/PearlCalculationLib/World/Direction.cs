using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace PearlCalculatorLib.PearlCalculationLib.World
{
    [Flags]
    [Serializable]
    public enum Direction
    {
        None  = 0,
        North = 1,
        South = 2,
        East  = 4,
        West  = 8,
        NorthWest = North | West,
        NorthEast = North | East,
        SouthWest = South | West,
        SouthEast = South | East
    }

    public static class DirectionExtension
    {
        public static bool IsNorth(this Direction direction) => (direction & Direction.North) > 0;

        public static bool IsSouth(this Direction direction) => (direction & Direction.South) > 0;

        public static bool IsEast(this Direction direction) => (direction & Direction.East) > 0;

        public static bool IsWest(this Direction direction) => (direction & Direction.West) > 0;
    }

    public static class DirectionUtils
    {
        public static Direction FormName(string name) => Enum.TryParse<Direction>(name, out var value) ? value : Direction.None;
    }
}
