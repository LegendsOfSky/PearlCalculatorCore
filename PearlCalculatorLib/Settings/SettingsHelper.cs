using PearlCalculatorLib.General;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PearlCalculatorLib.Settings
{
    public static class SettingsHelper
    {
        public static List<CannonSettings> Settings { get; set; } = new List<CannonSettings>();

        public static void SetGeneralData(string cannonName)
        {
            CannonSettings settings   = Settings.Find(x => x.CannonName == cannonName);
            if(settings == null)        return;
            Data.MaxTNT               = settings.MaxTNT;
            Data.DefaultBlueDuper     = settings.DefaultBlueDirection;
            Data.DefaultRedDuper      = settings.DefaultRedDirection;
            Data.NorthEastTNT         = settings.NorthEastTNT;
            Data.NorthWestTNT         = settings.NorthWestTNT;
            Data.SouthEastTNT         = settings.SouthEastTNT;
            Data.SouthWestTNT         = settings.SouthWestTNT;
            Data.Pearl                = settings.Pearl;
            Data.PearlOffset          = settings.Offset;
            Data.RedTNTConfiguration  = settings.RedTNTConfiguration;
            Data.BlueTNTConfiguration = settings.BlueTNTConfiguration;
        }
    }
}
