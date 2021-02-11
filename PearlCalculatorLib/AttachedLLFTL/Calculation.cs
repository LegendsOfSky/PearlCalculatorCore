using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.world;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace PearlCalculatorLib.AttachedLLFTL
{
    public static class Calculation
    {
        public static void LoadDataFromGeneral()
        {
            Data.DefaultBlueDuper = General.Data.DefaultBlueDuper;
            Data.DefaultRedDuper = General.Data.DefaultRedDuper;
            Data.Destination = General.Data.Destination;
            Data.Direction = General.Data.Direction;
            Data.MaxTNT = General.Data.MaxTNT;
            Data.OriginalNorthEastTNT = General.Data.NorthEastTNT;
            Data.OriginalNorthWestTNT = General.Data.NorthWestTNT;
            Data.OriginalSouthEastTNT = General.Data.SouthEastTNT;
            Data.OriginalSouthWestTNT = General.Data.SouthWestTNT;
            Data.OriginalPearl = General.Data.Pearl;
            Data.PearlOffset = General.Data.PearlOffset;
        }

        public static void CalculateSuitableAttachLocation(Direction direction)
        {
            Chunk origin = Data.OriginalPearl.Position.ToChunk();

        }

        private static PearlEntity PearlSimulation(int redTNT , int blueTNT , int ticks , Direction direction)
        {
            PearlEntity pearlEntity = new PearlEntity(Data.OriginalPearl);
            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            pearlEntity.Motion = redTNT * redTNTVector + blueTNT * blueTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearlEntity.Tick();
            }
            return pearlEntity;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxTicks"></param>
        /// <param name="minChunk">the closest X or Z distance between General FTL and LLFTL</param>
        /// <param name="maxChunk">the max X or Z distance between Genearl FTL and LLFTL</param>
        /// <returns></returns>
        private static Stack<TNTCalculationResult> CalculateTNTAmount(int maxTicks , int minChunk , int maxChunk)
        {
            int redTNT;
            int blueTNT;
            double divider = 0;
            Space3D distance;
            Space3D destination = new Space3D();
            Direction direction;
            Chunk orginChunk = Data.OriginalPearl.Position.ToChunk();
            Stack<TNTCalculationResult> result = new Stack<TNTCalculationResult>();
            distance = destination - Data.OriginalPearl.Position;
            for(int i = minChunk; i <= maxChunk; i++)
            {
                Chunk AABBMax = new Chunk(orginChunk);
                Chunk AABBMin = new Chunk(orginChunk);
                AABBMax.X += i;
                AABBMax.Z += i;
                AABBMin.X += i;
                AABBMin.Z += i;

            }
            if(distance.X == 0 || distance.Z == 0)
                return result;
            else
            {
                Data.TNTResult.Clear();
                direction = Data.OriginalPearl.Position.Direction(Data.OriginalPearl.Position.WorldAngle(destination));
                CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);

                for(int tick = 1; tick <= maxTicks; tick++)
                {
                    divider += Math.Pow(0.99 , tick - 1);
                    distance = (destination - Data.OriginalPearl.Position) / divider;
                    redTNT = Convert.ToInt32(Math.Round((distance.Z * blueTNTVector.X - distance.X * blueTNTVector.Z) / (redTNTVector.Z * blueTNTVector.X - blueTNTVector.Z * redTNTVector.X)));
                    blueTNT = Convert.ToInt32(Math.Round((distance.X - redTNT * redTNTVector.X) / blueTNTVector.X));

                    for(int r = -5; r <= 5; r++)
                    {
                        for(int b = -5; b <= 5; b++)
                        {
                            PearlEntity pearlEntity = PearlSimulation(redTNT + r , blueTNT + b , tick , direction);
                            if(Math.Abs(pearlEntity.Position.X - destination.X) <= 10 && Math.Abs(pearlEntity.Position.Z - destination.Z) <= 10)
                            {
                                TNTCalculationResult temp = new TNTCalculationResult
                                {
                                    Distance = pearlEntity.Position.Distance2D(destination) ,
                                    Tick = tick ,
                                    Blue = blueTNT + b ,
                                    Red = redTNT + r ,
                                    TotalTNT = blueTNT + b + redTNT + r
                                };
                                result.Push(temp);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static void CalculateTNTVector(Direction direction , out Space3D redTNTVector , out Space3D blueTNTVector)
        {
            Space3D aVector = new Space3D(0 , 0 , 0);
            Space3D bVector = new Space3D(0 , 0 , 0);
            redTNTVector = new Space3D(0 , 0 , 0);
            blueTNTVector = new Space3D(0 , 0 , 0);
            Space3D pearlPosition = Data.PearlOffset;
            pearlPosition.Y = Data.OriginalPearl.Position.Y;
            if(direction.IsSouth())
            {
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalNorthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalNorthWestTNT);
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
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalSouthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalSouthWestTNT);
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
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalSouthWestTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalNorthWestTNT);
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
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalSouthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.OriginalNorthEastTNT);
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
                tntCoordinate = Data.OriginalNorthEastTNT;
            else if(coner == Direction.NorthWest)
                tntCoordinate = Data.OriginalNorthWestTNT;
            else if(coner == Direction.SouthEast)
                tntCoordinate = Data.OriginalSouthEastTNT;
            else if(coner == Direction.SouthWest)
                tntCoordinate = Data.OriginalSouthWestTNT;
            return tntCoordinate;
        }
    }

}
