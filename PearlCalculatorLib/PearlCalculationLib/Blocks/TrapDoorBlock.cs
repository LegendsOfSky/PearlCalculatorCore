using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class TrapDoorBlock : Block
    {
        private bool isOpened;
        private Direction direction;

        public TrapDoorBlock()
        {

        }

        public TrapDoorBlock(bool isOpened)
        {
            this.isOpened = isOpened;
        }

        public TrapDoorBlock(Direction direction)
        {
            this.direction = direction;
        }

        public override Size GetSize()
        {
            if(!isOpened)
                return BlockSize.BottomTrapDoorBlock;
            switch(direction)
            {
                case Direction.South:
                    return BlockSize.SouthOpenTrapDoorBlock;
                case Direction.East:
                    return BlockSize.EastOpenTrapDoorBlock;
                case Direction.West:
                    return BlockSize.WestOpenTrapDoorBlock;
                default:
                    return BlockSize.NorthOpenTrapDoorBlock;
            }
        }
    }
}
