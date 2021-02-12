using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.world;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class TrapDoorBlock : Block , IDisableSetAABB
    {

        private bool isOpened;
        private Direction direction;

        public override Space3D Size
        {
            get 
            {
                if (!isOpened)
                    return new Space3D(1, 0.1875, 1);

                switch (direction)
                {
                    case Direction.East:
                    case Direction.West:
                        return new Space3D(0.1875, 1, 1);
                    default:  //N  S and other
                        return new Space3D(1, 1, 0.1875);
                }
            }
        }

        public TrapDoorBlock(Space3D pos) : base(pos)
        {

        }

        public TrapDoorBlock(Space3D pos, Direction direction) : base(pos)
        {
            isOpened = true;
            this.direction = direction;
            InitAABB();
        }

        private void InitAABB()
        {
            if (isOpened)
            {
                AABB = direction switch
                {
                    Direction.North => new AABBBox(Position + new Space3D(0, 0, 0.8125), Position + Space3D.one),
                    Direction.South => new AABBBox(Position, Position + new Space3D(1, 1, 0.1875)),
                    Direction.East  => new AABBBox(Position, Position + new Space3D(0.1875, 1, 1)),
                    _               => new AABBBox(Position + new Space3D(0.8125, 0, 0), Position + Space3D.one),
                };
            }
        }

    }
}
