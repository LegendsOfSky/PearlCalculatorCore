using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PearlCalculatorCP.Utils;
using PearlCalculatorCP.Views;

#nullable disable

namespace PearlCalculatorCP
{
    public class AboutWindow : Window
    {
        
        private static AboutWindow _window;

        private TextBlock _verTextTB;
        
        private bool _isPressed;
        private Point _pressedPoint;
        
        public AboutWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _verTextTB = this.FindControl<TextBlock>("VerTextTB");
#if DEBUG
            _verTextTB.Text = $"{ProgramInfo.Version} (development)";
#else
            _verTextTB.Text = $"{ProgramInfo.Version} (release)";
#endif
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        public static void OpenWindow(Window parent)
        {
            if (_window is null)
            {
                _window = new AboutWindow();
                _window.Show(parent);
            }
            
            var x = (int)((parent.Width - _window.Width) * 0.5) + parent.Position.X;
            var y = (int)((parent.Height - _window.Height) * 0.5) + parent.Position.Y;
            _window.Position = new PixelPoint(x, y);
        }

        public static void CloseWindow() => _window?.Close();
        
        private void TopLevel_OnClosed(object sender, EventArgs e) => _window = null;

        private void DiscordUrlBtn_OnClick(object sender, RoutedEventArgs e) => UrlUtils.OpenUrl("https://discord.gg/MMzsVuuSxT");

        private void Window_OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            _pressedPoint = e.GetPosition(null);
            _isPressed = true;
        }

        private void Window_OnPinterReleased(object sender, PointerReleasedEventArgs e) => _isPressed = false;

        private void Window_OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (!_isPressed) return;
            var pointerPos = e.GetPosition(null);
            var offset = pointerPos - _pressedPoint;
            this.Position += PixelPoint.FromPoint(offset, 1);
        }
    }
}