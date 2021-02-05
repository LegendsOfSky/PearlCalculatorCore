using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class SlabBlock : Block
    {
        public override Size GetSize()
        {
            return BlockSize.SlabBlock;
        }
    }
}
