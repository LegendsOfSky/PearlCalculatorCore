using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SkullBlock : Block , IDisableSetAABB
    {
        public static readonly Space3D BlockSize = new Space3D(0.5, 0.5, 0.5);

        private bool isOnWall;
        private Direction direction;

        public override Space3D Size => BlockSize;

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
            if(isOnWall)
            {
                switch(direction)
                {
                    case Direction.North:
                        break;
                    case Direction.South:
                        break;
                    case Direction.East:
                        break;
                    case Direction.West:
                        break;
                }
            }

        }
    }
}
