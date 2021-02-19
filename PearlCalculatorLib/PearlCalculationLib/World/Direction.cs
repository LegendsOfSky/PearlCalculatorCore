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

        public static bool TryParse(string s , out Direction direction)
        {
            switch(s)
            {
                case "N":
                case "North":
                    direction = Direction.North;
                    return true;
                case "S":
                case "South":
                    direction = Direction.South;
                    return true;
                case "E":
                case "East":
                    direction = Direction.East;
                    return true;
                case "W":
                case "West":
                    direction = Direction.West;
                    return true;
                case "NW":
                case "NorthWest":
                case "North West":
                    direction = Direction.NorthWest;
                    return true;
                case "NE":
                case "NorthEast":
                case "North East":
                    direction = Direction.NorthEast;
                    return true;
                case "SW":
                case "SouthWest":
                case "South West":
                    direction = Direction.SouthWest;
                    return true;
                case "SE":
                case "SouthEast":
                case "South East":
                    direction = Direction.SouthEast;
                    return true;
                default:
                    direction = Direction.None;
                    return false;
            }
            
        }
    }

    public static class DirectionUtils
    {
        public static Direction FormName(string name) => Enum.TryParse<Direction>(name, out var value) ? value : Direction.None;
    }
}
