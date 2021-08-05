using System;

namespace PearlCalculatorLib.PearlCalculationLib.MathLib
{
    /// <summary>
    /// Ask Mojang for it's weird API
    /// I have No clue about it
    /// </summary>
    public static class MathHelper
    {
        public static float Sqrt(double value) => (float)Math.Sqrt(value);

        public static double DegreeToRadiant(double degree) => degree * Math.PI / 180;

        public static double RadiantToDegree(double radiant) => radiant * 180 / Math.PI;

        public static bool IsInside(double border1 , double border2 , double num)
        {
            return num <= Math.Max(border1 , border2) && num >= Math.Min(border1 , border2);
        }
    }
}
