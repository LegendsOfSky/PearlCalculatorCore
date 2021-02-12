﻿using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.world;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PearlCalculatorLib.AttachedLLFTL
{
    public static class Data
    {
        public static Stack<TNTCalculationResult> TNTResult = new Stack<TNTCalculationResult>();

        public static PearlEntity OriginalPearl = new PearlEntity().
            WithPosition(0 , 170.34722638929408 , 0).
            WithVector(0 , 0.2716278719434352 , 0);

        public static Space3D PearlOffset;

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
    }
}
