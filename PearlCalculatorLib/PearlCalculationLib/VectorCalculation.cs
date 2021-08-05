using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.MathLib;

namespace PearlCalculatorLib.PearlCalculationLib
{
    public static class VectorCalculation
    {
        /// <summary>
        /// Calculate the accelerating vector of the TNT
        /// </summary>
        /// <param name="pearlPosition">The Gobal Coordinate of the Ender Pearl(Might Occur Error when it is too far away from the TNT</param>
        /// <param name="tntPosition">The Gobal Coordinate of the TNT(Might Occur Error when it is too far away from the TNT</param>
        /// <returns>Return the accelerating vector of the TNT</returns>
        public static Space3D CalculateMotion(Space3D pearlPosition , Space3D tntPosition)
        {
            //Don't ask me
            //I have No Idea about it
            //Ask Mojang instead
            tntPosition.Y += 0.98F * 0.0625D;
            Space3D distance = pearlPosition - tntPosition;
            double distanceSqrt = MathHelper.Sqrt(distance.DistanceSq());
            double d12 = distanceSqrt / 8;
            Space3D vector = new Space3D(distance.X , pearlPosition.Y + (0.85F * 0.25F) - tntPosition.Y , distance.Z);
            double d13 = MathHelper.Sqrt(vector.DistanceSq());
            vector /= d13;
            double d11 = 1.0D - d12;
            return new Space3D(vector * d11);
        }
    }
}
