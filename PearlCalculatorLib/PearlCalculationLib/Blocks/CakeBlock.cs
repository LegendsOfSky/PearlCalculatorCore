using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class CakeBlock : Block
    {
        public override Size GetSize()
        {
            return BlockSize.CakeBlock;
        }
    }
}
