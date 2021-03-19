using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    public static class Calculation
    {
        private static PearlEntity PearlSimulation(int redTNT , int blueTNT , int ticks , Direction direction)
        {
            PearlEntity pearlEntity = new PearlEntity(Data.Pearl);
            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            pearlEntity.Motion = redTNT * redTNTVector + blueTNT * blueTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearlEntity.Tick();
            }
            return pearlEntity;
        }

        public static bool CalculateTNTAmount(int maxTicks , double maxDistance)
        {
            int redTNT;
            int blueTNT;
            Space3D distance;
            double divider = 0;
            Direction direction;

            distance = Data.Destination - Data.Pearl.Position;

            if(Math.Abs(distance.X) == 0 && Math.Abs(distance.Z) == 0)
                return false;
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
                        PearlEntity pearlEntity = PearlSimulation(redTNT + r , blueTNT + b , tick , direction);
                        if(Math.Abs(pearlEntity.Position.X - Data.Destination.X) <= maxDistance && Math.Abs(pearlEntity.Position.Z - Data.Destination.Z) <= maxDistance)
                        {
                            TNTCalculationResult result = new TNTCalculationResult
                            {
                                Distance = pearlEntity.Position.Distance2D(Data.Destination) ,
                                Tick = tick ,
                                Blue = blueTNT + b ,
                                Red = redTNT + r ,
                                TotalTNT = blueTNT + b + redTNT + r ,
                                Pearl = pearlEntity
                                
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

            Data.TNTResult = Data.TNTResult.Where(result => result.Blue >= 0 && result.Red >= 0).ToList();
            return true;
        }

        public static List<Entity> CalculatePearlTrace(int redTNT , int blueTNT , int ticks , Direction direction)
         {
            List<Entity> result = new List<Entity>();
            PearlEntity pearl = new PearlEntity(Data.Pearl);

            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            pearl.Motion = redTNT * redTNTVector + blueTNT * blueTNTVector + Data.Pearl.Motion;
            result.Add(new PearlEntity(pearl));
            for(int i = 0; i < ticks; i++)
            {
                pearl.Tick();
                result.Add(new PearlEntity(pearl));
            }
            return result;
        }

        public static void CalculateTNTVector(Direction direction , out Space3D redTNTVector , out Space3D blueTNTVector)
        {
            Space3D aVector = new Space3D(0 , 0 , 0);
            Space3D bVector = new Space3D(0 , 0 , 0);
            redTNTVector = new Space3D(0 , 0 , 0);
            blueTNTVector = new Space3D(0 , 0 , 0);
            Space3D pearlPosition = Data.PearlOffset.ToSpace3D();
            pearlPosition.Y = Data.Pearl.Position.Y;
            if(direction.IsSouth())
            {
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.NorthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.NorthWestTNT);
                if(Data.DefaultBlueDuper.IsNorth())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsNorth())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else if(direction.IsNorth())
            {
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.SouthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.SouthWestTNT);
                if(Data.DefaultBlueDuper.IsSouth())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsSouth())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultRedDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else if(direction.IsEast())
            {
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.SouthWestTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.NorthWestTNT);
                if(Data.DefaultBlueDuper.IsWest())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsWest())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultRedDuper));
                    blueTNTVector = aVector + bVector - redTNTVector;
                }
            }
            else if(direction.IsWest())
            {
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.SouthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.NorthEastTNT);
                if(Data.DefaultBlueDuper.IsEast())
                {
                    blueTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsEast())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultRedDuper));
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
