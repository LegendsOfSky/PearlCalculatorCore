using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.Manually
{
    public static class Data
    {
        public static Space3D ATNT;
        public static Space3D BTNT;
        public static Surface2D Destination = new Surface2D();
        public static PearlEntity Pearl = new PearlEntity();
        public static int ATNTAmount;
        public static int BTNTAmount;
    }
}
