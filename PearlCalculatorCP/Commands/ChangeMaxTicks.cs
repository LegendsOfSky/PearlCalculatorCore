using PearlCalculatorCP.Models;
using PearlCalculatorCP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorCP.Commands
{
    public class ChangeMaxTicks : ICommand
    {
        public void Excute(string[]? paramaters, Action<ConsoleOutputItemModel> messageSender)
        {
            if(paramaters != null && paramaters.Length != 0)
            {
                int.TryParse(paramaters[0], out int maxTicks);
                if(maxTicks > 0)
                {
                    MainWindowViewModel.MaxTicks = maxTicks;
                    messageSender(DefineCmdOutput.MsgTemplate($"Change Max Ticks to {maxTicks}"));
                }
                else
                    messageSender(DefineCmdOutput.ErrorTemplate("Value Incorrect"));
            }
            else
                messageSender(DefineCmdOutput.ErrorTemplate("Missing Value"));
        }
    }
}
