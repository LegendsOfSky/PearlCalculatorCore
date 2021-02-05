using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;

namespace PearlCalculatorLib.Manually
{
    public static class Calculation
    {
        public static Space3D ATNTPosition;
        public static Space3D BTNTPosition;
        public static Pearl Pearl;

        public static bool CalculateTNTAmount(Space3D destination , int ticks , out List<TNTCalculationResult> result)
        {
            Space3D aTNTVector;
            Space3D bTNTVector;
            double divider = 0;
            int redTNT;
            int blueTNT;
            result = new List<TNTCalculationResult>();
            Space3D distance = destination - Pearl.Position;
            if(Math.Abs(distance.X) == 0 && Math.Abs(distance.Z) == 0)
                return false;
            aTNTVector = VectorCalculation.CalculateMotion(Pearl.Position , ATNTPosition);
            bTNTVector = VectorCalculation.CalculateMotion(Pearl.Position , BTNTPosition);
            for(int i = 0; i < ticks; i++)
            {
                divider += Math.Pow(0.99 , ticks - 1);
                distance = (Pearl.Position - distance) / divider;
                redTNT = Convert.ToInt32(Math.Round((distance.Z * bTNTVector.X - distance.X * bTNTVector.Z) / (aTNTVector.Z * bTNTVector.Z - bTNTVector.Z * aTNTVector.X)));
                blueTNT = Convert.ToInt32(Math.Round((distance.X - redTNT * aTNTVector.X) / bTNTVector.X));
                for(int r = -5; r <= 5; r++)
                {
                    for(int b = -5; b <= 5; b++)
                    {
                        Pearl aPearl = PearlSimulation(redTNT + r , blueTNT + b , ticks , aTNTVector , bTNTVector);
                        if(Math.Abs(aPearl.Position.X - destination.X) <= 10 && Math.Abs(aPearl.Position.Z - destination.Z) <= 10)
                        {
                            TNTCalculationResult tResult = new TNTCalculationResult
                            {
                                Distance = Pearl.Position.Distance2D(destination) ,
                                Tick = ticks ,
                                Blue = blueTNT + b ,
                                Red = redTNT + r ,
                                TotalTNT = blueTNT + b + redTNT + r
                            };
                            result.Add(tResult);
                        }
                    }
                }
            }
            return true;
        }

        private static Pearl PearlSimulation(int aTNT, int bTNT, int ticks, Space3D aTNTVector , Space3D bTNTVector)
        {
            Pearl pearl = Pearl;
            pearl.Vector += aTNT * aTNTVector + bTNT * bTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearl.Position += pearl.Vector;
                pearl.Vector *= 0.99;
                pearl.Vector.Y -= 0.03;
            }
            return pearl;
        }

        public static List<Pearl> CalculatePearl(int aTNT , int bTNT, int ticks)
        {
            Pearl pearl = Pearl;
            Space3D aTNTVector = VectorCalculation.CalculateMotion(Pearl.Position , ATNTPosition);
            Space3D bTNTVector = VectorCalculation.CalculateMotion(Pearl.Position , BTNTPosition);
            List<Pearl> pearlTrace = new List<Pearl>();
            pearl.Vector += aTNT * aTNTVector + bTNT * bTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearl.Position += pearl.Vector;
                pearl.Vector *= 0.99;
                pearl.Vector.Y -= 0.03;
                pearlTrace.Add(pearl);
            }
            return pearlTrace;
        }
    }
}
