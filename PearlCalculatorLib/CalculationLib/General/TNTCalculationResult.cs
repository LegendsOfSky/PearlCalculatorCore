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

    static class TNTCalculationResultSortComparer 
    {
        public static int ByDistanceWithWeight(TNTCalculationResult r1, TNTCalculationResult r2)
        {
            double r1Ranking;
            double r2Ranking;

            r1Ranking = r1.Distance * (101 - Data.TNTWeight) / (Data.MaxCalculateDistance) - (r1.TotalTNT * (Data.TNTWeight + 1) * 80 / Data.MaxCalculateTNT);
            r2Ranking = r2.Distance * (101 - Data.TNTWeight) / (Data.MaxCalculateDistance) - (r2.TotalTNT * (Data.TNTWeight + 1) * 80 / Data.MaxCalculateTNT);

            if (r1Ranking < r2Ranking)
                return -1;
            else if(r1Ranking > r2Ranking)
                return 1;
            return 0;
        }

        public static int ByTick(TNTCalculationResult r1, TNTCalculationResult r2)
        {
            if(r1.Tick < r2.Tick)
                return -1;
            else if (r1.Tick > r2.Tick)
                return 1;

            // r1.tick == r2.tick
            return ByDefault(r1 , r2);
        }

        public static int ByTotal(TNTCalculationResult r1, TNTCalculationResult r2)
        {
            if(r1.TotalTNT < r2.TotalTNT)
                return -1;
            else if(r1.TotalTNT > r2.TotalTNT)
                return 1;

            // r1.TotalTNT == r2.TotalTNT
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