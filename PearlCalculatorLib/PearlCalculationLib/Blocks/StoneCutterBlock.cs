using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;

namespace PearlCalculatorLib.PearlCalculationLib.Blocks
{
    public class StoneCutterBlock : Block
    {
        public override Size GetSize()
        {
            return BlockSize.StoneCutterBlock;
        }
    }
}
