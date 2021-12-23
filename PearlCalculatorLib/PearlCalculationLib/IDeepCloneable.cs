namespace PearlCalculatorLib.PearlCalculationLib
{
    public interface IDeepCloneable
    {
        object DeepClone();
    }

    public interface IDeepCloneable<T> : IDeepCloneable
    {
        T DeepClone();
    }
}