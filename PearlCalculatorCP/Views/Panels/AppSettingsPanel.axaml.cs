using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PearlCalculatorCP.Views.Panels;

public class AppSettingsPanel : UserControl
{
    public event EventHandler<RoutedEventArgs>? OnSetDefaultSettingsClick; 

    public AppSettingsPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void SetDefaultSettings(object sender, RoutedEventArgs e) => OnSetDefaultSettingsClick?.Invoke(sender, e);

}