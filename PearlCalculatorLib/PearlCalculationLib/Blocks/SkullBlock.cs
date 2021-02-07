using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SkullBlock : Block
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
            AABB = !isOnWall 
                ? AABB.ReSize(Position + new Space3D(0.25, 0, 0.25), Position + new Space3D(0.75, 0.5, 0.75)) 
                : direction switch
            {
                Direction.North => new AABBBox(Position + new Space3D(0.25, 0.25, 0.5), Position + new Space3D(0.75, 0.75, 1)),
                Direction.South => new AABBBox(Position + new Space3D(0.25, 0.25, 0), Position + new Space3D(0.75, 0.75, 0.5)),
                Direction.East  => new AABBBox(Position + new Space3D(0, 0.25, 0.25), Position + new Space3D(0.5, 0.75, 0.75)),
                _               => new AABBBox(Position + new Space3D(0.5, 0.25, 0.25), Position + new Space3D(1, 0.75, 0.75)),
            };
        }
    }
}
