using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace RTPearlCalculatorLib.Blocks
{
    public class BrewingStandBlock : Block
    {
        public static readonly Space3D BlockSize = new Space3D(0.125, 0.875, 0.125);
        
        public override Space3D Size => new Space3D(0.125, 0.875, 0.125);

        public BrewingStandBlock(Space3D pos) : base(pos)
        {

        }
    }
}
