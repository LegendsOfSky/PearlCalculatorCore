using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.General.Result
{
    public struct TNTCalculationResult
    {
        public double Distance;
        public int Tick;
        public int Blue;
        public int Red;
        public int TotalTNT;

        public override string ToString()
        {
            return $"Ticks:{Tick}, B:{Blue}, R:{Red}, Total:{TotalTNT}";
        }
    }

    public static class TNTCalculationResultExtension
    {
        public static void SortByWeightedDistance(this List<TNTCalculationResult> results)
        {
            results.Sort(TNTCalculationResultSortComparer.ByWeightedDistance);
        }

        public static void SortByDistance(this List<TNTCalculationResult> results)
        {
            results.Sort(TNTCalculationResultSortComparer.ByDefault);
        }

        public static void SortByTick(this List<TNTCalculationResult> results)
        {
            results.Sort(TNTCalculationResultSortComparer.ByTick);
        }

        public static void SortByTotal(this List<TNTCalculationResult> results)
        {
            results.Sort(TNTCalculationResultSortComparer.ByTotal);
        }

        public static void SortByWeightedTotal(this List<TNTCalculationResult> results)
        {
            results.Sort(TNTCalculationResultSortComparer.ByWeightedTotal);
        }

    }
}
