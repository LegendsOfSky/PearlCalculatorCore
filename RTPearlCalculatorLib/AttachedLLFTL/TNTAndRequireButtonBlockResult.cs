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
        public double Distance { get; set; }
        public int Tick { get; set; }
        public int Blue { get; set; }
        public int Red { get; set; }
        public int TotalTNT { get; set; }
        public PearlEntity Pearl { get; set; }
        public List<Type> BottomBlock { get; set; }
        public LLFTLType Type { get; set; }

        public override string ToString()
        {
            var bottomBlocksStr = new StringBuilder();

            foreach (var item in BottomBlock)
                bottomBlocksStr.Append($"{item.Name} , ");

            return $"Distance : {Distance} , Tick : {Tick} , Blue : {Blue} , Red : {Red} , Total TNT : {TotalTNT} , Pearl[Motion : {Pearl.Motion} , Position : {Pearl.Position} ] , Type : {Type}\nBlockType : {bottomBlocksStr.ToString()}";
        }
    }
}
