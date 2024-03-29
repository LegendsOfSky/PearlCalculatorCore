using System;
using Avalonia.Media;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public class ChangeLanguage : ICommand, ICommandOutputLink
    {
        public void OnLinkOutput(Action<ConsoleOutputItemModel> messageSender)
        {
            if (Translator.Instance.CurrentLanguage == Translator.FallbackLanguage)
            {
                messageSender(new ConsoleOutputItemModel("Msg/i18n", "haven't default language setting file or can't load i18n file"));
                messageSender(new ConsoleOutputItemModel("Msg/i18n", $"{Translator.FallbackLanguage} currently in use"));
            }
            else
                messageSender(new ConsoleOutputItemModel("Msg/i18n", $"i18n file \"{Translator.Instance.CurrentActivedI18nFile}\" loaded"));
        }
        
        public void Execute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            var len = parameters?.Length ?? 0;
#nullable disable
            var lang = len == 1 ? parameters[0].ToLower() : string.Empty;

            if (parameters is null || len != 1)
            {
                messageSender(DefineCmdOutput.ErrorTemplate($"\"{cmdName}\" don't accept {len} parameters"));
                messageSender(DefineCmdOutput.ErrorTemplate($"optional paras: {Translator.Instance.GetLanguagesOptional()}"));
            }
            else if (Translator.Instance.CurrentLanguage == lang)
                messageSender(DefineCmdOutput.MsgTemplate("you don't need change language"));
            else
            {
                if (lang == Translator.FallbackLanguage || Translator.Instance.Languages.FindIndex(e => e.Language == lang) != -1)
                {
                    if (Translator.Instance.LoadLanguage(lang, s => messageSender(new ConsoleOutputItemModel("Error/i18n", s, Brushes.Red))))
                        messageSender(DefineCmdOutput.MsgTemplate("change language success"));
                }
                else
                {
                    messageSender(DefineCmdOutput.ErrorTemplate($"language option \"{lang}\" not found"));
                    messageSender(DefineCmdOutput.ErrorTemplate($"optional paras: {Translator.Instance.GetLanguagesOptional()}"));
                }
            }
        }
    }
}