using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public interface ICommand
    {
        public virtual void OnLinkOutput(Action<ConsoleOutputItemModel> messageSender){}
        
        public void Excute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender);
    }
}