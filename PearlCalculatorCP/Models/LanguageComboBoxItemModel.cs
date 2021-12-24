namespace PearlCalculatorCP.Models
{
    public class LanguageComboBoxItemModel
    {
        public string DisplayName { get; init; }
        public string CommandOption { get; init; }

        public LanguageComboBoxItemModel(string? displayName, string? commandOption)
        {
            DisplayName = displayName ?? "Empty";
            CommandOption = commandOption ?? string.Empty;
        }
    }
}