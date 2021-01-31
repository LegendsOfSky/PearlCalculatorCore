using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace PearlCalculatorLib.General
{
    public class Calculation
    {
        private static Pearl PearlSimulation(int redTNT , int blueTNT , int ticks , Direction direction)
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
            Direction direction;

            distance = Data.Destination - Data.Pearl.Position;

            if(Math.Abs(distance.X) == 0 && Math.Abs(distance.Z) == 0)
            {
                return false;
            }
            Data.TNTResult.Clear();
            direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination));
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

        public static List<Pearl> CalculatePearlTrace(int redTNT , int blueTNT , int ticks , Direction direction)
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

        private static void CalculateTNTVector(Direction direction , out Space3D redTNTVector , out Space3D blueTNTVector)
        {
            Space3D pearlPosition = Data.Pearl.Position + Data.PearlOffset;
            Space3D aVector = new Space3D(0 , 0 , 0);
            Space3D bVector = new Space3D(0 , 0 , 0);
            redTNTVector = new Space3D(0 , 0 , 0);
            blueTNTVector = new Space3D(0 , 0 , 0);
            if(direction.IsSouth())
            {
                aVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.NorthEastTNT);
                bVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.NorthWestTNT);
                if(Data.DefaultBlueDuper.IsNorth())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsNorth())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else if(direction.IsNorth())
            {
                aVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.SouthEastTNT);
                bVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.SouthWestTNT);
                if(Data.DefaultBlueDuper.IsSouth())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsSouth())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultRedDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else if(direction.IsEast())
            {
                aVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.SouthWestTNT);
                bVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.NorthWestTNT);
                if(Data.DefaultBlueDuper.IsWest())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsWest())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultRedDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else if(direction.IsWest())
            {
                aVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.SouthEastTNT);
                bVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , Data.NorthEastTNT);
                if(Data.DefaultBlueDuper.IsEast())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsEast())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position + Data.PearlOffset , TNTDirectionToCoordinate(Data.DefaultRedDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else
                throw new ArgumentException();
        }

        private static Space3D TNTDirectionToCoordinate(Direction coner)
        {
            Space3D tntCoordinate = new Space3D(0 , 0 , 0);
            if(coner == Direction.NorthEast)
                tntCoordinate = Data.NorthEastTNT;
            else if(coner == Direction.NorthWest)
                tntCoordinate = Data.NorthWestTNT;
            else if(coner == Direction.SouthEast)
                tntCoordinate = Data.SouthEastTNT;
            else if(coner == Direction.SouthWest)
                tntCoordinate = Data.SouthWestTNT;
            return tntCoordinate;
        }
    }
}
