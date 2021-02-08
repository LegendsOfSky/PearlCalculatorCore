using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.SizeDataBase
{
    public static class BlockSize
    {
        public readonly static AABBBox BottomTrapDoorBlock = new AABBBox(1 , 0.1875 , 1);
        public readonly static AABBBox EastOpenTrapDoorBlock = new AABBBox(0.1875 , 1 , 1);
        public readonly static AABBBox WestOpenTrapDoorBlock = new AABBBox(0.8125 , 0 , 0 , 1 , 1 , 1);
        public readonly static AABBBox SouthOpenTrapDoorBlock = new AABBBox(1 , 1 , 0.1875);
        public readonly static AABBBox NorthOpenTrapDoorBlock = new AABBBox(0 , 0 , 0.8125 , 1 , 1 , 1);
        public readonly static AABBBox DaylightSensorBlock = new AABBBox(1 , 0.375 , 1);
        public readonly static AABBBox CampfireBlock = new AABBBox(1 , 0.4375 , 1);
        public readonly static AABBBox CakeBlock = new AABBBox(0.0625 , 0 , 0.0625 , 0.9375 , 0.5 , 0.9375);
        public readonly static AABBBox SlabBlock = new AABBBox(1 , 0.5 , 1);
        public readonly static AABBBox StoneCutterBlock = new AABBBox(1 , 0.5625 , 1);
        public readonly static AABBBox PistonBaseBlock = new AABBBox(1 , 0.75 , 1);
        public readonly static AABBBox EnchantingTableBlock = new AABBBox(1 , 0.75 , 1);
        public readonly static AABBBox SkullBlock = new AABBBox(0.25 , 0 , 0.25 , 0.75 , 0.5 , 0.75);
        public readonly static AABBBox NorthWallSkullBlock = new AABBBox(0.25 , 0.25 , 0.5 , 0.75 , 0.75 , 1);
        public readonly static AABBBox SouthWallSkullBlock = new AABBBox(0.25 , 0.25 , 0 , 0.75 , 0.75 , 0.5);
        public readonly static AABBBox EastWallSkullBlock = new AABBBox(0 , 0.25 , 0.25 , 0.5 , 0.75 , 0.75);
        public readonly static AABBBox WestWallSkullBlock = new AABBBox(0.5 , 0.25 , 0.24 , 1 , 0.75 , 0.75);
        public readonly static AABBBox BrewingStandBlock = new AABBBox(0.4375 , 0 , 0.4375 , 0.5625 , 0.875 , 0.5625);
        public readonly static AABBBox SoulSandBlock = new AABBBox(1 , 0.875 , 1);
        public readonly static AABBBox ChestBlock = new AABBBox(0.1 , 0 , 0.1 , 0.9 , 0.9 , 0.9);
    }
}
