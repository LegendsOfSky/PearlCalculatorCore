using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PearlCalculatorLib.CalculationLib;

namespace PearlCalculatorLib.General
{
    public class Data
    {
        [Checker(typeof(UpdatePearlChecker))]
        public static Pearl Pearl = new Pearl().
            WithPosition(0 , 197.34722638929408 , 0).
            WithVector(0 , 0.2716278719434352 , 0);
        public static Space3D Destination = new Space3D();
        [Checker(typeof(UpdatePearlOffsetChecker))]
        public static Space3D PearlOffset = new Space3D(0.9375 , 0 , 0.0625);
        public static int TNTWeight;
        public static int RedTNT;
        public static int BlueTNT;
        public static int MaxTNT;
        public static int MaxCalculateTNT;
        public static double MaxCalculateDistance;
        public static string Direction = "North";

        [Checker(typeof(UpdateTNTDataChecker))]
        public static TNT NorthWest = new TNT().WithIndecedVector(0.5512392437442475 , -0.00102112023649516 , 0.6350142057709602);
        [Checker(typeof(UpdateTNTDataChecker))]
        public static TNT NorthEast = new TNT().WithIndecedVector(-0.5871676601182904 , -0.00094418168052536 , 0.5871676601182904);
        [Checker(typeof(UpdateTNTDataChecker))]
        public static TNT SouthWest = new TNT().WithIndecedVector(0.6025678785239389 , -0.00111620183360574 , -0.6025678785239389);
        [Checker(typeof(UpdateTNTDataChecker))]
        public static TNT SouthEast = new TNT().WithIndecedVector(-0.6350142057709602 , -0.00102112023649516 , -0.5512392437442475);

        [Checker(typeof(UpdateArrayChecker))]
        public static TNTArray SouthArray = new TNTArray("NE" , "NW");
        [Checker(typeof(UpdateArrayChecker))]
        public static TNTArray WestArray = new TNTArray("SE" , "NE");
        [Checker(typeof(UpdateArrayChecker))]
        public static TNTArray NorthArray = new TNTArray("SE" , "SW");
        [Checker(typeof(UpdateArrayChecker))]
        public static TNTArray EastArray = new TNTArray("SW" , "NW");
        public static List<TNTCalculationResult> TNTResult = new List<TNTCalculationResult>();

        public static void ResetParameter()
        {
            NorthWest.WithIndecedVector(0.5512392437442475 , -0.00102112023649516 , 0.6350142057709602);
            NorthEast.WithIndecedVector(-0.5871676601182904 , -0.00094418168052536 , 0.5871676601182904);
            SouthWest.WithIndecedVector(0.6025678785239389 , -0.00111620183360574 , -0.6025678785239389);
            SouthEast.WithIndecedVector(-0.6350142057709602 , -0.00102112023649516 , -0.5512392437442475);

            Pearl.WithPosition(0 , 197.34722638929408 , 0).WithPosition(0 , 0.2716278719434352 , 0);

            SouthArray.Red = "NE";
            SouthArray.Blue = "NW";

            WestArray.Red = "SE";
            WestArray.Blue = "NE";

            NorthArray.Red = "SE";
            NorthArray.Blue = "SW";

            EastArray.Red = "SW";
            EastArray.Blue = "NW";

            PearlOffset = new Space3D(0.9375 , 0 , 0.0625);
        }

        public static void UpdateData<V>(string dataName, V value)
        {
            var field = typeof(Data).GetField(dataName , System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.CreateInstance);
            var attr = field.GetCustomAttributes(typeof(CheckerAttribute), false).FirstOrDefault() as CheckerAttribute;

            if(attr == null)  //Non attribute
                field.SetValue(null , value);
            else
            {
                var checker = CheckerAttribute.GetChecker<V>(attr.CheckerType);
                if(checker != null)
                {
                    checker.Check(dataName , ref value , out var isModify);
                    if(isModify)
                        field.SetValue(null , value);
                }
                else
                    field.SetValue(null , value);
            }
        }
    }
}
