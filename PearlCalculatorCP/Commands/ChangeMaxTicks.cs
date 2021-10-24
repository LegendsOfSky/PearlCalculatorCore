using PearlCalculatorCP.Models;
using PearlCalculatorCP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorCP.Commands
{
    public class ChangeMaxTicks : ICommand
    {
        public void Execute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            if(parameters != null && parameters.Length != 0)
            {
                if (int.TryParse(parameters[0], out int maxTicks))
                {
                    if (maxTicks < 10)
                        maxTicks = 10;
                    else if (maxTicks > 10270)
                        maxTicks = 10270;

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
