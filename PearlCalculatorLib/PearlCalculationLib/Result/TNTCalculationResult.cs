using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.Result
{
    public struct TNTCalculationResult
    {
        public double Distance { get; set; }
        public int Tick { get; set; }
        public int Blue { get; set; }
        public int Red { get; set; }
        public int TotalTNT { get; set; }
        public PearlEntity Pearl { get; set; }

        public override string ToString() => $"Ticks:{Tick}, B:{Blue}, R:{Red}, Total:{TotalTNT}";
    }

    public static class TNTCalculationResultExtension
    {
        public static void SortByWeightedDistance(this List<TNTCalculationResult> results , TNTResultSortByWeightedArgs args)
        {
            results.Sort(new TNTCalculationResultSortComparerByWeighted(args).ByDistance);
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

        public static void SortByWeightedTotal(this List<TNTCalculationResult> results , TNTResultSortByWeightedArgs args)
        {
            results.Sort(new TNTCalculationResultSortComparerByWeighted(args).ByTotal);
        }

    }
}
