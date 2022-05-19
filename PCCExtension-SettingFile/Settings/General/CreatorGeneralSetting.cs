using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PCCExtension_SettingFile.Settings.General
{
    internal class CreatorGeneralSetting : GeneralSetting
    {
        public List<CannonSetting> Cannons = new List<CannonSetting>();



        public class CannonSetting
        {
            public int MaxTNt { get; set; }
            public string CannonName { get; set; }
            public Surface2D Offset { get; set; }
            public Space3D NorthWestTNT { get; set; }
            public Space3D NorthEastTNT { get; set; }
            public Space3D SouthWestTNT { get; set; }
            public Space3D SouthEastTNT { get; set; }
            public PearlEntity Pearl { get; set; }
        }



        public CreatorGeneralSetting()
        {
            SubType = GeneralType.Creator;
        }
    }
}
