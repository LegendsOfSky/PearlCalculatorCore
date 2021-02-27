using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    [DefaultBlockSize(nameof(TrapDoorBlock.BlockSize_Closed))]
    public class TrapDoorBlock : Block , IDisableSetAABB
    {
        public static readonly Space3D BlockSize_Closed = new Space3D(1, 0.1875, 1);
        public static readonly Space3D BlockSize_EW = new Space3D(0.1875, 1, 1);
        public static readonly Space3D BlockSize_NS = new Space3D(1, 1, 0.1875);

        private bool isOpened;
        private Direction direction;

        public override Space3D Size
        {
            get
            {
                if (!isOpened)
                    return BlockSize_Closed;

                switch (direction)
                {
                    case Direction.East:
                    case Direction.West:
                        return BlockSize_EW;
                    default:  //N  S and other
                        return BlockSize_NS;
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
