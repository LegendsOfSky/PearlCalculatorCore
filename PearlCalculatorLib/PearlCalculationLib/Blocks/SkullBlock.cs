using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SkullBlock : Block
    {
        private bool isOnWall;
        private Direction direction;

        public SkullBlock()
        {

        }

        public SkullBlock(Direction direction)
        {
            isOnWall = true;
            this.direction = direction;
        }

        public override Size GetSize()
        {
            if(!isOnWall)
                return BlockSize.SkullBlock;
            switch(direction)
            {
                case Direction.South:
                    return BlockSize.SouthWallSkullBlock;
                case Direction.East:
                    return BlockSize.EastWallSkullBlock;
                case Direction.West:
                    return BlockSize.WestWallSkullBlock;
                default:
                    return BlockSize.NorthWallSkullBlock;
            }
        }
    }
}
