using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PCCExtension_SettingFile.Settings.Custom
{
    public abstract class CustomSetting : Setting
    {
        public CustomType SubType { get; set; }
        public Surface2D Destination { get; set; }
        public PearlEntity Pearl { get; set; }

        public enum CustomType
        {
            Single,
            Dual
        }

        public CustomSetting()
        {
            Type = SettingType.Custom;
        }
    }
}
