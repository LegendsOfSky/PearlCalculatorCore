using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PCCExtension_SettingFile.Settings.Custom
{
    public class SingleTNTCustomSetting : CustomSetting
    {
        public int Amount { get; set; }
        public Space3D TNT { get; set; }



        public SingleTNTCustomSetting()
        {
            SubType = CustomType.Single;
        }
    }
}
