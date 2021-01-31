using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.Result
{
    static class TNTCalculationResultSortComparer
    {
        public static int ByTick(TNTCalculationResult r1 , TNTCalculationResult r2)
        {
            if(r1.Tick < r2.Tick)
                return -1;
            else if(r1.Tick > r2.Tick)
                return 1;
            return ByDefault(r1 , r2);
        }

        public static int ByTotal(TNTCalculationResult r1 , TNTCalculationResult r2)
        {
            if(r1.TotalTNT < r2.TotalTNT)
                return -1;
            else if(r1.TotalTNT > r2.TotalTNT)
                return 1;
            return ByDefault(r1 , r2);
        }

        public static int ByDefault(TNTCalculationResult r1 , TNTCalculationResult r2)
        {
            if(r1.Distance < r2.Distance)
                return -1;
            else if(r1.Distance > r2.Distance)
                return 1;
            return 0;
        }
    }
}
