using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using RTPearlCalculatorLib.Blocks;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;

namespace RTPearlCalculatorLib.AttachedLLFTL
{
    public static class Calculation
    {
        private static List<Type> _blocksType;
        static List<Type> BlocksType
        {
            get
            {
                if(_blocksType is {})
                    return _blocksType;

                var blockBaseType = typeof(Block);
                _blocksType = new List<Type>();
                var types = Assembly.GetAssembly(blockBaseType).GetExportedTypes();

                foreach (var type in types)
                {
                    if(type.BaseType == blockBaseType)
                        _blocksType.Add(type);
                }

                return _blocksType;
            }
        }

        public static void CalculateSuitableAttachLocation(int maxTick , Direction direction , int minChunkDistance , int maxChunkDistance)
        {
            PearlEntity pearl = PearlCalculatorLib.General.Data.Pearl.DeepClone();
            Space3D NorthWestTNT = PearlCalculatorLib.General.Data.NorthWestTNT;
            Space3D NorthEastTNT = PearlCalculatorLib.General.Data.NorthEastTNT;
            Space3D SouthWestTNT = PearlCalculatorLib.General.Data.SouthWestTNT;
            Space3D SouthEastTNT = PearlCalculatorLib.General.Data.SouthEastTNT;
            Stack<Surface2D> spot = CalculateSpot(direction , minChunkDistance , maxChunkDistance , pearl.Position.ToSurface2D());
            foreach(var temp in spot)
            {
                PearlCalculatorLib.General.Data.Destination = temp.ToSpace3D();
                PearlCalculatorLib.General.Data.Pearl = pearl.DeepClone();
                PearlCalculatorLib.General.Data.NorthWestTNT = NorthWestTNT;
                PearlCalculatorLib.General.Data.NorthEastTNT = NorthEastTNT;
                PearlCalculatorLib.General.Data.SouthWestTNT = SouthWestTNT;
                PearlCalculatorLib.General.Data.SouthEastTNT = SouthEastTNT;
                PearlCalculatorLib.General.Calculation.CalculateTNTAmount(maxTick , 0.5);
                List<TNTCalculationResult> possileResult = PearlCalculatorLib.General.Data.TNTResult.Where(t =>
                {
                    switch(direction)
                    {
                        case Direction.North:
                            return t.Pearl.Position.Z / 16 < Math.Round(t.Pearl.Position.Z / 16);
                        case Direction.South:
                            return t.Pearl.Position.Z / 16 > Math.Round(t.Pearl.Position.Z / 16);
                        case Direction.West:
                            return t.Pearl.Position.X / 16 < Math.Round(t.Pearl.Position.X / 16);
                        default:
                            return t.Pearl.Position.X / 16 > Math.Round(t.Pearl.Position.X / 16);
                    }
                }).ToList();
                Data.LLFTLResult = CheckLLFTLType(RefineTNTResult(direction , possileResult));
            }
        }

        private static Stack<TNTAndRequireButtonBlockResult> CheckLLFTLType(Stack<TNTAndRequireButtonBlockResult> unrefineResult)
        {
            Stack<TNTAndRequireButtonBlockResult> result = new Stack<TNTAndRequireButtonBlockResult>();
            foreach(var temp in unrefineResult)
            {
                if(temp.Pearl.Position.ToSurface2D() - temp.Pearl.Position.ToChunk().ToSurface2D() <= 0.5)
                    temp.Type = LLFTLType.AntiBlindSpot;
                else
                    temp.Type = LLFTLType.EnhanceRange;
                result.Push(temp);
            }
            return result;
        }

