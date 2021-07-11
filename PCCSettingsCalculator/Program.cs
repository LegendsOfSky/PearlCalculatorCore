using Microsoft.Win32.SafeHandles;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Encodings.Web;
using System.Text.Json;

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


        static void Main(string[] args)
        {
            #region Initialize

            Clear();
            string temp = "Start";
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
                    Console.WriteLine("Please enter the corrdinate of the center Of your FTL blast chamber :");
                    chamber = ReadSurface2DFromConsole("FTL blast chamber ceter coordinate");
                }
                if(temp == "2" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please enter the coordinate of the pearl:");
                    settings.Pearl.Position = ReadSpace3DFromConsole("Pearl coordinate");
                }
                if(temp == "3" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please enter the pearl motion :");
                    settings.Pearl.Motion = ReadSpace3DFromConsole("Pearl Motion");
                }
                if(temp == "4" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please enter the coordinates of the north west TNT :");
                    settings.NorthWestTNT = ReadSpace3DFromConsole("North west TNT coordinate");
                }
                if(temp == "5" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please enter the coordinates of the north east TNT :");
                    settings.NorthEastTNT = ReadSpace3DFromConsole("North east TNT coordinate");
                }
                if(temp == "6" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please enter the coordinates of the south west TNT :");
                    settings.SouthWestTNT = ReadSpace3DFromConsole("South west TNT coordinate");
                }
                if(temp == "7" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please enter the coordinates of the south east TNT :");
                    settings.SouthEastTNT = ReadSpace3DFromConsole("South east TNT coordinate");
                }
                if(temp == "9" || temp == "Start")
                {
                    Clear();
                    Console.WriteLine("Please take a look in the following AA image and press a key");
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
                    Console.WriteLine("Please take a look in the following statement and understand it.");
                    SeparatingLine();
                    Console.WriteLine("Once your TNT was flew into the chamber.");
                    Console.WriteLine("Don't use anything to move it.");
                    Console.WriteLine("Let the TNT drop and remember it's place.");
                    Console.WriteLine("Them, enter the corresponding English symbol.");
                    Console.WriteLine("For example, the TNT was dropped in the south east corner.");
                    Console.WriteLine("The corresponding English symbol will be D.");
                    SeparatingLine();
                    Console.WriteLine("Corresponding English symbol for red TNT");
                    string temp2 = Console.ReadLine();
                    while(temp2 != "A" && temp2 != "B" && temp2 != "C" && temp2 != "D")
                    {
                        SeparatingLine();
                        Console.WriteLine("Wrong format. Please enter the following value again.");
                        Console.WriteLine("Corresponding English symbol for red TNT");
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
                    Console.WriteLine("Please enter the maximum amount of TNT :");
                    temp = Console.ReadLine();
                    while(!int.TryParse(temp , out settings.MaxTNT))
                    {
                        Clear();
                        Console.WriteLine("Wrong format. Please enter the following value again.");
                        Console.WriteLine("Maximum amount of TNT :");
                        temp = Console.ReadLine();
                    }
                }

                #endregion

                #region Verify

                Clear();
                Console.WriteLine("These are the parameters that you have entered.");
                Console.WriteLine("1) FTL blast chamber center " + chamber);
                Console.WriteLine("2) Ender Pearl " + settings.Pearl.Position);
                temp = settings.Pearl.Motion.X.ToString() + " , " + settings.Pearl.Motion.Y.ToString() + " , " + pearl.Motion.Z.ToString();
                Console.WriteLine("3) Ender Pearl motion : " + temp);
                Console.WriteLine("4) North west TNT " + settings.NorthWestTNT);
                Console.WriteLine("5) North east TNT " + settings.NorthEastTNT);
                Console.WriteLine("6) South west TNT " + settings.SouthWestTNT);
                Console.WriteLine("7) South east TNT " + settings.SouthEastTNT);
                Console.WriteLine("8) Max TNT : " + settings.MaxTNT.ToString());
                Console.WriteLine("9) Direction of the red TNT : " + settings.DefaultRedTNTDirection.ToString());
                Console.WriteLine("Please confirm if they are all correct. (Y/N)");
                temp = Console.ReadLine().ToUpper();
                while(temp != "Y" && temp != "N")
                {
                    Console.WriteLine("Unexpected response. Please confirm whether they are all correct. (Y/N)");
                    temp = Console.ReadLine().ToUpper();
                }
                if(temp == "N")
                {
                    Console.WriteLine("Please specify the number of the parameter you would like to chamge.");
                    temp = Console.ReadLine();
                    while(!(int.TryParse(temp , out int i) && i < 10 && i > 0))
                    {
                        Console.WriteLine("Wrong format. Please enter the following parameter again.");
                        Console.WriteLine("Please specify the number of the parameter you would like to chamge.");
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
            Console.WriteLine("These are the settings it had generated.");
            Console.WriteLine("1) Max TNT : " + settings.MaxTNT.ToString());
            Console.WriteLine("2) North West TNT " + settings.NorthWestTNT.ToString());
            Console.WriteLine("3) North East TNT " + settings.NorthEastTNT.ToString());
            Console.WriteLine("4) South West TNT " + settings.SouthWestTNT.ToString());
            Console.WriteLine("5) South East TNT " + settings.SouthEastTNT.ToString());
            Console.WriteLine("6) Ender Pearl Height : " + settings.Pearl.Position.Y.ToString());
            temp = settings.Pearl.Motion.X.ToString() + " , " + settings.Pearl.Motion.Y.ToString() + " , " + settings.Pearl.Motion.Z.ToString();
            Console.WriteLine("7) Ender Pearl Motion : " + temp);
            Console.WriteLine("8) Offset " + settings.Offset.ToString());
            Console.WriteLine("9) Default Red TNT Direction : " + settings.DefaultRedTNTDirection.ToString());
            Console.WriteLine("10) Default Blue TNT Direction : " + settings.DefaultBlueTNTDirection.ToString());
            Console.WriteLine("Do you want to save as settings.json? (Y/N)");
            temp = Console.ReadLine().ToUpper();
            while(temp != "Y" && temp != "N")
            {
                Console.WriteLine("Unexpected response. Do you want to save as settings.json? (Y/N)");
                temp = Console.ReadLine().ToUpper();
            }
            Clear();
            if(temp == "Y")
            {
                SaveSettingsToJson("./settings.json" , settings);
                Console.WriteLine("File saved.");
            }
            Console.WriteLine("Press any key to quit.");
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


        public static Space3D ReadSpace3DFromConsole(string valueName)
        {
            Space3D result;
            Console.WriteLine(valueName + " X :");
            string temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.X))
            {
                Console.WriteLine("Wrong format. Please enter the following parameter again.");
                Console.WriteLine(valueName + " X :");
                temp = Console.ReadLine();
            }
            Console.WriteLine(valueName + " Y :");
            temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.Y))
            {
                Console.WriteLine("Wrong format. Please enter the following parameter again.");
                Console.WriteLine(valueName + " X :");
                temp = Console.ReadLine();
            }
            Console.WriteLine(valueName + " Z :");
            temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.Z))
            {
                Console.WriteLine("Wrong format. Please enter the following parameter again.");
                Console.WriteLine(valueName + " Z :");
                temp = Console.ReadLine();
            }
            return result;
        }

        public static Surface2D ReadSurface2DFromConsole(string valueName)
        {
            Surface2D result = new Surface2D();
            Console.WriteLine(valueName + " X :");
            string temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.X))
            {
                Console.WriteLine("Wrong format. Please enter the following parameter again.");
                Console.WriteLine(valueName + " X :");
                temp = Console.ReadLine();
            }
            Console.WriteLine(valueName + " Z :");
            temp = Console.ReadLine();
            while(!double.TryParse(temp , out result.Z))
            {
                Console.WriteLine("Wrong format. Please enter the following parameter again.");
                Console.WriteLine(valueName + " Z :");
                temp = Console.ReadLine();
            }
            return result;
        }

        public static void Clear()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Pearl Calculator Core Settings Generator.");
            Console.WriteLine("Please make sure you can see all the symbols(=) in the following line");
            Console.WriteLine("=====================================================================================================================");
            Console.WriteLine("Note :");
            Console.WriteLine("Please keep in mind that,");
            Console.WriteLine("There will be precision loss for those settings generated by this application.");
            Console.WriteLine("Press any key to proceed if you acknowledge the above remark and prefer using this application.");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void SeparatingLine()
        {
            Console.WriteLine("---------------------------------------------------Separating Line---------------------------------------------------");
        }
    }
}
