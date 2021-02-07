using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class ChestBlock : Block
    {
        public override Space3D Size => new Space3D(0.8, 0.9, 0.8);

        public ChestBlock(Space3D pos) : base(pos)
        {

        }
    }
}
