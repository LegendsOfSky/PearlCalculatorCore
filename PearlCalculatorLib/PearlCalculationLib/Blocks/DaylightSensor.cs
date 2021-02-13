﻿using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class DaylightSensor : Block
    {
        public override Space3D Size => new Space3D(1, 0.375, 1);

        public DaylightSensor(Space3D pos) : base(pos)
        {

        }
    }
}