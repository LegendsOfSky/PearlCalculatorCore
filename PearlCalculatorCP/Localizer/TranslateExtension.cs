using System;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace PearlCalculatorCP.Localizer
{
    public class TranslateExtension : MarkupExtension
    {
        public string Key { get; set; }
        public string Fallback { get; set; } = string.Empty;
        
        public TranslateExtension(string key)
        {
            this.Key = key;
        }
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new ReflectionBindingExtension($"[{Key}]")
            {
                Mode = BindingMode.TwoWay,
                Source = Translator.Instance,
            };
            
            Translator.Instance.AddFallbackTranslate(Key, Fallback);

            return binding.ProvideValue(serviceProvider);
        }
    }
}