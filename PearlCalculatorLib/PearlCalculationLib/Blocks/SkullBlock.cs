using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SkullBlock : Block , IDisableSetAABB
    {
        private bool isOnWall;
        private Direction direction;

        public override Space3D Size => new Space3D(0.5, 0.5, 0.5);

        public SkullBlock(Space3D pos) : base(pos)
        {

        }

        public SkullBlock(Space3D pos, Direction direction) : base(pos)
        {
            isOnWall = true;
            this.direction = direction;
            InitAABB();
        }

        private void InitAABB()
        {



        }
    }
}