        private static Stack<TNTAndRequireButtonBlockResult> RefineTNTResult(Direction direction , List<TNTCalculationResult> tntResult)
        {
            Stack<TNTAndRequireButtonBlockResult> result = new Stack<TNTAndRequireButtonBlockResult>();
            foreach(var temp in tntResult)
            {
                if(CalculateBottomBlock(direction , temp , out List<Type> bottomBlock))
                {
                    TNTAndRequireButtonBlockResult tResult = new TNTAndRequireButtonBlockResult 
                    { 
                        Distance = temp.Distance,
                        Blue = temp.Blue,
                        Red = temp.Red,
                        Pearl = temp.Pearl,
                        Tick = temp.Tick,
                        TotalTNT = temp.TotalTNT,
                        BottomBlock = bottomBlock
                    };
                    result.Push(tResult);
                }
            }
            return result;
        }

        private static bool CalculateBottomBlock(Direction direction , TNTCalculationResult result , out List<Type> bottomBlock)
        {
            bottomBlock = new List<Type>();
            Space3D redTNTVector, blueTNTVector;
            bool isFoundBottomBlocks = false;

            PearlCalculatorLib.General.Data.Pearl = result.Pearl;

            foreach(var item in BlocksType)
            {
                if(Block.TryGetBlockSize(item , out var size))
                {
                    SetTNTYPosition(result.Pearl.Position.Y + size.Y);
                    PearlCalculatorLib.General.Calculation.CalculateTNTVector(direction , out redTNTVector , out blueTNTVector);
                    if(redTNTVector.Y > 0 && redTNTVector.Y <= 0.02 && blueTNTVector.Y > 0 && blueTNTVector.Y <= 0.02)
                    {
                        bottomBlock.Add(item);
                        isFoundBottomBlocks = true;
                    }
                }
            }

            return isFoundBottomBlocks;
        }

        private static void SetTNTYPosition(double height)
        {
            Space3D ne = PearlCalculatorLib.General.Data.NorthEastTNT;
            Space3D nw = PearlCalculatorLib.General.Data.NorthWestTNT;
            Space3D se = PearlCalculatorLib.General.Data.SouthEastTNT;
            Space3D sw = PearlCalculatorLib.General.Data.SouthWestTNT;

            ne.Y = height;
            nw.Y = height;
            se.Y = height;
            sw.Y = height;

            PearlCalculatorLib.General.Data.NorthEastTNT = ne;
            PearlCalculatorLib.General.Data.NorthWestTNT = nw;
            PearlCalculatorLib.General.Data.SouthEastTNT = se;
            PearlCalculatorLib.General.Data.SouthWestTNT = sw;
        }

        private static Stack<Surface2D> CalculateSpot(Direction direction , int minChunkDistance , int maxChunkDistance , Surface2D pearlPosition)
        {
            Stack<Surface2D> localSpot = new Stack<Surface2D>();
            Stack<Surface2D> gobalSpot = new Stack<Surface2D>();
            switch(direction)
            {
                case Direction.North:
                    for(int z = minChunkDistance; z <= maxChunkDistance; z++)
                    {
                        for(int x = 16 * -maxChunkDistance; x <= 16 * maxChunkDistance; x++)
                        {
                            localSpot.Push(new Surface2D(x , 16 * -z));
                        }
                    }
                    break;
                case Direction.South:
                    for(int z = minChunkDistance; z <= maxChunkDistance; z++)
                    {
                        for(int x = 16 * -maxChunkDistance; x <= 16 * maxChunkDistance; x++)
                        {
                            localSpot.Push(new Surface2D(x , 16 * z));
                        }
                    }
                    break;
                case Direction.West:
                    for(int x = minChunkDistance; x <= maxChunkDistance; x++)
                    {
                        for(int z = 16 * -maxChunkDistance; z <= 16 * maxChunkDistance; z++)
                        {
                            localSpot.Push(new Surface2D(16 * -x , z));
                        }
                    }
                    break;
                default:
                    for(int x = minChunkDistance; x <= maxChunkDistance; x++)
                    {
                        for(int z = 16 * -maxChunkDistance; z <= 16 * maxChunkDistance; z++)
                        {
                            localSpot.Push(new Surface2D(16 * x , z));
                        }
                    }
                    break;
            }
            foreach(var temp in localSpot)
            {
                gobalSpot.Push(temp + pearlPosition);
            }
            return gobalSpot;
        }
    }
}
