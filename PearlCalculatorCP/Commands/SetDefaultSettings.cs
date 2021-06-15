using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public class SetDefaultSettings : ICommand
    {
        public void Execute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            if (parameters is null || parameters.Length != 1)
            {
                messageSender(DefineCmdOutput.ErrorTemplate("parameters length isn't 1"));
                return;
            }

            AppSettings.Instance.DefaultLoadSettingsFile = parameters[0];
            AppSettings.Instance.MarkPropertyChanged();
            messageSender(DefineCmdOutput.MsgTemplate("will auto import settings file at app launch"));
        }
    }
}