using System.Collections.Generic;
using PearlCalculatorCP.Commands;

namespace PearlCalculatorCP
{
    public static class CommandReg
    {
        private static bool isRegistered = false;

        public static void Register()
        {
            if (isRegistered) return;
        
            CommandManager.Register("showData", new ShowGeneralData(), new List<string>
            {
                "show some General Data",
                "you can check these values"
            });

            CommandManager.Register("changeMaxTicks", new ChangeMaxTicks(), new List<string> 
            {
                "change MaxTicks"
            });

            CommandManager.Register("changeMaxDistance", new ChangeMaxDistance(), new List<string>
            {
                "change MaxDistance"
            });

            CommandManager.Register("clear", new Clear(), null);
            
            CommandManager.Register("changeLang", new ChangeLanguage(), null);
            CommandManager.Register("setDefaultLang", new SetDefaultLanguage(), null);
            CommandManager.Register("setDefaultSettings", new SetDefaultSettings(), null);
            
            isRegistered = true;
        }
    }
}