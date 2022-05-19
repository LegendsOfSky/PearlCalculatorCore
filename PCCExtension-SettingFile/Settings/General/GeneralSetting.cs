using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCCExtension_SettingFile.Settings.General
{
    public abstract class GeneralSetting : Setting
    {
        public GeneralType SubType { get; protected set; }



        public enum GeneralType
        {
            Creator,
            Multiple,
        }
    }
}
