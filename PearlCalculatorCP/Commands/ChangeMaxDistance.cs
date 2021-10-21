using PearlCalculatorCP.Models;
using PearlCalculatorCP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorCP.Commands
{
    public class ChangeMaxDistance : ICommand
    {
        public void Execute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            if(parameters != null && parameters.Length != 0)
            {
                double.TryParse(parameters[0], out double maxDistance);
                if(maxDistance > 0)
                {
                    MainWindowViewModel.MaxDistance = maxDistance;
                    messageSender(DefineCmdOutput.MsgTemplate($"Change Max Distance to {maxDistance}"));
                }
                else
                    messageSender(DefineCmdOutput.ErrorTemplate("Value Incorrect"));
            }
            else
                messageSender(DefineCmdOutput.ErrorTemplate("Missing Value"));
        }
    }
}
