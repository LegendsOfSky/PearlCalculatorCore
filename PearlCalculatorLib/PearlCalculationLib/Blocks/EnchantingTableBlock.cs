using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class EnchantingTableBlock : Block
    {
        public override Space3D Size => new Space3D(1, 0.75, 1);

        public EnchantingTableBlock(Space3D pos) : base(pos)
        {

        }
    }
}
