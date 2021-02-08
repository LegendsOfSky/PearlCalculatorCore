using PearlCalculatorLib.CalculationLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class FenceBlock : Block
    {
        public override Space3D Size => new Space3D(0.25 , 1.5 , 0.25);

        public FenceBlock(Space3D pos) : base(pos)
        {

        }
    }
}
