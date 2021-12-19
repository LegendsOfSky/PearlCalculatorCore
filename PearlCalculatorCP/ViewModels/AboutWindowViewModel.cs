using Avalonia;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class AboutWindowViewModel : ViewModelBase, IWindowViewModelScale
    {
        private static readonly Size DefaultWindowSize = new Size(600, 350);
        
        private Size _windowSize = DefaultWindowSize;
        public Size WindowSize
        {
            get => _windowSize;
            private set => this.RaiseAndSetIfChanged(ref _windowSize, value);
        }

        private double _windowScale = 1.0d;
        public double WindowScale
        {
            get => _windowScale;
            set
            {
                this.RaiseAndSetIfChanged(ref _windowScale, value);
                WindowSize = DefaultWindowSize * value;
            }
        }

        public AboutWindowViewModel()
        {
            this.ApplyScale();
        }
    }
}