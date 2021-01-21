using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace PearlCalculatorLib.PearlCalculationLib
{
    [Flags]
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
        public static bool ConerIsNorth(this Direction direction) => (direction & Direction.North) > 0;

        public static bool ConerIsSouth(this Direction direction) => (direction & Direction.South) > 0;

        public static bool ConerIsEast(this Direction direction) => (direction & Direction.East) > 0;

        public static bool ConerIsWest(this Direction direction) => (direction & Direction.West) > 0;
    }

    public static class DirectionUtils
    {
        public static Direction FormName(string name) => Enum.TryParse<Direction>(name, out var value) ? value : Direction.None;
    }
}
