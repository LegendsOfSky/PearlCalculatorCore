using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class ChestBlock : Block
    {
        public override Size GetSize()
        {
            return BlockSize.ChestBlock;
        }
    }
}
