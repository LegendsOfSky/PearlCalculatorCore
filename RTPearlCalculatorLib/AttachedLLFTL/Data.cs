using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RTPearlCalculatorLib.AttachedLLFTL
{
    public static class Data
    {
        public static Stack<TNTAndRequireButtonBlockResult> LLFTLResult = new Stack<TNTAndRequireButtonBlockResult>();

        public static PearlEntity OriginalPearl = new PearlEntity().
            WithPosition(0 , 170.34722638929408 , 0).
            WithMotion(0 , 0.2716278719434352 , 0);

        public static Surface2D PearlOffset = new Surface2D();

        public static Space3D OriginalNorthWestTNT = new Space3D(-0.884999990463257 , 170.5 , -0.884999990463257);
        public static Space3D OriginalNorthEastTNT = new Space3D(+0.884999990463257 , 170.5 , -0.884999990463257);
        public static Space3D OriginalSouthWestTNT = new Space3D(-0.884999990463257 , 170.5 , +0.884999990463257);
        public static Space3D OriginalSouthEastTNT = new Space3D(+0.884999990463257 , 170.5 , +0.884999990463257);

        public static Space3D Destination = new Space3D();

        public static int MaxTNT;
        public static int ViewDistance;

        public static Direction Direction = Direction.North;

        public static Direction DefaultRedDuper = Direction.SouthEast;
        public static Direction DefaultBlueDuper = Direction.NorthWest;
        
        /// <summary>
        /// Load data form General <see cref="General.Data"/>
        /// </summary>
        public static void LoadDataFromGeneral()
        {
            DefaultBlueDuper = PearlCalculatorLib.General.Data.DefaultBlueDuper;
            DefaultRedDuper = PearlCalculatorLib.General.Data.DefaultRedDuper;
            Destination = PearlCalculatorLib.General.Data.Destination;
            Direction = PearlCalculatorLib.General.Data.Direction;
            MaxTNT = PearlCalculatorLib.General.Data.MaxTNT;
            OriginalNorthEastTNT = PearlCalculatorLib.General.Data.NorthEastTNT;
            OriginalNorthWestTNT = PearlCalculatorLib.General.Data.NorthWestTNT;
            OriginalSouthEastTNT = PearlCalculatorLib.General.Data.SouthEastTNT;
            OriginalSouthWestTNT = PearlCalculatorLib.General.Data.SouthWestTNT;
            OriginalPearl = PearlCalculatorLib.General.Data.Pearl;
            PearlOffset = PearlCalculatorLib.General.Data.PearlOffset;
        }
    }
}
