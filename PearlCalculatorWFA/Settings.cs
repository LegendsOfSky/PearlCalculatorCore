using System;
using System.Collections.Generic;
using System.Text;
using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.General;

namespace PearlCalculatorWFA
{
    [Serializable]
    public class Settings
    {
        public TNT NorthWest = new TNT();
        public TNT NorthEast = new TNT();
        public TNT SouthWest = new TNT();
        public TNT SouthEast = new TNT();

        public Pearl Pearl = new Pearl();

        public TNTArray SouthArray = new TNTArray();
        public TNTArray WestArray = new TNTArray();
        public TNTArray NorthArray = new TNTArray();
        public TNTArray EastArray = new TNTArray();

        public int RedTNT;
        public int BlueTNT;
        public int MaxTNT;

        public Space3D Destination = new Space3D();
        public Space3D Offset = new Space3D();

        public string Direction;
    }
}
