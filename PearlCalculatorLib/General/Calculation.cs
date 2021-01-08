using System;
using System.Collections.Generic;
using System.Text;
using PearlCalculatorLib.CalculationLib;

namespace PearlCalculatorLib.General
{

    public static class Calculation
    {
        public static Pearl PearlSimulation(int redTNT , int blueTNT , int ticks , string direction)
        {
            Pearl pearl = Data.Pearl;
            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            pearl.Vector = redTNT * redTNTVector + blueTNT * blueTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearl.Position += pearl.Vector;
                pearl.Vector *= 0.99;
                pearl.Vector.Y -= 0.03;
            }
            return pearl;
        }

        public static bool CalculateTNTAmount(int maxTicks)
        {
            int redTNT;
            int blueTNT;
            Space3D distance = new Space3D();
            double divider = 0;
            string direction;
            distance = Data.Destination - Data.Pearl.Position;

            if(Math.Abs(distance.X) == 0 && Math.Abs(distance.Z) == 0)
            {
                return false;
            }

            Data.TNTResult.Clear();
            direction = Data.Pearl.Position.Direction(Data.Pearl.Position.Angle(Data.Destination));
            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);

            for(int tick = 1; tick <= maxTicks; tick++)
            {
                divider += Math.Pow(0.99 , tick - 1);
                distance = (Data.Destination - Data.Pearl.Position) / divider;
                redTNT = Convert.ToInt32(Math.Round((distance.Z * blueTNTVector.X - distance.X * blueTNTVector.Z) / (redTNTVector.Z * blueTNTVector.X - blueTNTVector.Z * redTNTVector.X)));
                blueTNT = Convert.ToInt32(Math.Round((distance.X - redTNT * redTNTVector.X) / blueTNTVector.X));
                for(int r = -5; r <= 5; r++)
                {
                    for(int b = -5; b <= 5; b++)
                    {
                        Pearl pearl = PearlSimulation(redTNT + r , blueTNT + b , tick , direction);
                        if(Math.Abs(pearl.Position.X - Data.Destination.X) <= 10 && Math.Abs(pearl.Position.Z - Data.Destination.Z) <= 10)
                        {
                            TNTCalculationResult result = new TNTCalculationResult
                            {
                                Distance = pearl.Position.Distance2D(Data.Destination) ,
                                Tick = tick ,
                                Blue = blueTNT + b ,
                                Red = redTNT + r ,
                                TotalTNT = blueTNT + b + redTNT + r
                            };
                            if(Data.MaxTNT <= 0)
                                Data.TNTResult.Add(result);
                            else if((blueTNT + b) <= Data.MaxTNT && (redTNT + r) <= Data.MaxTNT)
                                Data.TNTResult.Add(result);
                            if(Data.MaxCalculateTNT < result.TotalTNT)
                                Data.MaxCalculateTNT = result.TotalTNT;
                            if(Data.MaxCalculateDistance < result.Distance)
                                Data.MaxCalculateDistance = result.Distance;
                        }
                    }
                }
            }
            return true;
        }

        public static List<Pearl> CalculatePearlTrace(int redTNT, int blueTNT, int ticks, string direction)
        {
            List<Pearl> result = new List<Pearl>();
            Pearl pearl = new Pearl(Data.Pearl);

            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            pearl.Vector = redTNT * redTNTVector + blueTNT * blueTNTVector + Data.Pearl.Vector;
            result.Add(pearl);

            for(int i = 0; i < ticks; i++)
            {
                pearl.Position += pearl.Vector;
                pearl.Vector *= 0.99;
                pearl.Vector.Y -= 0.03;
                result.Add(pearl);
            }
            return result;
        }

        static void CalculateTNTVector(string direction , out Space3D redTNTVector , out Space3D blueTNTVector)
        {
            string redArray = "";
            string blueArray = "";
            redTNTVector = new Space3D();
            blueTNTVector = new Space3D();
            switch(direction)
            {
                case "North":
                    redArray = Data.NorthArray.Red;
                    blueArray = Data.NorthArray.Blue;
                    break;
                case "South":
                    redArray = Data.SouthArray.Red;
                    blueArray = Data.SouthArray.Blue;
                    break;
                case "East":
                    redArray = Data.EastArray.Red;
                    blueArray = Data.EastArray.Blue;
                    break;
                case "West":
                    redArray = Data.WestArray.Red;
                    blueArray = Data.WestArray.Blue;
                    break;
                default:
                    break;
            }
            switch(redArray)
            {
                case "NE":
                    redTNTVector = Data.NorthEast.InducedVector;
                    break;
                case "NW":
                    redTNTVector = Data.NorthWest.InducedVector;
                    break;
                case "SE":
                    redTNTVector = Data.SouthEast.InducedVector;
                    break;
                case "SW":
                    redTNTVector = Data.SouthWest.InducedVector;
                    break;
                default:
                    break;
            }
            switch(blueArray)
            {
                case "NE":
                    blueTNTVector = Data.NorthEast.InducedVector;
                    break;
                case "NW":
                    blueTNTVector = Data.NorthWest.InducedVector;
                    break;
                case "SE":
                    blueTNTVector = Data.SouthEast.InducedVector;
                    break;
                case "SW":
                    blueTNTVector = Data.SouthWest.InducedVector;
                    break;
                default:
                    break;
            }
        }
    }
}
