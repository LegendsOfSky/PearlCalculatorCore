using System;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorLib.Settings
{
    [Serializable]
    public class SettingsCollection
    {
        public const string CurrentVersion = "2.71";


        public string Version { get; set; }

        public int RedTNT { get; set; }

        public int BlueTNT { get; set; }

        public int TNTWeight { get; set; }

        public string SelectedCannon { get; set; }

        public Direction Direction { get; set; }

        public Surface2D Destination { get; set; }

        public CannonSettings[] CannonSettings { get; set; }
    }
}
