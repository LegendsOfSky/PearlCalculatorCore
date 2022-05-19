using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCCExtension_SettingFile.Settings
{
    public abstract class Setting
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public SettingType Type { get; protected set; }

        public enum SettingType
        {
            General,
            Custom,
        }
    }
}
