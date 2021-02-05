using PearlCalculatorLib.CalculationLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.SizeDataBase
{
    public static class BlockSize
    {
        public readonly static Size BottomTrapDoorBlock = new Size(1 , 0.1875 , 1);
        public readonly static Size EastOpenTrapDoorBlock = new Size(0.1875 , 1 , 1);
        public readonly static Size WestOpenTrapDoorBlock = new Size(0.8125 , 0 , 0 , 1 , 1 , 1);
        public readonly static Size SouthOpenTrapDoorBlock = new Size(1 , 1 , 0.1875);
        public readonly static Size NorthOpenTrapDoorBlock = new Size(0 , 0 , 0.8125 , 1 , 1 , 1);
        public readonly static Size DaylightSensorBlock = new Size(1 , 0.375 , 1);
        public readonly static Size CampfireBlock = new Size(1 , 0.4375 , 1);
        public readonly static Size CakeBlock = new Size(0.0625 , 0 , 0.0625 , 0.9375 , 0.5 , 0.9375);
        public readonly static Size SlabBlock = new Size(1 , 0.5 , 1);
        public readonly static Size StoneCutterBlock = new Size(1 , 0.5625 , 1);
        public readonly static Size PistonBaseBlock = new Size(1 , 0.75 , 1);
        public readonly static Size EnchantingTableBlock = new Size(1 , 0.75 , 1);
        public readonly static Size SkullBlock = new Size(0.25 , 0 , 0.25 , 0.75 , 0.5 , 0.75);
        public readonly static Size NorthWallSkullBlock = new Size(0.25 , 0.25 , 0.5 , 0.75 , 0.75 , 1);
        public readonly static Size SouthWallSkullBlock = new Size(0.25 , 0.25 , 0 , 0.75 , 0.75 , 0.8);
        public readonly static Size EastWallSkullBlock = new Size(0 , 0.25 , 0.25 , 0.5 , 0.75 , 0.75);
        public readonly static Size WestWallSkullBlock = new Size(0.5 , 0.25 , 0.24 , 1 , 0.75 , 0.75);
        public readonly static Size BrewingStandBlock = new Size(0.4375 , 0 , 0.4375 , 0.5625 , 0.875 , 0.5625);
        public readonly static Size SoulSandBlock = new Size(1 , 0.875 , 1);
        public readonly static Size ChestBlock = new Size(0.1 , 0 , 0.1 , 0.9 , 0.9 , 0.9);
        public readonly static Size EnderChestBlock = new Size(0.1 , 0.1 , 0.1 , 0.9 , 0.9 , 0.9);
    }
}
