using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.world;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SlabBlock : Block
    {
        public override Space3D Size => new Space3D(1, 0.5, 1);

        public SlabBlock(Space3D pos) : base(pos)
        {

        }
    }
}
