using System.Collections.Generic;
using System.IO;
using System.Text.Json;

#nullable disable

namespace PearlCalculatorCP
{
    class AppSettings
    {
        private static readonly string FilePath = $"{ProgramInfo.BaseDirectory}AppSettings.json";

        private static AppSettings _instance;

        public static AppSettings Instance
        {
            get
            {
                if (_instance is { }) return _instance;
                
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    _instance = JsonSerializer.Deserialize<AppSettings>(json);
                    return _instance;
                }

                return _instance = new AppSettings();
            }
        }

        private bool _hasChanged;
        
        public string Lanuage { get; set; } = string.Empty;
        public string DefaultLoadSettingsFile { get; set; } = string.Empty;

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
    }

    static class AppRuntimeSettings
    {
        private static Dictionary<string, object> _settings = new Dictionary<string, object>();
        public static Dictionary<string, object> Settings => _settings;
    }
}