using System;
using System.Collections.Generic;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorLib.Settings
{
    [Serializable]
    public class CannonSettings : IDeepCloneable<CannonSettings>
    {
        public string CannonName { get; set; }

        public int MaxTNT { get; set; }

        public Direction DefaultRedDirection { get; set; }
        public Direction DefaultBlueDirection  { get; set; }
        
        public Space3D NorthWestTNT  { get; set; }
        public Space3D NorthEastTNT  { get; set; }
        public Space3D SouthWestTNT { get; set; }
        public Space3D SouthEastTNT { get; set; }
        
        public Surface2D Offset { get; set; }
        public PearlEntity Pearl { get; set; }

        public List<int> RedTNTConfiguration { get; set; }
        public List<int> BlueTNTConfiguration { get; set; }
        
        public CannonSettings DeepClone()
        {
            List<int> redTNTConfiguration = new List<int>(RedTNTConfiguration.Count);
            List<int> blueTNTConfiguration = new List<int>(BlueTNTConfiguration.Count);
            
            redTNTConfiguration.AddRange(RedTNTConfiguration);
            blueTNTConfiguration.AddRange(BlueTNTConfiguration);

            CannonSettings result = new CannonSettings
            {
                CannonName           = $"{CannonName}(clone)",
                MaxTNT               = MaxTNT,
                DefaultRedDirection  = DefaultRedDirection,
                DefaultBlueDirection = DefaultBlueDirection,
                NorthWestTNT         = NorthWestTNT,
                NorthEastTNT         = NorthEastTNT,
                SouthWestTNT         = SouthWestTNT,
                SouthEastTNT         = SouthEastTNT,
                Offset               = Offset,
                Pearl                = Pearl.DeepClone(),
                RedTNTConfiguration  = redTNTConfiguration,
                BlueTNTConfiguration = blueTNTConfiguration
            };

            return result;
        }

        object IDeepCloneable.DeepClone() => DeepClone();
    }
}
