#if NET6_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorLib.Settings
{
    public static class JsonUtility
    {
        private static readonly JsonSerializerOptions DefaultReadConverter = new JsonSerializerOptions { Converters = { new SettingsJsonConverter() } };

        private static readonly JsonSerializerOptions NewVersionReadOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true , 
            IncludeFields = true, 
            IgnoreReadOnlyProperties = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static string Serialize(SettingsCollection settings , JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(settings , options ?? DefaultSerializerOptions);
        }

        public static byte[] SerializeToUtf8Bytes(SettingsCollection settings , JsonSerializerOptions options = null)
        {
            return JsonSerializer.SerializeToUtf8Bytes(settings, options ?? DefaultSerializerOptions);
        }

        public static SettingsCollection DeSerialize(string json)
        {
            return JsonSerializer.Deserialize<SettingsCollection>(json , DefaultReadConverter);
        }

        internal class SettingsJsonConverter : JsonConverter<SettingsCollection>
        {
            public override SettingsCollection Read(ref Utf8JsonReader reader , Type typeToConvert , JsonSerializerOptions options)
            {
                JsonDocument document = JsonDocument.ParseValue(ref reader);
                if (document.RootElement.TryGetProperty("Version" , out JsonElement ver))
                {
                    string str = ver.GetString();
                    if (string.IsNullOrEmpty(str) || str.Length <= "2.7".Length)
                    {
                        return ReadOldSettings(document);
                    }
                }

                return document.Deserialize<SettingsCollection>(NewVersionReadOptions);
            }

            public override void Write(Utf8JsonWriter writer , SettingsCollection value , JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            private SettingsCollection ReadOldSettings(JsonDocument document)
            {
                JsonElement root = document.RootElement;

                SettingsCollection result = new SettingsCollection();

                result.Version = SettingsCollection.CurrentVersion;

                result.RedTNT = root.GetProperty(nameof(result.RedTNT)).GetInt32();
                result.BlueTNT = root.GetProperty(nameof(result.BlueTNT)).GetInt32();

                result.SelectedCannon = "Default";

                {
                    result.Direction =
                        Enum.TryParse<Direction>(root.GetProperty(nameof(result.Direction)).GetString() ,
                            out var direction)
                            ? direction
                            : Direction.North;
                }

                result.Destination = ReadSurface2D(root.GetProperty(nameof(result.Destination)));

#region read cannon settings
                CannonSettings cannon = new CannonSettings();

                cannon.CannonName = "Default";

                cannon.MaxTNT = root.GetProperty(nameof(cannon.MaxTNT)).GetInt32();

                {
                    cannon.DefaultRedDirection = root.TryGetProperty("DefaultRedTNTDirection" , out var drt) &&
                        Enum.TryParse<Direction>(drt.GetString() , out var direction)
                            ? direction
                            : PearlCalculatorLib.General.Data.DefaultRedDuper;
                }

                {
                    cannon.DefaultBlueDirection = root.TryGetProperty("DefaultBlueTNTDirection" , out var drt) &&
                        Enum.TryParse<Direction>(drt.GetString() , out var direction)
                            ? direction
                            : PearlCalculatorLib.General.Data.DefaultBlueDuper;
                }

                cannon.NorthWestTNT = ReadSpace3D(root.GetProperty(nameof(cannon.NorthWestTNT)));
                cannon.NorthEastTNT = ReadSpace3D(root.GetProperty(nameof(cannon.NorthEastTNT)));
                cannon.SouthWestTNT = ReadSpace3D(root.GetProperty(nameof(cannon.SouthWestTNT)));
                cannon.SouthEastTNT = ReadSpace3D(root.GetProperty(nameof(cannon.SouthEastTNT)));

                var pearlElemRoot = root.GetProperty(nameof(cannon.Pearl));
                cannon.Pearl = new PearlCalculatorLib.PearlCalculationLib.Entity.PearlEntity
                {
                    Position = ReadSpace3D(pearlElemRoot.GetProperty(nameof(cannon.Pearl.Position))) ,
                    Motion = ReadSpace3D(pearlElemRoot.GetProperty(nameof(cannon.Pearl.Motion)))
                };
                
                cannon.Offset = ReadSurface2D(root.GetProperty(nameof(cannon.Offset)));

                cannon.RedTNTConfiguration = new List<int>();
                cannon.BlueTNTConfiguration = new List<int>();
#endregion

                result.CannonSettings = new[] { cannon };

                return result;
            }

            private Space3D ReadSpace3D(JsonElement elem)
            {
                return new Space3D
                {
                    X = elem.GetProperty("X").GetDouble(),
                    Y = elem.GetProperty("Y").GetDouble(),
                    Z = elem.GetProperty("Z").GetDouble()
                };
            }

            private Surface2D ReadSurface2D(JsonElement elem)
            {
                return new Surface2D
                {
                    X = elem.GetProperty("X").GetDouble(),
                    Z = elem.GetProperty("Z").GetDouble()
                };
            }
        }
    }

}

#endif