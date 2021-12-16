using System;
using System.ComponentModel.DataAnnotations;
using PearlCalculatorLib;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System.Collections.Generic;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.IO.Compression;
using PearlCalculatorLib.Manually;
using PearlCalculatorLib.Result;
using PearlCalculatorIntermediateLib.Settings;
using System.Text.Json;

namespace PearlCalculatorCore
{
    class Program
    {
        static void Main(string[] args)
        {
            CannonSettings cannonSettings = new CannonSettings()
            {
                CannonName = "Test",
                MaxTNT = 0,
                DefaultBlueDirection = Direction.NorthEast,
                DefaultRedDirection = Direction.SouthWest,
                NorthEastTNT = Data.NorthEastTNT,
                NorthWestTNT = Data.NorthWestTNT,
                SouthEastTNT = Data.SouthEastTNT,
                SouthWestTNT = Data.SouthWestTNT,
                Pearl = Data.Pearl,
                Offset = Data.PearlOffset,
            };

            SettingsColletion settingsColletion = new SettingsColletion()
            {
                RedTNT = 10,
                BlueTNT = 10,
                Version = SettingsColletion.CurrentVersion,
                SelectedCannon = "Test",
                Direction = Direction.North,
                Destination = new Surface2D(10 , 10),
                CannonSettings = new CannonSettings[] {cannonSettings }
            };

            JsonSerializerOptions option = new JsonSerializerOptions { WriteIndented = true , IncludeFields = true};
            string json = JsonSerializer.Serialize(settingsColletion , option);
            File.WriteAllText(@"G:\json.json" , json);

            string text = File.ReadAllText(@"G:\SMT_588FTL_by_LegendsOfSky.json");
            SettingsColletion collection = JsonUtils.DeSerialize(text);
        }
    }
}
