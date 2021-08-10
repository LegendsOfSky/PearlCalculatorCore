using System;

namespace PearlCalculatorLib.PearlCalculationLib.World
{
    [Flags]
    [Serializable]
    public enum Direction
    {
        None = 0,
        North = 1,
        South = 2,
        East = 4,
        West = 8,
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

        public static Direction Invert(this Direction direction)
        {
            int result = 0;

            if(((int)direction & 0b0011) > 0)
                result = ~(int)direction & 0b0011;
            if(((int)direction & 0b1100) > 0)
                result |= ~(int)direction & 0b1100;

            return (Direction)result;
        }

    }

    public static class DirectionUtils
    {
        public static Direction FormName(string name) => Enum.TryParse<Direction>(name , out var value) ? value : Direction.None;

        public static bool TryParse(string s , out Direction result)
        {
            switch(s)
            {
                case "N":
                case "North":
                    result = Direction.North;
                    return true;
                case "S":
                case "South":
                    result = Direction.South;
                    return true;
                case "E":
                case "East":
                    result = Direction.East;
                    return true;
                case "W":
                case "West":
                    result = Direction.West;
                    return true;
                case "NW":
                case "NorthWest":
                case "North West":
                    result = Direction.NorthWest;
                    return true;
                case "NE":
                case "NorthEast":
                case "North East":
                    result = Direction.NorthEast;
                    return true;
                case "SW":
                case "SouthWest":
                case "South West":
                    result = Direction.SouthWest;
                    return true;
                case "SE":
                case "SouthEast":
                case "South East":
                    result = Direction.SouthEast;
                    return true;
                default:
                    result = Direction.None;
                    return false;
            }
        }

        public static Direction GetDirection(double angle)
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
    }
}
