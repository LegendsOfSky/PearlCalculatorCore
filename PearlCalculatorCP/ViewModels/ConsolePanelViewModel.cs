using PearlCalculatorCP.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using PearlCalculatorCP.Commands;
using PearlCalculatorCP.Utils;

namespace PearlCalculatorCP.ViewModels
{
    class ConsolePanelViewModel : ViewModelBase
    {
        private string _commandText = string.Empty;
        public string CommandText
        {
            get => _commandText;
            set => this.RaiseAndSetIfChanged(ref _commandText, value);
        }

        private ObservableCollection<ConsoleOutputItemModel>? _consoleOutputs;
        public ObservableCollection<ConsoleOutputItemModel>? ConsoleOutputs
        {
            get => _consoleOutputs;
            set => this.RaiseAndSetIfChanged(ref _consoleOutputs, value);
        }

        public ConsolePanelViewModel()
        {
            ConsoleOutputs ??= new ObservableCollection<ConsoleOutputItemModel>();
            
            CommandManager.Instance.OnMessageSend += AddConsoleMessage;
            LogUtils.OnLog += AddConsoleMessage;
            Clear.OnExcute += ClearConsole;
        }
        
        public void SendCmd()
        {
            if(string.IsNullOrEmpty(CommandText) || string.IsNullOrWhiteSpace(CommandText) || CommandText[0] != '/')
                return;

            var cmd = CommandText[1..];
            CommandText = string.Empty;
            
            CommandManager.Instance.ExecuteCommand(cmd);
        }
        
        private void AddConsoleMessage(ConsoleOutputItemModel message)
        {
            if(ConsoleOutputs.Count >= 500)
                for (int i = 0; i < 50; i++)
                    ConsoleOutputs.RemoveAt(0);
            
            ConsoleOutputs.Add(message);
        }

        private void ClearConsole() => ConsoleOutputs?.Clear();

    }
}
