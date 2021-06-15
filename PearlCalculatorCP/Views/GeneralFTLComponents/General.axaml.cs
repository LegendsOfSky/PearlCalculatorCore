using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views.GeneralFTLComponents
{
    public partial class General : UserControl
    {
        public General()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
