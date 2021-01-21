using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.CalculationLib
{
    public static class MathHelper
    {
        public static float Sqrt(double value)
        {
            return (float)Math.Sqrt(value);
        }

        public static double DegreeToRadiant(double degree)
        {
            return degree * Math.PI / 180;
        }

        public static double RadiantToDegree(double radiant)
        {
            return radiant * 180 / Math.PI;
        }
    }
}
