using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Collections;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    public static class Calculation
    {
        private static PearlEntity PearlSimulation(int redTNT , int blueTNT , int ticks , Direction direction)
        {
            //Simulating the pearl and return the end point
            PearlEntity pearlEntity = (PearlEntity)new PearlEntity(Data.Pearl).AddPosition(Data.PearlOffset);

            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            
            pearlEntity.Motion += redTNT * redTNTVector + blueTNT * blueTNTVector;
            
            for(int i = 0; i < ticks; i++)
                pearlEntity.Tick();
            
            return pearlEntity;
        }

        /// <summary>
        /// Calculate all the suitable TNT combination
        /// <para>There are some other parameters you had to input in <see cref="Data"/> for it to work correctly</para>
        /// <para>See more detail in <see cref="Data"/></para>
        /// </summary>
        /// <param name="maxTicks">The maximum time the Ender Pearl allowed to travel</param>
        /// <param name="maxDistance">The maximum difference between Ender Pearl drop off and Destination in each axis</param>
        /// <returns>Returns a true or false value indicates whether the calculation is correctly executed or not
        /// <para>TNT combination result will be stored into <see cref="Data.TNTResult"/></para>
        /// </returns>
        public static bool CalculateTNTAmount(int maxTicks , double maxDistance)
        {
            int redTNT , blueTNT;
            double divider = 0;
            Direction direction;
            Space3D distance , trueDistance , truePosition;
            
            truePosition = Data.Pearl.Position + Data.PearlOffset.ToSpace3D();
            trueDistance = Data.Destination - truePosition;

            //trueDistance with zero in X and Z axis triggers DevidedByZeroEception
            if(trueDistance == 0)
                return false;
            
            Data.TNTResult.Clear();
            direction = DirectionUtils.GetDirection(truePosition.WorldAngle(Data.Destination));
            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            
            for(int tick = 1; tick <= maxTicks; tick++)
            {
                //Factorization trueDistance to get a easier calculation
                divider += Math.Pow(0.99 , tick - 1);
                distance = trueDistance / divider;

                //Calculate redTNT and blueTNT through simultaneous equations
                redTNT = Convert.ToInt32(Math.Round((distance.Z * blueTNTVector.X - distance.X * blueTNTVector.Z) / (redTNTVector.Z * blueTNTVector.X - blueTNTVector.Z * redTNTVector.X)));
                blueTNT = Convert.ToInt32(Math.Round((distance.X - redTNT * redTNTVector.X) / blueTNTVector.X));
                
                //Run through the loops to get a better result around the redTNT and blueTNT
                //It is not perfect dude to the round up
                for(int r = -5; r <= 5; r++)
                {
                
                    for(int b = -5; b <= 5; b++)
                    {
                        //Simulate the pearl and get it's end point
                        PearlEntity pearlEntity = PearlSimulation(redTNT + r , blueTNT + b , tick , direction);
                        
                        //Only the pearl with a difference smaller than 5 will pass the check
                        if((pearlEntity.Position - Data.Destination).ToSurface2D().Absolute() <= 5)
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
                            
                            //Bypass MaxTNT check if it is set to 0
                            //If MaxTNT is greater than 0 , only add those result which is lesser than MaxTNT
                            //If both redTNT and blueTNT are not greater or equal than zero , ignore them
                            if((Data.MaxTNT <= 0 || (result.Blue <= Data.MaxTNT && result.Red <= Data.MaxTNT)) && result.Blue >= 0 && result.Red >= 0)
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

        /// <summary>
        /// Calculate the trace of the Ender Pearl
        /// <para>There are some other parameters you had to input in the <see cref="Data"/> class for it to work correctly</para>
        /// <para>See more detail in <see cref="Data"/> class</para>
        /// </summary>
        /// <param name="redTNT">The Amount of Red TNT for Accelerating Ender Pearl</param>
        /// <param name="blueTNT">The Amount of Blue TNT for Accelerating Ender Pearl</param>
        /// <param name="ticks">The Maximum Tick the Ender Pearl Allowed to travel</param>
        /// <param name="direction">The accelerating Direction of the Ender Pearl. Only Allow North, South, East, West</param>
        /// <returns>Return A List of Entity contains the Motion and Position in each Ticks</returns>
        public static List<Entity> CalculatePearlTrace(int redTNT , int blueTNT , int ticks , Direction direction)
        {
            List<Entity> result = new List<Entity>();
            PearlEntity pearl = new PearlEntity((PearlEntity)Data.Pearl.AddPosition(Data.PearlOffset));
            
            CalculateTNTVector(direction , out Space3D redTNTVector , out Space3D blueTNTVector);
            pearl.Motion += redTNT * redTNTVector + blueTNT * blueTNTVector;
            result.Add(new PearlEntity(pearl));
            
            for(int i = 0; i < ticks; i++)
            {
                pearl.Tick();
                result.Add(new PearlEntity(pearl));
            }
            
            return result;
        }

        /// <summary>
        /// Calculate the combination of TNT
        /// </summary>
        /// <param name="redTNT">The amount of red TNT</param>
        /// <param name="blueTNT">The amount of blue TNT</param>
        /// <param name="tntCombination">Output the TNT combination</param>
        /// <returns>Returns a true or false value indicates whether the calculation is correctly executed or not</returns>
        public static bool CalculateTNTConfiguration(int redTNT , int blueTNT , out BitArray tntCombination)
        {
            int indexCount = Data.RedTNTConfiguration.Count + Data.BlueTNTConfiguration.Count;
            tntCombination = new BitArray(indexCount);
            
            //How can i sort it if i don't know anything about the configuration
            if(Data.RedTNTConfiguration.Count == 0 && Data.BlueTNTConfiguration.Count == 0)
                return false;
            
            BitArray redBitArray = new BitArray(indexCount);
            BitArray blueBitArray = new BitArray(indexCount);
            List<int> sortedRedConfig = new List<int>(Data.RedTNTConfiguration);
            List<int> sortedBlueConfig = new List<int>(Data.BlueTNTConfiguration);
            
            //Sorts the list from large to small
            sortedRedConfig.Sort((a , b) => a >= b ? (a == b ? 0 : -1) : 1);
            sortedBlueConfig.Sort((a , b) => a >= b ? (a == b ? 0 : -1) : 1);
            
            for(int i = 0; i < sortedRedConfig.Count; i++)
            {
            
                if(redTNT >= sortedRedConfig[i])
                {
                    redTNT -= sortedRedConfig[i];
                    //Sets the corresponding bit to true according to the Data.RedTNTConfiguration
                    redBitArray.Set(Data.RedTNTConfiguration.FindIndex(x => x == sortedRedConfig[i]) , true);
                }
                else
                    redBitArray.Set(Data.RedTNTConfiguration.FindIndex(x => x == sortedRedConfig[i]) , false);
            }
            
            for(int i = 0; i < sortedBlueConfig.Count; i++)
            {
            
                if(blueTNT >= sortedBlueConfig[i])
                {
                    blueTNT -= sortedBlueConfig[i];
                    //Sets the corresponding bit to true according to the Data.BlueTNTConfiguration
                    blueBitArray.Set(Data.BlueTNTConfiguration.FindIndex(x => x == sortedBlueConfig[i]) , true);
                }
                else
                    blueBitArray.Set(Data.BlueTNTConfiguration.FindIndex(x => x == sortedBlueConfig[i]) , false);
            }
            
            //Combine those result into tntCombination
            tntCombination.Or(redBitArray);
            blueBitArray.LeftShift(Data.RedTNTConfiguration.Count);
            tntCombination.Or(blueBitArray);
            
            return true;
        }

        /// <summary>
        /// Calculate the accelerating vector of each Blue and Red TNT in given Direction
        /// </summary>
        /// <param name="direction">The Acceleration Direction of the Ender Pearl</param>
        /// <param name="redTNTVector">Return am accelerating vector of Red TNT</param>
        /// <param name="blueTNTVector">Return an accelerating vector of Blue TNT</param>
        public static void CalculateTNTVector(Direction direction , out Space3D redTNTVector , out Space3D blueTNTVector)
        {
            Space3D aVector , bVector;

            Space3D pearlPosition = Data.PearlOffset.ToSpace3D();
            
            redTNTVector = blueTNTVector = Space3D.zero;
            pearlPosition.Y = Data.Pearl.Position.Y;
            
            if(direction.IsSouth())
            {
                aVector = VectorCalculation.CalculateMotion(pearlPosition , Data.NorthEastTNT);
                bVector = VectorCalculation.CalculateMotion(pearlPosition , Data.NorthWestTNT);
                
                //There is only one TNT which does not need to be recalibrate
                //The following method is used to check which TNT does not need any recalibration
                if(Data.DefaultBlueDuper.IsNorth())
                {
                    //Calculate the vector of the TNT which does not need to be recalibrate
                    blueTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultBlueDuper));

                    //By canceling each other
                    //We can get the vector of the remaining TNT
                    redTNTVector = aVector + bVector - blueTNTVector;
                }
                else if(Data.DefaultRedDuper.IsNorth())
                {
                    redTNTVector = VectorCalculation.CalculateMotion(pearlPosition , TNTDirectionToCoordinate(Data.DefaultRedDuper));
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
