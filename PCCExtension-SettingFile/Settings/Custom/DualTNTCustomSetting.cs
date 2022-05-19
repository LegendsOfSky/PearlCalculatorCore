using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PCCExtension_SettingFile.Settings.Custom
{
    public class DualTNTCustomSetting : CustomSetting
    {
        public int AAmount { get; set; }
        public int BAmount { get; set; }
        public Space3D ATNT { get; set; }
        public Space3D BTNT { get; set; }



        public DualTNTCustomSetting()
        {
            SubType = CustomType.Dual;
        }
    }
}
