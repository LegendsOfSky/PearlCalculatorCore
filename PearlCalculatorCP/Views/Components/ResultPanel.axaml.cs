#nullable disable

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using PearlCalculatorCP.ViewModels;

namespace PearlCalculatorCP.Views.Components
{
    public class ResultPanel : UserControl
    {
        private Panel _pearlTraceItemContextPanel;
        
        private Grid _tntAmountGrid;
        private Grid _pearlTraceGrid;
        private Grid _pearlMotionGrid;
        private Grid _chunkTraceGrid;

        private static ColumnDefinition OneWidthDefinition => new ColumnDefinition {Width = new GridLength(1)};

        public ResultPanel()
        {
            InitializeComponent();
            
            (DataContext as ResultPanelViewModel).OnShowModeSet += OnShowModeSet;

            DataContextChanged += (sender, args) =>
            {
                (DataContext as ResultPanelViewModel).OnShowModeSet += OnShowModeSet;
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _pearlTraceItemContextPanel = this.FindControl<Panel>("PearlTraceItemContextPanel");
            _tntAmountGrid = this.FindControl<Grid>("TNTAmountGrid");
            _pearlTraceGrid = this.FindControl<Grid>("PearlTraceGrid");
            _pearlMotionGrid = this.FindControl<Grid>("PearlMotionGrid");
            _chunkTraceGrid = this.FindControl<Grid>("ChunkTraceGrid");
        }

        private void OnShowModeSet(ResultShowMode mode)
        {
            switch (mode)
            {
                case ResultShowMode.Amount:
                    _tntAmountGrid.ColumnDefinitions = new ColumnDefinitions
                    {
                        new ColumnDefinition {Width = GridLength.Parse("2*"), MinWidth = 30},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 30},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 30},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 30},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 30}
                    };
                    break;
                case ResultShowMode.Trace:
                    _pearlTraceGrid.ColumnDefinitions = new ColumnDefinitions
                    {
                        new ColumnDefinition {Width = GridLength.Parse("0.3*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                    };
                    break;
                case ResultShowMode.Motion:
                    _pearlMotionGrid.ColumnDefinitions = new ColumnDefinitions
                    {
                        new ColumnDefinition {Width = GridLength.Parse("0.3*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                    };
                    break;
                case ResultShowMode.ChunkTrace:
                    _chunkTraceGrid.ColumnDefinitions = new ColumnDefinitions
                    {
                        new ColumnDefinition {Width = GridLength.Parse("0.3*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50},
                        OneWidthDefinition,
                        new ColumnDefinition {Width = GridLength.Parse("*"), MinWidth = 50}
                    };
                    break;
            }
        }

        private void PearlTraceItem_OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            var grid = sender as Grid;
            if (e.GetCurrentPoint(grid).Properties.IsRightButtonPressed)
            {
                var contextMenu = _pearlTraceItemContextPanel.ContextMenu;
                contextMenu.PlacementTarget = grid;
                contextMenu.PlacementRect = grid.Bounds;
                contextMenu.Open();
            }
        }
    }
}