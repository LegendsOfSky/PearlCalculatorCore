using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class DaylightSensor : Block
    {
        public static readonly Space3D BlockSize = new Space3D(1, 0.375, 1);

        public override Space3D Size => BlockSize;

        public DaylightSensor(Space3D pos) : base(pos)
        {

        }
    }
}
