using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace PearlCalculatorLib.PearlCalculationLib
{
    public class Direction
    {
        public bool isNorth;
        public bool isSouth;
        public bool isEast;
        public bool isWest;
        public bool isNorthWest;
        public bool isNorthEast;
        public bool isSouthWest;
        public bool isSouthEast;

        public bool ConerIsNorth()
        {
            if(isNorth || isNorthWest || isNorthEast)
                return true;
            else
                return false;
        }

        public bool ConerIsSouth()
        {
            if(isSouth || isSouthWest || isSouthEast)
                return true;
            else
                return false;
        }

        public bool ConerIsEast()
        {
            if(isEast || isNorthEast || isSouthEast)
                return true;
            else
                return false;
        }

        public bool ConerIsWest()
        {
            if(isWest || isNorthWest || isSouthWest)
                return true;
            else
                return false;
        }

        public Direction()
        {

        }

        public Direction(string direction)
        {
            ClearDirection();
            switch(direction)
            {
                case "North":
                    isNorth = true;
                    break;
                case "South":
                    isSouth = true;
                    break;
                case "East":
                    isEast = true;
                    break;
                case "West":
                    isWest = true;
                    break;
                case "NorthWest":
                    isNorthWest = true;
                    break;
                case "NorthEast":
                    isNorthEast = true;
                    break;
                case "SouthWest":
                    isSouthWest = true;
                    break;
                case "SouthEast":
                    isSouthEast = true;
                    break;
                default:
                    break;
            }
        }

        public void ClearDirection()
        {
            isNorth = false;
            isSouth = false;
            isEast = false;
            isWest = false;
            isNorthWest = false;
            isNorthEast = false;
            isSouthWest = false;
            isSouthEast = false;
        }
    }
}
