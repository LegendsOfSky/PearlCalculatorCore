using Avalonia.Media;

namespace PearlCalculatorCP.Models
{
    public class ConsoleOutputItemModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public IBrush TextColor { get; set; }

        public ConsoleOutputItemModel()
        {
            this.TextColor = Brushes.Black;
        }

        public ConsoleOutputItemModel(string type, string message, IBrush? textColor = null)
        {
            this.Type = type;
            this.Message = message;
            this.TextColor = textColor ?? Brushes.Black;
        }
    }
}