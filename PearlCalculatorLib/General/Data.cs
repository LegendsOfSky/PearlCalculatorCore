﻿using System.Collections.Generic;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    public static class Data
    {
        /// <summary>
        /// The Local Coordinate of the North West TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D NorthWestTNT = new Space3D(-0.884999990463257 , 170.5 , -0.884999990463257);

        /// <summary>
        /// The Local Coordinate of the North East TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D NorthEastTNT = new Space3D(+0.884999990463257 , 170.5 , -0.884999990463257);

        /// <summary>
        /// The Local Coordinate of the South West TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D SouthWestTNT = new Space3D(-0.884999990463257 , 170.5 , +0.884999990463257);

        /// <summary>
        /// The Local Coordinate of the South East TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D SouthEastTNT = new Space3D(+0.884999990463257 , 170.5 , +0.884999990463257);



        /// <summary>
        /// The Gobal Coordinate of the Destination
        /// <para>Note : Y coordinate can be ignored. It does not take part in any calculation</para>
        /// <para>Required for <see cref="Calculation.CalculateTNTAmount(int, double)"/></para>
        /// </summary>
        public static Space3D Destination = new Space3D();



        /// <summary>
        /// The Gobal Coordinate(Only Y Axis is related to pearl) and motion of the Ender Pearl
        /// <para>Note : X and Z should be the gobal coordinate of the lava pool center</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static PearlEntity Pearl = (PearlEntity)new PearlEntity().WithPosition(0 , 170.34722638929412 , 0).WithMotion(0 , 0.2716278719434352 , 0);



        /// <summary>
        /// The Offset between Ender Pearl X and Z coordinate and lava pool center coordinate
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Surface2D PearlOffset
        {
            get => _PearlOffset;
            set
            {
                if(value.IsInside(SouthEastTNT.ToSurface2D() , NorthWestTNT.ToSurface2D()))
                    _PearlOffset = value;
            }
        }
        private static Surface2D _PearlOffset = new Surface2D(0 , 0);



        /// <summary>
        /// The number of Blue TNT for accelerating the Ender Pearl
        /// <para>Note : Not bound to any calculation. You can store Blue acceleration TNT here or ignore this </para>
        /// </summary>
        public static int BlueTNT;

        /// <summary>
        /// The number of Red TNT for accelerating the Ender Pearl
        /// <para>Note : Not bound to any calculation. You can store Red acceleration TNT here or ignore this </para>
        /// </summary>
        public static int RedTNT;



        /// <summary>
        /// The weight value for sorting the result
        /// <para>Note : Value Must be a Natural Number which is not larger than 100</para>
        /// <para>Required for <see cref="TNTCalculationResultSortComparerByWeighted"/></para>
        /// </summary>
        public static int TNTWeight;



        /// <summary>
        /// The Max number of TNT from each side
        /// <para>Required for <see cref="Calculation.CalculateTNTAmount(int, double)"/></para>
        /// </summary>
        public static int MaxTNT;



        /// <summary>
        /// Ignore This Please. For Internal Calculation Only
        /// </summary>
        public static int MaxCalculateTNT;

        /// <summary>
        /// Ignore This Please. For Internal Calculation Only
        /// </summary>
        public static double MaxCalculateDistance;



        /// <summary>
        /// The acceleration direction of the Ender Pearl
        /// <para>Note : Not bound to any calculation. You can store your acceleration direction here or ignore this </para>
        /// <para>Note : Only Allow For North, South, East, West</para>
        /// </summary>
        public static Direction Direction = Direction.North;



        /// <summary>
        /// The default position in the lava pool. Should be opposite to Blue
        /// <para>Note : Only Allow For NorthWest, NorthEast, SouthWest, SouthEast</para>
        /// <para>Required for all calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Direction DefaultRedDuper
        {
            get => _DefaultRedDuper;
            set
            {
                if((value.IsNorth() || value.IsSouth()) && (value.IsEast() || value.IsWest()))
                    _DefaultRedDuper = value;
            }
        }

        private static Direction _DefaultRedDuper = Direction.SouthEast;



        /// <summary>
        /// The default position in the lava pool. Should be opposite to Red
        /// <para>Note : Only Allow For NorthWest, NorthEast, SouthWest, SouthEast</para>
        /// <para>Required for all calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Direction DefaultBlueDuper
        {
            get => _DefaultBlueDuper;
            set
            {
                if((value.IsNorth() || value.IsSouth()) && (value.IsEast() || value.IsWest()))
                    _DefaultBlueDuper = value;
            }    
        }

        private static Direction _DefaultBlueDuper = Direction.NorthWest;



        /// <summary>
        /// The Result of the <see cref="Calculation.CalculateTNTAmount(int, double)"/>
        /// <para>Note : It is a Data Output</para>
        /// </summary>
        public static List<TNTCalculationResult> TNTResult = new List<TNTCalculationResult>();


        /// <summary>
        /// A list of TNT configuration. 
        /// </summary>
        public static List<int> RedTNTConfiguration = new List<int>();

        /// <summary>
        /// A list if TNT configuration
        /// </summary>
        public static List<int> BlueTNTConfiguration = new List<int>();



        /// <summary>
        /// Reset the whole <see cref="Data"/> into default value
        /// </summary>
        public static void Reset()
        {
            NorthWestTNT = new Space3D(-0.884999990463257 , 170.5 , -0.884999990463257);
            NorthEastTNT = new Space3D(+0.884999990463257 , 170.5 , -0.884999990463257);
            SouthWestTNT = new Space3D(-0.884999990463257 , 170.5 , +0.884999990463257);
            SouthEastTNT = new Space3D(+0.884999990463257 , 170.5 , +0.884999990463257);
            Pearl.Position = new Space3D(0 , 170.34722638929412 , 0);
            Pearl.Motion = new Space3D(0 , 0.2716278719434352 , 0);
            DefaultRedDuper = Direction.SouthEast;
            DefaultBlueDuper = Direction.NorthWest;
            PearlOffset = new Surface2D();
        }
    }
}
