using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views.Components
{
    public class ResultPanel : UserControl
    {
        public ResultPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}