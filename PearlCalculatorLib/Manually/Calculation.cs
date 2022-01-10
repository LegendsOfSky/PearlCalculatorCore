using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Net;
using System.Xml.Schema;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Threading.Tasks;
using System.Numerics;
using System.Linq;

namespace PearlCalculatorLib.Manually
{
    public static class Calculation
    {

        public static bool CalculateTNTAmount(ManuallyData data , int ticks , double maxDistance , out List<TNTCalculationResult> result)
        {
            Space3D vectorA = VectorCalculation.CalculateMotion(data.Pearl.Position , data.ATNT);
            Space3D vectorB = VectorCalculation.CalculateMotion(data.Pearl.Position , data.BTNT);
            double angleDestination = Space3D.Zero.AngleInAbsPolarRad(data.Destination.ToSpace3D());
            double angleA = Space3D.Zero.AngleInAbsPolarRad(vectorA);
            double angleB = Space3D.Zero.AngleInAbsPolarRad(vectorB);
            bool isATNTUseable = Math.Abs(angleA - angleDestination) < Math.PI / 2;
            bool isBTNTUseable = Math.Abs(angleB - angleDestination) < Math.PI / 2;

            result = null;

            if(!(isATNTUseable || isBTNTUseable))
                return false;

            if(vectorA.X != 0 && vectorB.X != 0)
            {
                Surface2D distance = data.Destination - data.Pearl.Position.ToSurface2D();
                Surface2D iHat = distance.Normalized;
                if(distance.IsClockWise(vectorA.ToSurface2D()) ^ distance.IsCounterClockWise(vectorB.ToSurface2D()))
                    return CalculateSingleTNTAmount(data , vectorA , vectorB , ticks , out result);
                else
                    return CalculateDualTNTAmount(data , vectorA , vectorB , ticks , maxDistance , out result);
            }
            else if(Math.Abs(angleA - angleB) < Math.PI / 2)
                return CalculateDualTNTAmount(data , vectorA , vectorB , ticks , maxDistance , out result);
            else
                return false;
        }

        private static bool CalculateSingleTNTAmount(ManuallyData data , Space3D vectorA , Space3D vectorB , int ticks , out List<TNTCalculationResult> result)
        {
            Surface2D spot1 = new Surface2D();
            Surface2D spot2 = new Surface2D();
            Surface2D spot3 = new Surface2D();
            Surface2D spot4 = new Surface2D();

            double m1 = vectorA.Z / vectorA.X;
            double m2 = vectorA.X / -vectorA.Z;
            double m3 = vectorB.Z / vectorB.X;
            double m4 = vectorB.X / -vectorB.Z;
            double k1 = data.Pearl.Position.Z - m1 * data.Pearl.Position.X;
            double k2 = data.Destination.Z - m2 * data.Destination.X;
            double k3 = data.Pearl.Position.Z - m3 * data.Pearl.Position.X;
            double k4 = data.Destination.Z - m4 * data.Destination.X;

            spot1.X = (k1 - k2) / (m2 - m1);
            spot1.Z = m1 * spot1.X + k1;
            spot2.X = (k3 - k4) / (m4 - m3);
            spot2.Z = m3 * spot2.X + k3;

            bool isSpot1Closer = spot1.Distance(data.Destination) < spot2.Distance(data.Destination);
            Space3D vector = vectorA;

            if(!isSpot1Closer)
            {
                m1 = m3;
                k1 = k3;
                vector = vectorB;
            }
            spot3.X = data.Destination.X;
            spot3.Z = data.Destination.X * m1 - k1;
            spot4.X = (data.Destination.Z - k1) / m1;
            spot4.Z = data.Destination.Z;

            double trueAmount1 = spot1.X / vector.X;
            double trueAmount2 = spot2.X / vector.X;
            double trueAmount3 = spot3.X / vector.X;
            double trueAmount4 = spot4.X / vector.X;

            result = null;

                double divider = 0;

                result = new List<TNTCalculationResult>(4 * ticks);

            if (isSpot1Closer)
            {
                for (int tick = 1; tick <= ticks; tick++)
                {
                    divider += Math.Pow(0.99, tick - 1);
                    int tnt1 = Convert.ToInt32(trueAmount1 / divider);
                    int tnt2 = Convert.ToInt32(trueAmount2 / divider);
                    int tnt3 = Convert.ToInt32(trueAmount3 / divider);
                    int tnt4 = Convert.ToInt32(trueAmount4 / divider);
                    result.Add(SingleTNTPearlSimulation(tnt1, 0, tick, vector, data.Destination, data.Pearl));
                    result.Add(SingleTNTPearlSimulation(tnt2, 0, tick, vector, data.Destination, data.Pearl));
                    result.Add(SingleTNTPearlSimulation(tnt3, 0, tick, vector, data.Destination, data.Pearl));
                    result.Add(SingleTNTPearlSimulation(tnt4, 0, tick, vector, data.Destination, data.Pearl));
                }
            }
            else
            {
                for (int tick = 1; tick <= ticks; tick++)
                {
                    divider += Math.Pow(0.99, tick - 1);
                    int tnt1 = Convert.ToInt32(trueAmount1 / divider);
                    int tnt2 = Convert.ToInt32(trueAmount2 / divider);
                    int tnt3 = Convert.ToInt32(trueAmount3 / divider);
                    int tnt4 = Convert.ToInt32(trueAmount4 / divider);
                    result.Add(SingleTNTPearlSimulation(0, tnt1, tick, vector, data.Destination, data.Pearl));
                    result.Add(SingleTNTPearlSimulation(0, tnt2, tick, vector, data.Destination, data.Pearl));
                    result.Add(SingleTNTPearlSimulation(0, tnt3, tick, vector, data.Destination, data.Pearl));
                    result.Add(SingleTNTPearlSimulation(0, tnt4, tick, vector, data.Destination, data.Pearl));
                }
            }

            result = result.Where(r => r.Blue + r.Red != 0).ToList();
            return true;
        }

