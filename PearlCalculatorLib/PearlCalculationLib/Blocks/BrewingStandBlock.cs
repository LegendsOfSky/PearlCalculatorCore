using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class BrewingStandBlock : Block
    {
        public override Space3D Size => new Space3D(0.125, 0.875, 0.125);

        public BrewingStandBlock(Space3D pos) : base(pos)
        {

        }
    }
}
