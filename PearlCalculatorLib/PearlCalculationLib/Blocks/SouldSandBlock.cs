using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SouldSandBlock : Block
    {
        public override Space3D Size => new Space3D(1, 0.875, 1);

        public SouldSandBlock(Space3D pos) : base(pos)
        {

        }
    }
}