        private static bool CalculateDualTNTAmount(ManuallyData data , Space3D vectorA , Space3D vectorB , int ticks , double maxDistance , out List<TNTCalculationResult> result)
        {
            int aTNT , bTNT;
            double denominator, trueA, trueB;
            Space3D distance;

            double divider = 0;

            distance = data.Destination.ToSpace3D() - data.Pearl.Position;
            result = new List<TNTCalculationResult>();
            
            if(distance.Absolute().ToSurface2D() == 0)
                return false;
            
            vectorA = VectorCalculation.CalculateMotion(data.Pearl.Position , data.ATNT);
            vectorB = VectorCalculation.CalculateMotion(data.Pearl.Position , data.BTNT);
            denominator = vectorA.Z * vectorB.X - vectorB.Z * vectorA.X;

            trueA = (distance.Z * vectorB.X - distance.X * vectorB.Z) / denominator;
            if(vectorB.X == 0)
                trueB = (distance.Z - trueA * vectorA.Z) / vectorB.Z;
            else
                trueB = (distance.X - trueA * vectorA.X) / vectorB.X;

            if(denominator == 0)
                return false;
            
            for(int i = 1; i <= ticks; i++)
            {
                divider += Math.Pow(0.99 , i - 1);
                aTNT = Convert.ToInt32(trueA / divider);
                bTNT = Convert.ToInt32(trueB / divider);
                
                for(int a = -5; a <= 5; a++)
                {
                
                    for(int b = -5; b <= 5; b++)
                    {
                        PearlEntity aPearl = PearlSimulation(aTNT + a , bTNT + b , i , vectorA , vectorB , new PearlEntity(data.Pearl));
                        Surface2D displacement = aPearl.Position.ToSurface2D() - data.Destination;

                        if(displacement.AxialDistanceLessOrEqualTo(maxDistance) && bTNT + b > 0 && aTNT + a > 0)
                        {
                    
                            TNTCalculationResult tResult = new TNTCalculationResult
                            {
                                Distance = displacement.Length,
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

            if(result.Count == 0)
                return CalculateSingleTNTAmount(data , vectorA , vectorB , ticks , out result);

            return true;
        }

        private static PearlEntity PearlSimulation(int aTNT , int bTNT , int ticks , Space3D aTNTVector , Space3D bTNTVector , PearlEntity pearl)
        {
            pearl.Motion += aTNT * aTNTVector + bTNT * bTNTVector;

            for(int i = 0; i < ticks; i++)
                pearl.Tick();
            
            return pearl;
        }

        private static TNTCalculationResult SingleTNTPearlSimulation(int atnt , int btnt , int ticks , Space3D vector , Surface2D destination , PearlEntity pearl)
        {
            pearl.Motion += (atnt + btnt) * vector;

            for(int i = 0; i < ticks; i++)
                pearl.Tick();

            return new TNTCalculationResult
            {
                Red = atnt ,
                Blue = btnt ,
                Distance = destination.Distance(pearl.Position.ToSurface2D()) ,
                Tick = ticks ,
                TotalTNT = atnt + btnt
            };
        }

        public static List<Entity> CalculatePearlTrace(ManuallyData data , int ticks)
        {
            Space3D aTNTVector = VectorCalculation.CalculateMotion(data.Pearl.Position , data.ATNT);
            Space3D bTNTVector = VectorCalculation.CalculateMotion(data.Pearl.Position , data.BTNT);
            PearlEntity pearl = new PearlEntity(data.Pearl);
            List<Entity> pearlTrace = new List<Entity>();
            
            pearl.Motion += data.ATNTAmount * aTNTVector + data.BTNTAmount * bTNTVector;
            pearlTrace.Add(new PearlEntity(pearl));
            
            for(int i = 0; i < ticks; i++)
            {
                pearl.Tick();
                pearlTrace.Add(new PearlEntity(pearl)); ;
            }
            
            return pearlTrace;
        }
    }
}
