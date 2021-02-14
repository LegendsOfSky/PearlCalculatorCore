using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class FenceBlock : Block
    {
        public static readonly Space3D BlockSize = new Space3D(0.25, 1.5, 0.25);

        public override Space3D Size => BlockSize;

        public FenceBlock(Space3D pos) : base(pos)
        {

        }
    }
}
