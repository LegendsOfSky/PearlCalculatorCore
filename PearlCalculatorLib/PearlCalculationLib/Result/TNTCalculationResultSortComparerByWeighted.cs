namespace PearlCalculatorLib.Result
{
    class TNTCalculationResultSortComparerByWeighted
    {
        readonly TNTResultSortByWeightedArgs args;

        public TNTCalculationResultSortComparerByWeighted(TNTResultSortByWeightedArgs args)
        {
            this.args = args;
        }

        public int ByDistance(TNTCalculationResult r1 , TNTCalculationResult r2)
        {
            double r1Ranking;
            double r2Ranking;

            r1Ranking = r1.Distance * (101 - args.TNTWeight) / (args.MaxCalculateDistance) - (r1.TotalTNT * (args.TNTWeight + 1) * 80 / args.MaxCalculateTNT);
            r2Ranking = r2.Distance * (101 - args.TNTWeight) / (args.MaxCalculateDistance) - (r2.TotalTNT * (args.TNTWeight + 1) * 80 / args.MaxCalculateTNT);

            if(r1Ranking < r2Ranking)
                return -1;
            else if(r1Ranking > r2Ranking)
                return 1;
            return 0;
        }

        public int ByTotal(TNTCalculationResult r1 , TNTCalculationResult r2)
        {
            double r1Ranking = r1.TotalTNT * (args.TNTWeight + 1) - r1.TotalTNT * (101 - args.TNTWeight);
            double r2Ranking = r2.TotalTNT * (args.TNTWeight + 1) - r2.TotalTNT * (101 - args.TNTWeight);
            if(r1Ranking < r2Ranking)
                return -1;
            else if(r1Ranking > r2Ranking)
                return 1;
            return TNTCalculationResultSortComparer.ByDefault(r1 , r2);
        }
    }
}
