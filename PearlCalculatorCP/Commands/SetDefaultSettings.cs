using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public class SetDefaultSettings : ICommand
    {
        public void Excute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            if (parameters is null || parameters.Length != 1)
            {
                messageSender(DefineCmdOutput.ErrorTemplate("parameters length isn't 1"));
                return;
            }

            AppSettings.Instance.DefaultLoadSettingsFile = parameters[0];
            AppSettings.Instance.MarkProeprtyChanged();
            messageSender(DefineCmdOutput.MsgTemplate("will auto import settings file at app launch"));
        }
    }
}