using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
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
        public Surface2D Offset;
        public PearlEntity Pearl;
        public int RedTNT;
        public int BlueTNT;
        public int MaxTNT;
        public Direction Direction;

        public static Settings CreateSettingsFormData() => new Settings()
        {
            NorthWestTNT = Data.NorthWestTNT ,
            NorthEastTNT = Data.NorthEastTNT ,
            SouthWestTNT = Data.SouthWestTNT ,
            SouthEastTNT = Data.SouthEastTNT ,

            Pearl = Data.Pearl ,

            RedTNT = Data.RedTNT ,
            BlueTNT = Data.BlueTNT ,
            MaxTNT = Data.MaxTNT ,

            Destination = Data.Destination ,
            Offset = Data.PearlOffset ,

            Direction = Data.Direction
        };
    }
}
