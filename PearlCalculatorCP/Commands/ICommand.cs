using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public interface ICommand
    {
        public void Excute(string[]? paramaters, Action<ConsoleOutputItemModel> messageSender);
    }
}