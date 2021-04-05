using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.General;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
        public static Space3D CalculateMotion(Space3D pearlPosition, Space3D tntPosition)
        {
            tntPosition.Y += 0.0612500011921;
            Space3D distance = pearlPosition - tntPosition;
            double distanceSqrt = MathHelper.Sqrt(distance.DistanceSq());
            double d12 = distanceSqrt / 8;
            Space3D vector = new Space3D(distance.X , (pearlPosition.Y + 0.25 * 0.85) - tntPosition.Y , distance.Z);
            double d13 = MathHelper.Sqrt(vector.DistanceSq());
            vector /= d13;
            double d11 = (1 - d12);
            return new Space3D(vector * d11);
        }
    }
}
