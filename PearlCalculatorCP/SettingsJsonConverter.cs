using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;

#nullable disable

namespace PearlCalculatorCP
{
    public class SettingsJsonConverter : JsonConverter<Settings>
    {
        private Space3DJsonConverter _space3DJsonConverter = new Space3DJsonConverter();
        
        public override Settings Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var rootElement = document.RootElement;

            var settings = new Settings();
            
            settings.NorthEastTNT = ReadSpace3D(rootElement.GetProperty(nameof(settings.NorthEastTNT)));
            settings.NorthWestTNT = ReadSpace3D(rootElement.GetProperty(nameof(settings.NorthWestTNT)));
            settings.SouthEastTNT = ReadSpace3D(rootElement.GetProperty(nameof(settings.SouthEastTNT)));
            settings.SouthWestTNT = ReadSpace3D(rootElement.GetProperty(nameof(settings.SouthWestTNT)));

            settings.Destination = ReadSpace3D(rootElement.GetProperty(nameof(settings.Destination)));
            settings.Offset = ReadSurface2D(rootElement.GetProperty(nameof(settings.Offset)));

            settings.RedTNT = rootElement.GetProperty(nameof(settings.RedTNT)).GetInt32();
            settings.BlueTNT = rootElement.GetProperty(nameof(settings.BlueTNT)).GetInt32();
            settings.MaxTNT = rootElement.GetProperty(nameof(settings.MaxTNT)).GetInt32();

            settings.Direction =
                Enum.TryParse<Direction>(rootElement.GetProperty(nameof(settings.Direction)).GetString(),
                    out var direction)
                    ? direction
                    : Direction.North;
            
            var pearlElemRoot = rootElement.GetProperty(nameof(settings.Pearl));

            settings.Pearl = new PearlEntity
            {
                Position = ReadSpace3D(pearlElemRoot.GetProperty(nameof(settings.Pearl.Position))),
                Motion = ReadSpace3D(pearlElemRoot.GetProperty(nameof(settings.Pearl.Motion)))
            };
            
            return settings;
        }

        public override void Write(Utf8JsonWriter writer, Settings value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            WriteObject(writer, options, nameof(value.NorthEastTNT), ref value.NorthEastTNT, _space3DJsonConverter);

            WriteObject(writer, options, nameof(value.NorthWestTNT), ref value.NorthWestTNT, _space3DJsonConverter);

            WriteObject(writer, options, nameof(value.SouthEastTNT), ref value.SouthEastTNT, _space3DJsonConverter);

            WriteObject(writer, options, nameof(value.SouthWestTNT), ref value.SouthWestTNT, _space3DJsonConverter);
            
            WriteObject(writer, options, nameof(value.Destination), ref value.Destination, _space3DJsonConverter);

            WriteObject(writer, options, nameof(value.Offset), ref value.Offset, o =>
            {
                writer.WriteNumber("X", o.X);
                writer.WriteNumber("Z", o.Z);
            });
            
            WriteObject(writer, options, nameof(value.Pearl), ref value.Pearl, o =>
            {
                WriteObject(writer, options, nameof(o.Position), ref o.Position, _space3DJsonConverter);
                WriteObject(writer, options, nameof(o.Motion), ref o.Motion, _space3DJsonConverter);
            });
            
            writer.WriteNumber(nameof(value.RedTNT), value.RedTNT);
            writer.WriteNumber(nameof(value.BlueTNT), value.BlueTNT);
            writer.WriteNumber(nameof(value.MaxTNT), value.MaxTNT);
            
            writer.WriteString(nameof(value.Direction), value.Direction.ToString());
            
            writer.WriteEndObject();
        }

        private void WriteObject<T>(Utf8JsonWriter writer, JsonSerializerOptions options, string propertyName, ref T value, JsonConverter<T> converter)
        {
            writer.WriteStartObject(propertyName);
            converter.Write(writer, value, options);
            writer.WriteEndObject();
        }
        
        private void WriteObject<T>(Utf8JsonWriter writer, JsonSerializerOptions options, string propertyName, ref T value, Action<T> action)
        {
            writer.WriteStartObject(propertyName);
            action?.Invoke(value);
            writer.WriteEndObject();
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

    class Space3DJsonConverter : JsonConverter<Space3D>
    {
        public override Space3D Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Space3D value, JsonSerializerOptions options)
        {
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteNumber("Z", value.Z);
        }
    }
}