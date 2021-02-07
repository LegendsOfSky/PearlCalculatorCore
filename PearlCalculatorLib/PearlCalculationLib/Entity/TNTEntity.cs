using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    public class TNTEntity : Entity
    {
        public override Space3D Size => new Space3D(0.98, 0.98, 0.98);

        public override void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
