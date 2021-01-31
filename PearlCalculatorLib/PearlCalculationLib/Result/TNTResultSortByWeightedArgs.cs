namespace PearlCalculatorLib.Result
{
    public class TNTResultSortByWeightedArgs
    {
        public readonly int TNTWeight;
        public readonly int MaxCalculateTNT;
        public readonly double MaxCalculateDistance;

        public TNTResultSortByWeightedArgs(int tntWeight, int maxCalculateTNT, double maxCalculateDistance)
        {
            (this.TNTWeight, this.MaxCalculateTNT, this.MaxCalculateDistance) = (tntWeight, maxCalculateTNT, maxCalculateDistance);
        }
    }
}
