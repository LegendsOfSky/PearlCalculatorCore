using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;

namespace PearlCalculatorLib.Manually
{
    public static class Calculation
    {
        public static bool CalculateTNTAmount(Space3D destination , int ticks , out List<TNTCalculationResult> result)
        {
            Space3D aTNTVector;
            Space3D bTNTVector;
            double divider = 0;
            int aTNT;
            int bTNT;
            result = new List<TNTCalculationResult>();
            Space3D distance = destination - Data.Pearl.Position;
            if(Math.Abs(distance.X) == 0 && Math.Abs(distance.Z) == 0)
                return false;
            aTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position , Data.ATNT);
            bTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position , Data.BTNT);
            for(int i = 1; i <= ticks; i++)
            {
                divider += Math.Pow(0.99 , i - 1);
                distance = (distance - Data.Pearl.Position) / divider;
                 aTNT = Convert.ToInt32(Math.Round((distance.Z * bTNTVector.X - distance.X * bTNTVector.Z) / (aTNTVector.Z * bTNTVector.X - bTNTVector.Z * aTNTVector.X)));
                bTNT = Convert.ToInt32(Math.Round((distance.X - aTNT * aTNTVector.X) / bTNTVector.X));
                for(int a = -5; a <= 5; a++)
                {
                    for(int b = -5; b <= 5; b++)
                    {
                        PearlEntity aPearl = PearlSimulation(aTNT + a , bTNT + b , i , aTNTVector , bTNTVector);
                        if(Math.Abs(aPearl.Position.X - destination.X) <= 10 && Math.Abs(aPearl.Position.Z - destination.Z) <= 10)
                        {
                            TNTCalculationResult tResult = new TNTCalculationResult
                            {
                                Distance = Data.Pearl.Position.Distance2D(destination) ,
                                Tick = i ,
                                Blue = bTNT + b ,
                                Red = aTNT + a ,
                                TotalTNT = bTNT + b + aTNT + a
                            };
                            result.Add(tResult);
                        }
                    }
                }
            }
            return true;
        }

        private static PearlEntity PearlSimulation(int aTNT, int bTNT, int ticks, Space3D aTNTVector , Space3D bTNTVector)
        {
            PearlEntity pearl = Data.Pearl;
            pearl.Motion += aTNT * aTNTVector + bTNT * bTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearl.Tick();
            }
            return pearl;
        }

        public static List<Entity> CalculatePearl(int aTNT , int bTNT, int ticks)
        {
            PearlEntity pearl = new PearlEntity(Data.Pearl);
            Space3D aTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position , Data.ATNT);
            Space3D bTNTVector = VectorCalculation.CalculateMotion(Data.Pearl.Position , Data.BTNT);
            List<Entity> pearlTrace = new List<Entity>();
            pearl.Motion += aTNT * aTNTVector + bTNT * bTNTVector;
            for(int i = 0; i < ticks; i++)
            {
                pearl.Tick();
                pearlTrace.Add(pearl);
            }
            return pearlTrace;
        }
    }
}
