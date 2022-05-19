using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PCCExtension_SettingFile.Settings.General
{
    public class MultiGeneralSetting : GeneralSetting
    {
        public int RedTNT { get; set; }
        public int BlueTNT { get; set; }
        public int TNTSortWeight { get; set; }
        public string SelectedCannon { get; set; }
        public Direction Direction { get; set; }
        public Surface2D Destination { get; set; }
        public List<CannonSetting> Cannons { get; set; }



        public class CannonSetting
        {
            public int MaxTNT { get; set; }
            public string Name { get; set; }
            public Surface2D Offset { get; set; }
            public Space3D NorthWestTNT { get; set; }
            public Space3D NorthEastTNT { get; set; }
            public Space3D SouthWestTNT { get; set; }
            public Space3D SouthEastTNT { get; set; }
            public PearlEntity Pearl { get; set; }
        }



        public MultiGeneralSetting()
        {
            SubType = GeneralType.Multiple;
        }
    }
}
