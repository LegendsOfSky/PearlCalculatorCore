using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.Blocks;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.AttachedLLFTL
{
    public class TNTAndRequireButtonBlockResult
    {
        public double Distance;
        public int Tick;
        public int Blue;
        public int Red;
        public int TotalTNT;
        public PearlEntity Pearl;
        public List<BlockType> BottomBlock;
        public LLFTLType Type;

        public override string ToString()
        {
            return $"Distance : {Distance} , Tick : {Tick} , Blue : {Blue} , Red : {Red} , Total TNT : {TotalTNT} , Pearl[Motion : {Pearl.Motion} , Position : {Pearl.Position} ] , Type : {Type} , BlockType : {BottomBlock}";
        }
    }
}
