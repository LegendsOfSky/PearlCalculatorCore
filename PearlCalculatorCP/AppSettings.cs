using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace PearlCalculatorCP
{
    [Serializable]
    class AppSettings
    {
        private static readonly string FilePath = $"{ProgramInfo.BaseDirectory}AppSettings.json";

        private static AppSettings _instance;

        public static AppSettings Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    var options = new JsonSerializerOptions {Converters = {new AppSettingsConverter()}};
                    _instance = JsonSerializer.Deserialize<AppSettings>(json, options);
                    return _instance;
                }

                return _instance = new();
            }
        }

        private bool _hasChanged;
        
        public string Language { get; set; } = string.Empty;
        public string DefaultLoadSettingsFile { get; set; } = string.Empty;

        [JsonConstructor]
        private AppSettings() { }

        public static void Save()
        {
            if (!Instance._hasChanged && File.Exists(FilePath)) return;

            var json = JsonSerializer.SerializeToUtf8Bytes(Instance);
            using var fs = File.OpenWrite(FilePath);
            fs.SetLength(0);
            fs.Write(json);
        }

        public void MarkPropertyChanged() => _hasChanged = true;

        private class AppSettingsConverter : JsonConverter<AppSettings>
        {
            public override AppSettings Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var root = JsonDocument.ParseValue(ref reader).RootElement;
                var settings = new AppSettings();
                if (root.TryGetProperty(nameof(settings.Language), out var value))
                    settings.Language = value.GetString();
                
                if (root.TryGetProperty(nameof(settings.DefaultLoadSettingsFile), out value))
                    settings.DefaultLoadSettingsFile = value.GetString();

                return settings;
            }

            public override void Write(Utf8JsonWriter writer, AppSettings value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }
    }

    static class AppRuntimeSettings
    {
        public static readonly Dictionary<string, object> Settings = new();
    }
}