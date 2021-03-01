using System;
using System.IO;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public class SetDefaultLanguage : ICommand
    {
        public void Excute(string[]? parameters, string? cmdName, Action<ConsoleOutputItemModel> messageSender)
        {
            string lang = string.Empty;
            if (!(parameters is null))
            {
                if (parameters.Length > 1)
                {
                    messageSender(DefineCmdOutput.ErrorTemplate($"\"{cmdName}\" don't accept {parameters.Length} parameters"));
                    return;
                }
                
                if (parameters.Length == 1 && !Translator.Instance.Exists(parameters[0]))
                {
                    messageSender(DefineCmdOutput.ErrorTemplate($"language option \"{parameters[0]}\" not found"));
                    messageSender(DefineCmdOutput.ErrorTemplate($"optional paras: {Translator.Instance.GetLanguagesOptional()}"));
                    return;
                }
                
                lang = parameters.Length == 1 ? parameters[0] : Translator.Instance.CurrentLanguage;
            }
            else
                lang = Translator.Instance.CurrentLanguage;
                
            
            SetDefault(lang, messageSender);
        }

        private void SetDefault(string lang, Action<ConsoleOutputItemModel> messageSender)
        {
            if (lang == Translator.Fallbacklanguage)
                messageSender(DefineCmdOutput.ErrorTemplate($"current language is \"{Translator.Fallbacklanguage}\", can't set to default"));
            else
            {
                var path = $"{ProgramInfo.BaseDirectory}language";

                if (!File.Exists(path)) 
                    File.Create(path).Dispose();
                
                File.WriteAllText(path, lang);

                messageSender(DefineCmdOutput.MsgTemplate("default language option changed"));
                if (lang != Translator.Instance.CurrentLanguage)
                {
                    Translator.Instance.LoadLanguage(lang);
                    messageSender(DefineCmdOutput.MsgTemplate("change language success"));
                }
            }
        }
    }
}