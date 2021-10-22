using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.Manually
{
    public class ManuallyData
    {
        public int ATNTAmount;

        public int BTNTAmount;

        public Space3D ATNT;

        public Space3D BTNT;

        public Surface2D Destination;

        public PearlEntity Pearl;

        public ManuallyData(int aTNTAmount , int bTNTAmount , Space3D aTNT , Space3D bTNT , Surface2D destination , PearlEntity pearl)
        {
            ATNTAmount = aTNTAmount;
            BTNTAmount = bTNTAmount;
            ATNT = aTNT;
            BTNT = bTNT;
            Destination = destination;
            Pearl = pearl;
        }
    }
}
