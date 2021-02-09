using System.Collections.Generic;
using System.Text;
using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    public static class Data
    {
        public static Space3D NorthWestTNT = new Space3D(-0.884999990463257 , 170.5 , -0.884999990463257);
        public static Space3D NorthEastTNT = new Space3D(+0.884999990463257 , 170.5 , -0.884999990463257);
        public static Space3D SouthWestTNT = new Space3D(-0.884999990463257 , 170.5 , +0.884999990463257);
        public static Space3D SouthEastTNT = new Space3D(+0.884999990463257 , 170.5 , +0.884999990463257);

        public static Space3D Destination = new Space3D();

        public static PearlEntity Pearl = new PearlEntity().WithPosition(0 , 170.34722638929408 , 0).WithVector(0 , 0.2716278719434352 , 0);
        public static Space3D PearlOffset = new Space3D(0 , 0 , 0);

        public static int BlueTNT;
        public static int RedTNT;

        public static int TNTWeight;

        public static int MaxTNT;

        public static int MaxCalculateTNT;
        public static double MaxCalculateDistance;

        public static Direction Direction = Direction.North;

        public static Direction DefaultRedDuper = Direction.SouthEast;
        public static Direction DefaultBlueDuper = Direction.NorthWest;

        public static List<TNTCalculationResult> TNTResult = new List<TNTCalculationResult>();
    }
}
