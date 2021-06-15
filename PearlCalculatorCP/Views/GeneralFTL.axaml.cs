using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views
{
    public partial class GeneralFTL : UserControl
    {
        public GeneralFTL()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
