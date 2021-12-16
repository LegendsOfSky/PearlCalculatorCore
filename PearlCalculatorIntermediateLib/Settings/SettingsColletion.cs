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
        public double Version { get; set; } = 2.71;

        public int RedTNT { get; set; }

        public int BlueTNT { get; set; }

        public string CannonName { get; set; }

        public Direction Direction { get; set; }

        public Surface2D? Destination { get; set; }

        public Surface2D? CannonLocation { get; set; }

        public CannonSettings[] CannonSettings { get; set; }
    }
}
