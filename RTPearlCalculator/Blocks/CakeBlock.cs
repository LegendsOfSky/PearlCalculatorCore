using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace RTPearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class CakeBlock : Block
    {
        public static readonly Space3D BlockSize = new Space3D(0.875, 0.5, 0.875);

        public override Space3D Size => BlockSize;

        public CakeBlock(Space3D pos) : base(pos)
        {

        }
    }
}
