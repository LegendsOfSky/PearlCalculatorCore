using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PearlCalculatorCP.ViewModels;

namespace PearlCalculatorCP.Views.Components
{
    public partial class ConsolePanel : UserControl
    {
        private TextBox _consoleInputBox;
        //private ListBox _consoleOutputListBox;

        private List<string> _commandHistory = new List<string>(100);
        private int _historyIndex = -1;
        
        public ConsolePanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
            _consoleInputBox = this.FindControl<TextBox>("ConsoleInputBox");
            //_consoleOutputListBox = this.FindControl<ListBox>("ConsoleOutputListBox");
        }

        private void OnCmdInput_KeyUp(object? sender, KeyEventArgs e)
        {
            bool isSetCaretIndex = false;
            var vm = DataContext as ConsolePanelViewModel;
            
            if (e.Key == Key.Enter)
            {
                _commandHistory.Add(vm.CommandText);
                _historyIndex = -1;
                vm.SendCmd();
                
                //(_consoleOutputListBox.Scroll as ScrollViewer).ScrollToEnd();
            }
            else if (e.Key == Key.Up)
            {
                if (_historyIndex == -1)
                    _historyIndex = _commandHistory.Count - 1;
                else if (_historyIndex >= 0)
                {
                    if (--_historyIndex == -1)
                        _historyIndex = -2;
                }
                
                isSetCaretIndex = true;
            }
            else if (e.Key == Key.Down)
            {
                if (_historyIndex == -2)
                    _historyIndex = 0;
                else if (_historyIndex < _commandHistory.Count)
                    _historyIndex++;
                
                isSetCaretIndex = true;
            }

            if (isSetCaretIndex)
            {
                vm.CommandText = _historyIndex >= 0 && _historyIndex < _commandHistory.Count
                    ? _commandHistory[_historyIndex]
                    : string.Empty;

                _consoleInputBox.CaretIndex = vm.CommandText.Length;
            }
        }
    }
}
