using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PearlCalculatorIntermediateLib.Settings
{
    [Serializable]
    public class CannonSettings
    {
        public string CannonName { get; set; }

        public int MaxTNT { get; set; }

        public Direction DefaultRedDirection { get; set; }
        public Direction DefaultBlueDirection  { get; set; }
        
        public Space3D NorthWestTNT  { get; set; }
        public Space3D NorthEastTNT  { get; set; }
        public Space3D SouthWestTNT { get; set; }
        public Space3D SouthEastTNT { get; set; }
        
        public PearlEntity Pearl { get; set; }
        public Surface2D Offset { get; set; }

        public List<int> RedTNTConfiguration { get; set; }
        public List<int> BlueTNTConfiguration { get; set; }
    }
}
