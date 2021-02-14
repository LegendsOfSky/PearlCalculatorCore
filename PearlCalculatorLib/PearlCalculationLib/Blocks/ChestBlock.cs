using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class ChestBlock : Block
    {
        public static readonly Space3D BlockSize = new Space3D(0.8, 0.9, 0.8);
        
        public override Space3D Size => BlockSize;

        public ChestBlock(Space3D pos) : base(pos)
        {

        }
    }
}
