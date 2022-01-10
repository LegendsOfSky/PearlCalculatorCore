#nullable disable

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PearlCalculatorCP.ViewModels;

namespace PearlCalculatorCP.Views.Panels
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
            var pointer = e.GetCurrentPoint(grid);
            if (pointer.Properties.IsRightButtonPressed)
            {
                var contextMenu = _pearlTraceItemContextPanel.ContextMenu;
                contextMenu.PlacementTarget = grid;
                contextMenu.PlacementRect = new Rect(pointer.Position, grid.Bounds.Size);
                contextMenu.Open();
            }
        }
    }
}