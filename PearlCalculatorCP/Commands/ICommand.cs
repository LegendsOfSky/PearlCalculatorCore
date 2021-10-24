using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public interface ICommand
    {
        public void Execute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender);
    }

    public interface ICommandOutputLink
    {
        public void OnLinkOutput(Action<ConsoleOutputItemModel> messageSender);
    }
}