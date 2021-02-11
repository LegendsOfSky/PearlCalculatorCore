using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.world;
using System;
using System.Collections.Generic;
using System.Text;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    [Serializable]
    public class Settings
    {
        public const string FileSuffix = "pcld file|*.pcld";

        public Space3D NorthWestTNT;
        public Space3D NorthEastTNT;
        public Space3D SouthWestTNT;
        public Space3D SouthEastTNT;
        public Space3D Destination;
        public Space3D Offset;
        public PearlEntity Pearl;
        public int RedTNT;
        public int BlueTNT;
        public int MaxTNT;
        public Direction Direction;
    }
}
