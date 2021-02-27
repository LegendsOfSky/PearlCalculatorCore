using System;
using System.Text;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Commands
{
    public class ChangeLanguage : ICommand
    {
        public void OnLinkOutput(Action<ConsoleOutputItemModel> messageSender)
        {
            if (Translator.Instance.CurrentLanguage == Translator.Fallbacklanguage)
            {
                messageSender(new ConsoleOutputItemModel("Msg/i18n", "can't load i18n file"));
                messageSender(new ConsoleOutputItemModel("Msg/i18n", $"{Translator.Fallbacklanguage} currently in use"));
            }
        }
        
        public void Excute(string[]? parameters, Action<ConsoleOutputItemModel> messageSender)
        {
            var len = parameters?.Length ?? 0;
            var opt = len == 1 ? parameters[0].ToLower() : string.Empty;

            if (parameters is null || len != 1)
            {
                messageSender(DefineCmdOutput.ErrorTemplate($"this cmd don't accept {len} parameters"));

                StringBuilder sb = new StringBuilder();
                foreach (var lang in Translator.Instance.Languages)
                    sb.Append($"\"{lang}\" , ");

                messageSender(DefineCmdOutput.ErrorTemplate($"optional paras: {sb}"));
            }
            else if (Translator.Instance.CurrentLanguage == opt)
                messageSender(DefineCmdOutput.MsgTemplate("you don't need change language"));
            else
            {
                if (opt == "cn" || opt == "tw" || Translator.Instance.Languages.Contains(opt))
                {
                    var lang = opt switch
                    {
                        "cn" => "zh_cn",
                        "tw" => "zh_tw",
                        _ => opt
                    };
                    
                    Translator.Instance.LoadLanguage(lang);
                    messageSender(DefineCmdOutput.MsgTemplate("change language success"));
                }
                else
                    messageSender(DefineCmdOutput.ErrorTemplate($"language option \"{parameters[0]}\" not found"));
            }
        }
    }
}