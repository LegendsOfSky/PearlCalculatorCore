using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class StoneCutterBlock : Block
    {
        public override Space3D Size => new Space3D(1, 0.5625, 1);

        public StoneCutterBlock(Space3D pos) : base(pos)
        {

        }
    }
}
