using PearlCalculatorLib.CalculationLib;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PearlCalculatorLib.AttachedLLFTL
{
    public static class Calculation
    {
        public static void LoadDataFromGeneral()
        {
            Data.DefaultBlueDuper = General.Data.DefaultBlueDuper;
            Data.DefaultRedDuper = General.Data.DefaultRedDuper;
            Data.Destination = General.Data.Destination;
            Data.Direction = General.Data.Direction;
            Data.MaxTNT = General.Data.MaxTNT;
            Data.OriginalNorthEastTNT = General.Data.NorthEastTNT;
            Data.OriginalNorthWestTNT = General.Data.NorthWestTNT;
            Data.OriginalSouthEastTNT = General.Data.SouthEastTNT;
            Data.OriginalSouthWestTNT = General.Data.SouthWestTNT;
            Data.OriginalPearl = General.Data.Pearl;
            Data.OriginalPearl.Position += General.Data.PearlOffset;
        }

        public static void CalculateSuitableAttachLocation()
        {

        }


    }
}
