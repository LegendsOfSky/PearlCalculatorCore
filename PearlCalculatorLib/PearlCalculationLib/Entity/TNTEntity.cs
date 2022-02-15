using PearlCalculatorLib.PearlCalculationLib.World;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public class TNTEntity : Entity
    {
        public override Space3D GetSize() => new Space3D(0.98 , 0.98 , 0.98);

        public override void Tick()
        {
            Position += Motion;
            Motion *= 0.98;
            Motion.Y -= 0.04;
        }
    }
}
