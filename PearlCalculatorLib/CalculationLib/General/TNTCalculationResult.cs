using System;
using System.Collections.Generic;

namespace PearlCalculatorLib.General
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
        public static void SortByDistanceWithWeight(this List<TNTCalculationResult> results)
        {
            results.Sort(TNTCalculationResultSortComparer.ByDistanceWithWeight);
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

    }
}