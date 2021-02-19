using Avalonia.Media;

namespace PearlCalculatorCP.Models
{
    public class ConsoleOutputItemModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public IBrush TextColor { get; set; } = Brushes.Black;

        public ConsoleOutputItemModel() { }

        public ConsoleOutputItemModel(string type, string message, IBrush textColor = null)
        {
            this.Type = type;
            this.Message = message;
            this.TextColor = textColor is null ? Brushes.Black : textColor;
        }
    }
}