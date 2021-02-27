using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public class Clear : ICommand
    {
        public static event Action? OnExcute;

        public void Excute(string[]? parameters, Action<ConsoleOutputItemModel> messageSender)
        {
            OnExcute?.Invoke();
        }
    }
}