using Microsoft.Win32.SafeHandles;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace PCCSettingsGenerator
{
    class Program
    {
        private static readonly SettingsJsonConverter JsonConverter = new SettingsJsonConverter();

        private static JsonSerializerOptions WriteSerializerOptions = new JsonSerializerOptions
        {
            Converters = { JsonConverter } ,
            WriteIndented = true
        };

        private static ResourceManager manager = new ResourceManager("PCCSettingsGenerator.Resources.Language" , Assembly.GetExecutingAssembly());

        static void Main(string[] args)
        {
            #region Initialize

            string temp = "Start";
            try
            {
                Console.WriteLine(manager.GetObject("Language"));
            }
            catch(MissingManifestResourceException)
            {
                LanguageRequest();
            }
            Console.WriteLine(manager.GetObject("Translate"));
            Console.WriteLine(manager.GetObject("PressKey"));
            Console.ReadKey();
            Clear();
            Console.ReadKey();
            Settings settings = new Settings
            {
                Pearl = new PearlEntity() ,
                Offset = new Surface2D()
            };
            PearlEntity pearl = new PearlEntity();
            Surface2D chamber = new Surface2D();

            #endregion

            do
            {

                #region Input

                if(temp == "1" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("Chamber"));
                    chamber = ReadSurface2DFromConsole((string)manager.GetObject("ChamberParameter"));
                }
                if(temp == "2" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("PearlCoordinate"));
                    settings.Pearl.Position = ReadSpace3DFromConsole((string)manager.GetObject("PearlCoordinateParameter"));
                }
                if(temp == "3" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("PearlMotion"));
                    settings.Pearl.Motion = ReadSpace3DFromConsole((string)manager.GetObject("PearlMotionParameter"));
                }
                if(temp == "4" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("NorthWestTNT"));
                    settings.NorthWestTNT = ReadSpace3DFromConsole((string)manager.GetObject("NorthWestTNTParameter"));
                }
                if(temp == "5" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("NorthEastTNT"));
                    settings.NorthEastTNT = ReadSpace3DFromConsole((string)manager.GetObject("NorthEastTNTParameter"));
                }
                if(temp == "6" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("SouthWestTNT"));
                    settings.SouthWestTNT = ReadSpace3DFromConsole((string)manager.GetObject("SouthWestTNTParameter"));
                }
                if(temp == "7" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("SouthEastTNT"));
                    settings.SouthEastTNT = ReadSpace3DFromConsole((string)manager.GetObject("SouthEastTNTParameter"));
                }
                if(temp == "9" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("AAPressKey"));
                    Console.WriteLine();
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.WriteLine(@"      A |                                 | B                         ");
                    Console.WriteLine(@"--------+---------------------------------+--------                   ");
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.WriteLine(@"        |                                 |                      N    ");
                    Console.WriteLine(@"        |                                 |                      ^    ");
                    Console.WriteLine(@"        |                                 |                     /|\   ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                      |    ");
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.WriteLine(@"--------+---------------------------------+--------                   ");
                    Console.WriteLine(@"      C |                                 | D                         ");
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.WriteLine(@"        |                                 |                           ");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine(manager.GetObject("StatementHeader"));
                    SeparatingLine();
                    Console.WriteLine(manager.GetObject("Statement0"));
                    Console.WriteLine(manager.GetObject("Statement1"));
                    Console.WriteLine(manager.GetObject("Statement2"));
                    Console.WriteLine(manager.GetObject("Statement3"));
                    Console.WriteLine(manager.GetObject("Statement4"));
                    Console.WriteLine(manager.GetObject("Statement5"));
                    SeparatingLine();
                    Console.WriteLine(manager.GetObject("TNTSymbol"));
                    string temp2 = Console.ReadLine().ToUpper();
                    while(temp2 != "A" && temp2 != "B" && temp2 != "C" && temp2 != "D")
                    {
                        SeparatingLine();
                        Console.WriteLine(manager.GetObject("WrongFormat"));
                        Console.WriteLine(manager.GetObject("TNTSymbolParameter"));
                        temp2 = Console.ReadLine();
                    }
                    switch(temp2)
                    {
                        case "A":
                            settings.DefaultRedTNTDirection = Direction.NorthWest;
                            settings.DefaultBlueTNTDirection = Direction.SouthEast;
                            break;
                        case "B":
                            settings.DefaultRedTNTDirection = Direction.NorthEast;
                            settings.DefaultBlueTNTDirection = Direction.SouthWest;
                            break;
                        case "C":
                            settings.DefaultRedTNTDirection = Direction.SouthWest;
                            settings.DefaultBlueTNTDirection = Direction.NorthEast;
                            break;
                        case "D":
                            settings.DefaultRedTNTDirection = Direction.SouthEast;
                            settings.DefaultBlueTNTDirection = Direction.NorthWest;
                            break;
                    }
                }
                if(temp == "8" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine(manager.GetObject("MaxTNT"));
                    temp = Console.ReadLine();
                    while(!int.TryParse(temp , out settings.MaxTNT))
                    {
                        Clear();
                        Console.WriteLine(manager.GetObject("WrongFormat"));
                        Console.WriteLine(manager.GetObject("MaxTNTParamater"));
                        temp = Console.ReadLine();
                    }
                }

                #endregion

                #region Verify

                Clear();
                Console.WriteLine(manager.GetObject("ParameterHeader"));
                Console.WriteLine((string)manager.GetObject("Parameter0") + chamber);
                Console.WriteLine((string)manager.GetObject("Parameter1") + settings.Pearl.Position);
                Console.WriteLine((string)manager.GetObject("Parameter2") + settings.Pearl.Motion.ToString());
                Console.WriteLine((string)manager.GetObject("Parameter3") + settings.NorthWestTNT);
                Console.WriteLine((string)manager.GetObject("Parameter4") + settings.NorthEastTNT);
                Console.WriteLine((string)manager.GetObject("Parameter5") + settings.SouthWestTNT);
                Console.WriteLine((string)manager.GetObject("Parameter6") + settings.SouthEastTNT);
                Console.WriteLine((string)manager.GetObject("Parameter7") + settings.MaxTNT.ToString());
                Console.WriteLine((string)manager.GetObject("Parameter8") + settings.DefaultRedTNTDirection.ToString());
                Console.WriteLine(manager.GetObject("CorfirmParameter"));
                temp = Console.ReadLine().ToUpper();
                while(temp != "Y" && temp != "N")
                {
                    Console.WriteLine(manager.GetObject("UnexpectedResponseSettings"));
                    temp = Console.ReadLine().ToUpper();
                }
                if(temp == "N")
                {
                    Console.WriteLine(manager.GetObject("ChangeParameter"));
                    temp = Console.ReadLine();
                    while(!(int.TryParse(temp , out int i) && i < 10 && i > 0))
                    {
                        Console.WriteLine(manager.GetObject("WrongFormat"));
                        Console.WriteLine(manager.GetObject("ChangeParameter"));
                        temp = Console.ReadLine();
                    }
                }

                #endregion

            } while(temp != "Y");

            #region General settings.json

            settings.NorthWestTNT -= chamber.ToSpace3D();
            settings.NorthEastTNT -= chamber.ToSpace3D();
            settings.SouthWestTNT -= chamber.ToSpace3D();
            settings.SouthEastTNT -= chamber.ToSpace3D();
            settings.Offset = settings.Pearl.Position.ToSurface2D() - chamber;
            settings.Pearl.Position.X = 0;
            settings.Pearl.Position.Z = 0;
            settings.Direction = Direction.North;
            Clear();
            Console.WriteLine(manager.GetObject("SettingHeader"));
            Console.WriteLine((string)manager.GetObject("Setting0") + settings.MaxTNT.ToString());
            Console.WriteLine((string)manager.GetObject("Setting1") + settings.NorthWestTNT.ToString());
            Console.WriteLine((string)manager.GetObject("Setting2") + settings.NorthEastTNT.ToString());
            Console.WriteLine((string)manager.GetObject("Setting3") + settings.SouthWestTNT.ToString());
            Console.WriteLine((string)manager.GetObject("Setting4") + settings.SouthEastTNT.ToString());
            Console.WriteLine((string)manager.GetObject("Setting5") + settings.Pearl.Position.Y.ToString());
            Console.WriteLine((string)manager.GetObject("Setting6") + settings.Pearl.Motion.ToString());
            Console.WriteLine((string)manager.GetObject("Setting7") + settings.Offset.ToString());
            Console.WriteLine((string)manager.GetObject("Setting8") + settings.DefaultRedTNTDirection.ToString());
            Console.WriteLine((string)manager.GetObject("Setting9") + settings.DefaultBlueTNTDirection.ToString());
            Console.WriteLine((string)manager.GetObject("ConfirmSave"));
            temp = Console.ReadLine().ToUpper();
            while(temp != "Y" && temp != "N")
            {
                Console.WriteLine((string)manager.GetObject("UnexpectedResponseJson"));
                temp = Console.ReadLine().ToUpper();
            }
            Clear();
            if(temp == "Y")
            {
                SaveSettingsToJson("./settings.json" , settings);
                Console.WriteLine((string)manager.GetObject("FileSaved"));
            }
            Console.WriteLine((string)manager.GetObject("PressKeyExist"));
            Console.ReadKey();

            #endregion
        }

        private static void SaveSettingsToJson(string path , Settings settings)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(settings , WriteSerializerOptions);

            using var sr = File.OpenWrite(path);
            sr.SetLength(0);
            sr.Write(json.AsSpan());
        }


        private static Space3D ReadSpace3DFromConsole(string valueName)
        {
            Space3D result;
            Console.WriteLine(valueName + " X :");
            string temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.X))
            {
                Console.WriteLine(manager.GetObject("WrongFormat"));
                Console.WriteLine(valueName + " X :");
                temp = Console.ReadLine();
            }
            Console.WriteLine(valueName + " Y :");
            temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.Y))
            {
                Console.WriteLine(manager.GetObject("WrongFormat"));
                Console.WriteLine(valueName + " X :");
                temp = Console.ReadLine();
            }
            Console.WriteLine(valueName + " Z :");
            temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.Z))
            {
                Console.WriteLine(manager.GetObject("WrongFormat"));
                Console.WriteLine(valueName + " Z :");
                temp = Console.ReadLine();
            }
            return result;
        }

        private static Surface2D ReadSurface2DFromConsole(string valueName)
        {
            Surface2D result = new Surface2D();
            Console.WriteLine(valueName + " X :");
            string temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.X))
            {
                Console.WriteLine(manager.GetObject("WrongFormat"));
                Console.WriteLine(valueName + " X :");
                temp = Console.ReadLine();
            }
            Console.WriteLine(valueName + " Z :");
            temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.Z))
            {
                Console.WriteLine(manager.GetObject("WrongFormat"));
                Console.WriteLine(valueName + " Z :");
                temp = Console.ReadLine();
            }
            return result;
        }

        private static void Clear()
        {
            Console.Clear();
            Console.WriteLine(manager.GetObject("Welcome"));
            Console.WriteLine(manager.GetObject("Caution"));
            Console.WriteLine("=====================================================================================================================");
            Console.WriteLine(manager.GetObject("NoteHeader"));
            Console.WriteLine(manager.GetObject("Note0"));
            Console.WriteLine(manager.GetObject("Note1"));
            Console.WriteLine(manager.GetObject("Note2"));
            Console.WriteLine(manager.GetObject("StartPressKey"));
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void SeparatingLine() => Console.WriteLine(manager.GetObject("Separate"));

        private static void LanguageRequest()
        {
            int i = 0;
            Console.WriteLine("Available language option");
            foreach(var item in Enum.GetValues(typeof(LanguageType)))
                Console.WriteLine((++i).ToString() + ") " + LanguageTypeUtils.ToString((LanguageType)item));
            Console.WriteLine("Please choose a language.");
            int.TryParse(Console.ReadLine() , out i);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(LanguageTypeUtils.ToString((LanguageType)(i - 1)));
            Console.WriteLine(manager.GetObject("Language"));
        }
    }
}
