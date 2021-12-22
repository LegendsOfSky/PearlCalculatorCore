using PearlCalculatorLib.General;
using System.Collections.Generic;

namespace PearlCalculatorLib.Settings
{
    public static class SettingsHelper
    {
        private static List<CannonSettings> Settings { get; set; } = new List<CannonSettings>();

        private static CannonSettings _selectedCannon = null;

        public static bool SetGeneralData(string cannonName)
        {
            CannonSettings settings   = Settings.Find(x => x.CannonName == cannonName);
            if(settings == null)        return false;
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

            _selectedCannon = settings;
            return true;
        }

        public static CannonSettings GetSettings(string cannonName) => Settings.Find(x => x.CannonName == cannonName);
        
        public static void AddSettings(CannonSettings settings)
        {
            if (Settings.Contains(settings) || GetSettings(settings.CannonName) != null)
                return;
            
            Settings.Add(settings);
        }

        public static bool RemoveSettings(string cannonName)
        {
            int index = Settings.FindIndex(x => x.CannonName == cannonName);
            if (index > -1)
            {
                if (Settings[index] == _selectedCannon)
                    _selectedCannon = null;
                
                Settings.RemoveAt(index);
                return true;
            }
            return false;
        }

        public static SettingsCollection GenerateSettingsCollection()
        {
            SettingsCollection result = new SettingsCollection
            {
                Version = SettingsCollection.CurrentVersion,
                RedTNT = Data.RedTNT,
                BlueTNT = Data.BlueTNT,
                TNTWeight = Data.TNTWeight,
                SelectedCannon = _selectedCannon?.CannonName ?? string.Empty,
                Direction = Data.Direction,
                Destination = Data.Destination.ToSurface2D(),
                CannonSettings = Settings.ToArray()
            };
            return result;
        }
    }
}
