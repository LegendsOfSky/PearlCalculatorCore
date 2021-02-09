using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class CakeBlock : Block
    {
        public override Space3D Size => new Space3D(0.875, 0.5, 0.875);

        public CakeBlock(Space3D pos) : base(pos)
        {

        }
    }
}
