namespace PearlCalculatorCP.Models;

public class LanguageComboBoxModel
{
    public string DisplayName { get; init; }
    public string CommandOption { get; init; }

    public LanguageComboBoxModel(string? displayName, string? commandOption)
    {
        DisplayName = displayName ?? "Empty";
        CommandOption = commandOption ?? string.Empty;
    }
}