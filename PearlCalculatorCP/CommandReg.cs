using System.Collections.Generic;
using PearlCalculatorCP.Commands;

namespace PearlCalculatorCP
{
    public static class CommandReg
    {
        private static bool isRegistered = false;

        public static void Register()
        {
            CommandManager.Register("showData", new ShowGeneralData(), new List<string>
            {
                "show some General Data",
                "you can check these values"
            });

            CommandManager.Register("changeMaxTicks", new ChangeMaxTicks(), new List<string> 
            {
                "change MaxTicks"
            });
        }
    }
}