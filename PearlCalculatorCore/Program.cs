using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using System.Collections.Generic;
using System.IO;
using PearlCalculatorLib.Settings;

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
                RedTNTConfiguration = new List<int>(),
                BlueTNTConfiguration = new List<int>()
            };

            SettingsCollection settingsCollection = new SettingsCollection()
            {
                RedTNT = 10,
                BlueTNT = 10,
                Version = SettingsCollection.CurrentVersion,
                SelectedCannon = "Test",
                Direction = Direction.North,
                Destination = new Surface2D(10 , 10),
                CannonSettings = new CannonSettings[] {cannonSettings }
            };
            
            string json = JsonUtility.Serialize(settingsCollection);
            File.WriteAllText(@"G:\json.json" , json);
            
            string text = File.ReadAllText(@"G:\json.json");
            SettingsCollection collection = JsonUtility.DeSerialize(text);
        }
    }
}
