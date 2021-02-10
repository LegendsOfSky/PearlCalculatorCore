using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views
{
    public class InputBox : UserControl
    {
        public InputBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
