using System;
using System.Collections.Generic;
using System.Text;
using PearlCalculatorLib.CalculationLib;

namespace PearlCalculatorLib.General
{
    public class Data
    {
        public static Pearl Pearl = new Pearl().
            WithPosition(0 , 197.34722638929408 , 0).
            WithVector(0 , 0.2716278719434352 , 0);
        public static Space3D Destination = new Space3D();
        public static Space3D PearlOffset = new Space3D(0.9375 , 0 , 0.0625);
        public static int TNTWeight;
        public static int RedTNT;
        public static int BlueTNT;
        public static int MaxTNT;
        public static int MaxCalculateTNT;
        public static double MaxCalculateDistance;
        public static string Direction = "North";
        public static TNT NorthWest = new TNT().WithIndecedVector(0.5512392437442475 , -0.00102112023649516 , 0.6350142057709602);
        public static TNT NorthEast = new TNT().WithIndecedVector(-0.5871676601182904 , -0.00094418168052536 , 0.5871676601182904);
        public static TNT SouthWest = new TNT().WithIndecedVector(0.6025678785239389 , -0.00111620183360574 , -0.6025678785239389);
        public static TNT SouthEast = new TNT().WithIndecedVector(-0.6350142057709602 , -0.00102112023649516 , -0.5512392437442475);
        public static TNTArray SouthArray = new TNTArray("NE" , "NW");
        public static TNTArray WestArray = new TNTArray("SE" , "NE");
        public static TNTArray NorthArray = new TNTArray("SE" , "SW");
        public static TNTArray EastArray = new TNTArray("SW" , "NW");
        public static List<TNTCalculationResult> TNTResult = new List<TNTCalculationResult>();
    }
}
