using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlCalculatorIntermediateLib.Settings
{
    [Serializable]
    public class SettingsColletion
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
