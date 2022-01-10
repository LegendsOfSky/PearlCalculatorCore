using System;
using System.Collections.Generic;
using System.Linq;
using PearlCalculatorLib.General;

namespace PearlCalculatorLib.Settings
{
    public class SettingsManager
    {
        public List<CannonSettings> SettingsList { get; private set; }

        private CannonSettings _selectedCannon;
        public CannonSettings SelectedCannon
        {
            get => _selectedCannon;
            set
            {
                if (value is null || !SettingsList.Contains(value))
                    throw new ArgumentException("value is null or not found in SettingsList");

                _selectedCannon = value;
            }
        }

        public SettingsManager()
        {
            CreateDefaultSettings();
        }

        public SettingsManager(CannonSettings[] settings, string selectedCannonName = null)
        {
            if (settings is null || settings.Length == 0)
            {
                CreateDefaultSettings();
            }
            else
            {
                SettingsList = settings.ToList();
                SelectedCannon = selectedCannonName is null 
                    ? SettingsList[0]
                    : GetSettings(selectedCannonName) ?? SettingsList[0];
            }
        }

        private void CreateDefaultSettings()
        {
            SettingsList = new List<CannonSettings>();
            CannonSettings setting = new CannonSettings
            {
                CannonName           = "Default",
                MaxTNT               = Data.MaxTNT,
                DefaultRedDirection  = Data.DefaultRedDuper,
                DefaultBlueDirection = Data.DefaultBlueDuper,
                NorthWestTNT         = Data.NorthWestTNT,
                NorthEastTNT         = Data.NorthEastTNT,
                SouthWestTNT         = Data.SouthWestTNT,
                SouthEastTNT         = Data.SouthEastTNT,
                Offset               = Data.PearlOffset,
                Pearl                = Data.Pearl,
                RedTNTConfiguration  = Data.RedTNTConfiguration,
                BlueTNTConfiguration = Data.BlueTNTConfiguration
            };
            SettingsList.Add(setting);
            _selectedCannon = setting;
        }
        
        public bool SelectCannon(string cannonName)
        {
            CannonSettings settings = SelectedCannon.CannonName == cannonName ? SelectedCannon : GetSettings(cannonName);

            if(settings == null)
                return false;
            
            SetGeneralData(settings);
            SelectedCannon = settings;
            return true;
        }

        public bool SelectCannon(int index)
        {
            if (index < 0 || index >= SettingsList.Count)
                return false;
            
            SetGeneralData(SettingsList[index]);
            SelectedCannon = SettingsList[index];
            return true;
        }
        
        private void SetGeneralData(CannonSettings settings)
        {
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

        public CannonSettings GetSettings(string cannonName) => 
            SettingsList.Find(e => e.CannonName == cannonName);
        
        public bool AddSettings(CannonSettings settings)
        {
            if (GetSettings(settings.CannonName) == null)
            {
                SettingsList.Add(settings);
                return true;
            }

            return false;
        }

        public bool RemoveSettings(string cannonName) => 
            RemoveSettings(SettingsList.FindIndex(x => x.CannonName == cannonName));

        public bool RemoveSettings(int index)
        {
            if (index == 0 && SettingsList.Count == 1)
                return false;

            if (index <= -1 || index >= SettingsList.Count)
                return false;

            if (SettingsList[index] == SelectedCannon)
                SelectedCannon = index == 0 ? SettingsList[1] : SettingsList[0];

            SettingsList.RemoveAt(index);
            return true;
        }

        public SettingsCollection CreateSettingsCollection()
        {
            SettingsCollection result = new SettingsCollection
            {
                Version        = SettingsCollection.CurrentVersion,
                RedTNT         = Data.RedTNT,
                BlueTNT        = Data.BlueTNT,
                TNTWeight      = Data.TNTWeight,
                SelectedCannon = SelectedCannon?.CannonName ?? string.Empty,
                Direction      = Data.Direction,
                Destination    = Data.Destination.ToSurface2D(),
                CannonSettings = SettingsList.ToArray()
            };
            return result;
        }
    }
}