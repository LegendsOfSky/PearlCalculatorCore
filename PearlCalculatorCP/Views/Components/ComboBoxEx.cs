#nullable disable

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;

namespace PearlCalculatorCP.Views.Components
{
    public class ComboBoxEx : ComboBox
    {
        public static readonly StyledProperty<IDataTemplate> SelectionBoxItemTemplateProperty =
            AvaloniaProperty.Register<ComboBoxEx, IDataTemplate>(nameof(SelectionBoxItemTemplate));
        
        public IDataTemplate SelectionBoxItemTemplate
        {
            get => GetValue(SelectionBoxItemTemplateProperty);
            set => SetValue(SelectionBoxItemTemplateProperty, value);
        }
        
        private IItemContainerGenerator _selectionItemContainerGenerator;
        public IItemContainerGenerator SelectionItemContainerGenerator
        {
            get
            {
                if (_selectionItemContainerGenerator == null)
                {
                    _selectionItemContainerGenerator = new ItemContainerGenerator(this);

                    if (_selectionItemContainerGenerator != null)
                    {
                        _selectionItemContainerGenerator.ItemTemplate = SelectionBoxItemTemplate;
                        _selectionItemContainerGenerator.Materialized += (_, e) => OnContainersMaterialized(e);
                        _selectionItemContainerGenerator.Dematerialized += (_, e) => OnContainersDematerialized(e);
                        _selectionItemContainerGenerator.Recycled += (_, e) => OnContainersRecycled(e);
                    }
                }

                return _selectionItemContainerGenerator;
            }
        }


        static ComboBoxEx()
        {
            SelectionBoxItemTemplateProperty.Changed.AddClassHandler<ComboBoxEx>((x, e) =>
                 x.SelectionBoxItemTemplateChanged(e));
        }

        private void SelectionBoxItemTemplateChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (_selectionItemContainerGenerator != null)
            {
                _selectionItemContainerGenerator.ItemTemplate = (IDataTemplate)e.NewValue;
            }
        }
    }
}