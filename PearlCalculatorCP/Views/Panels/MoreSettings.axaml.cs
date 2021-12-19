using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views.Panels
{
    public partial class MoreSettings : UserControl
    {
        public MoreSettings()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
