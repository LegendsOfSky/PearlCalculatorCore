namespace PearlCalculatorLib.Result
{
    public class TNTResultSortByWeightedArgs
    {
        public readonly int TNTWeight;
        public readonly int MaxCalculateTNT;
        public readonly double MaxCalculateDistance;

        public TNTResultSortByWeightedArgs(int tntWeight , int maxCalculateTNT , double maxCalculateDistance)
        {
            this.TNTWeight = tntWeight;
            this.MaxCalculateTNT = maxCalculateTNT == 0 ? 1 : maxCalculateTNT;
            this.MaxCalculateDistance = maxCalculateDistance == 0 ? 1 : maxCalculateDistance;
        }
    }
}
