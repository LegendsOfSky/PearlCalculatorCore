using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class CampfireBlock : Block
    {
        public override Space3D Size => new Space3D(1, 0.4375, 1);

        public CampfireBlock(Space3D pos) : base(pos)
        {

        }

    }
}
