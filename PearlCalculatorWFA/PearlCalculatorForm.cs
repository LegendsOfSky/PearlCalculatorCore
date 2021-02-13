using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.General;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using GeneralData = PearlCalculatorLib.General.Data;
using GeneralCalculation = PearlCalculatorLib.General.Calculation;
using ManuallyData = PearlCalculatorLib.Manually.Data;
using ManuallyCalculation = PearlCalculatorLib.Manually.Calculation;
using System.Runtime;
using System.Drawing.Printing;
using System.Runtime.Serialization;
using System.Drawing;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorWFA
{
    public partial class PearlCalculatorWFA : Form
    {
        const string DefaultImportSettingsPath = @"./settings.pcld";

        private bool IsDisplayOnTNT = false;
        private string OffsetXTextBoxString = "0.";
        private string OffsetZTextBoxString = "0.";
        private int MaxTicks = 100;
        private int ManuallyAtntAmount = 0;
        private int ManuallyBtntAmount = 0;

        public PearlCalculatorWFA()
        {
            InitializeComponent();

            ConsoleListView.Columns.Add("Type" , 90);
            ConsoleListView.Columns.Add("Message" , 260);
            BasicDirectionOutSystem.Columns.Add("Direction" , 120);
            BasicDirectionOutSystem.Columns.Add("Angle" , 230);
            GeneralSettingListView.Columns.Add("Name" , 120);
            GeneralSettingListView.Columns.Add("Value" , 240);
            Log("Main" , "Msg" , "InitializeComponent");
            DisplaySetting();
            Log("Main" , "Msg" , "Type cmd.help to check instructions");
            BasicOutputSystem.ColumnClick += BasicOutputSystem_ColumnClick;

            ImportSettingFormDefaultFile();
        }

        #region General : Event and Method

        private void BasicOutputSystem_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            if(!IsDisplayOnTNT)
                return;

            bool isSorted = true;

            switch(e.Column)
            {
                case 0:
                    Log("Main" , "Msg" , "Sort TNT result by distance from short to far");
                    GeneralData.TNTResult.SortByDistance();
                    break;
                case 1:
                    Log("Main" , "Msg" , "Sort TNT result by Ticks");
                    GeneralData.TNTResult.SortByTick();
                    break;
                case 4:
                    Log("Main" , "Msg" , "Sort TNT result by toal TNT");
                    GeneralData.TNTResult.SortByTotal();
                    break;
                default:
                    isSorted = false;
                    break;
            }
            if(isSorted)
                DisplayTNTAmount(true);
        }

        private void CalculateTNTButton_Click(object sender , EventArgs e)
        {
            Log("Main" , "Msg" , "Calculate TNT");
            if(GeneralCalculation.CalculateTNTAmount(MaxTicks , 10))
            {
                Log("Main" , "Msg" , "TNT calculated");
                DisplayTNTAmount(false);
                DisplayDirection();
            }
            else
            {
                Log("Main" , "Warn" , "============================");
                Log("Main" , "Warn" , "TNT did not calculated");
                Log("Main" , "Warn" , "Please check for your input");
                Log("Main" , "Warn" , "============================");
            }
        }

        private void PearlSimulateButton_Click(object sender , EventArgs e) => PearlSimulate();

        private void PearlSimulate()
        {
            Log("Main" , "Msg" , "Caluete pearl trace");
            DisplayPearTrace(GeneralCalculation.CalculatePearlTrace(GeneralData.RedTNT , GeneralData.BlueTNT , MaxTicks , GeneralData.Direction));
            IsDisplayOnTNT = false;
        }


        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {
            if(!IsDisplayOnTNT)
                return;
            Log("Main" , "Msg" , "Auto-implement");
            int index = BasicOutputSystem.FocusedItem.Index;
            var direction = GeneralData.Pearl.Position.Direction(GeneralData.Pearl.Position.WorldAngle(GeneralData.Destination));

            GeneralRedTNTTextBox.Text = GeneralData.TNTResult[index].Red.ToString();
            GeneralBlueTNTTextBox.Text = GeneralData.TNTResult[index].Blue.ToString();

            switch(direction)
            {
                case Direction.North:
                    GeneralNorthRadioButton.Checked = true;
                    break;
                case Direction.South:
                    GeneralSouthRadioButton.Checked = true;
                    break;
                case Direction.East:
                    GeneralEastRadioButton.Checked = true;
                    break;
                case Direction.West:
                    GeneralWestRadioButton.Checked = true;
                    break;
            }
        }

        #endregion

        #region General : Display

        private void DisplayTNTAmount(bool isRadioOverriden)
        {
            Log("Main" , "Msg" , "Reset display");
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Distance" , 120 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Ticks" , 40 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Blue" , 60 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Red" , 60 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Total TNT" , 70 , HorizontalAlignment.Left);
            IsDisplayOnTNT = true;
            if(!isRadioOverriden)
            {
                if(GeneralTNTRadioButton.Checked)
                {
                    Log("Main" , "Msg" , "Sort TNT result by weighted total TNT");
                    GeneralData.TNTResult.SortByWeightedTotal(new TNTResultSortByWeightedArgs(GeneralData.TNTWeight , GeneralData.MaxCalculateTNT , GeneralData.MaxCalculateDistance));
                }
                else if(GeneralDistanceRadioButton.Checked)
                {
                    Log("Main" , "Msg" , "Sort TNT result by distance deviation");
                    GeneralData.TNTResult.SortByDistance();
                }
                else
                {
                    Log("Main" , "Msg" , "Sort TNT result ...");
                    Log("Main" , "Msg" , "by weighted distance deviation");
                    GeneralData.TNTResult.SortByWeightedDistance(new TNTResultSortByWeightedArgs(GeneralData.TNTWeight , GeneralData.MaxCalculateTNT , GeneralData.MaxCalculateDistance));
                }
            }
            Log("Main" , "Msg" , "Start outputing TNT result");
            for(int i = 0; i < GeneralData.TNTResult.Count; i++)
            {
                if(GeneralData.TNTResult[i].Red >= 0 && GeneralData.TNTResult[i].Blue >= 0)
                {
                    ListViewItem result = new ListViewItem(GeneralData.TNTResult[i].Distance.ToString());
                    result.SubItems.Add(GeneralData.TNTResult[i].Tick.ToString());
                    result.SubItems.Add(GeneralData.TNTResult[i].Blue.ToString());
                    result.SubItems.Add(GeneralData.TNTResult[i].Red.ToString());
                    result.SubItems.Add(GeneralData.TNTResult[i].TotalTNT.ToString());
                    BasicOutputSystem.Items.Add(result);
                }
            }
            Log("Main" , "Msg" , "TNT result output finished");
        }

        private void DisplayDirection()
        {
            Log("Main" , "Msg" , "Clear direction display");
            BasicDirectionOutSystem.Items.Clear();
            Log("Main" , "Msg" , "Calculate and display direction");
            ListViewItem result = new ListViewItem(GeneralData.Pearl.Position.Direction(GeneralData.Pearl.Position.WorldAngle(GeneralData.Destination)).ToString());
            result.SubItems.Add(GeneralData.Pearl.Position.WorldAngle(GeneralData.Destination).ToString());
            BasicDirectionOutSystem.Items.Add(result);
        }

        private void DisplayPearTrace(List<Entity> pearlTrace)
        {
            Log("Main" , "Msg" , "Display pearl trace");
            Log("Main" , "Msg" , "Clear display");
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Ticks" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("X Coordinate" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Y Coordinate" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Z Coordinate" , 100 , HorizontalAlignment.Left);
            Log("Main" , "Msg" , "Start outputing pearl trace");
            for(int i = 0; i < pearlTrace.Count; i++)
            {
                ListViewItem result = new ListViewItem(i.ToString());
                result.SubItems.Add(pearlTrace[i].Position.X.ToString());
                result.SubItems.Add(pearlTrace[i].Position.Y.ToString());
                result.SubItems.Add(pearlTrace[i].Position.Z.ToString());
                BasicOutputSystem.Items.Add(result);
            }
            Log("Main" , "Msg" , "Pearl trace output finished");
        }

        private void DisplayPearlMomemtum(List<Entity> pearlMomemtum)
        {
            Log("Main" , "Msg" , "Display pearl Momemtum.");
            Log("Main" , "Msg" , "Clear display");
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Ticks" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("X Momemtum" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Y Momemtum" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Z Momemtum" , 100 , HorizontalAlignment.Left);
            Log("Main" , "Msg" , "Start outputing pearl Momemtum");
            for(int i = 0; i < pearlMomemtum.Count; i++)
            {
                ListViewItem result = new ListViewItem(i.ToString());
                result.SubItems.Add(pearlMomemtum[i].Motion.X.ToString());
                result.SubItems.Add(pearlMomemtum[i].Motion.Y.ToString());
                result.SubItems.Add(pearlMomemtum[i].Motion.Z.ToString());
                BasicOutputSystem.Items.Add(result);
            }
            Log("Main" , "Msg" , "Pearl Momemtum output finished");
        }

        #endregion

        #region General : Input

        private void PearlXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralPearlXTextBox.Text , out GeneralData.Pearl.Position.X);
        }

        private void PearlZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralPearlZTextBox.Text , out GeneralData.Pearl.Position.Z);
        }

        private void DestinationXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralDestinationXTextBox.Text , out GeneralData.Destination.X);
        }

        private void DestinationZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralDestinationZTextBox.Text , out GeneralData.Destination.Z);
        }

        private void MaxTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(GeneralMaxTNTTextBox.Text , out GeneralData.MaxTNT);
        }

        private void RedTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(GeneralRedTNTTextBox.Text , out GeneralData.RedTNT);
        }

        private void BlueTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(GeneralBlueTNTTextBox.Text , out GeneralData.BlueTNT);
        }

        private void NorthRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            GeneralData.Direction = Direction.North;
        }

        private void SouthRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            GeneralData.Direction = Direction.South;
        }

        private void EastRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            GeneralData.Direction = Direction.East;
        }

        private void WestRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            GeneralData.Direction = Direction.West;
        }

        private void TNTWeightTrackerSlider_Scroll(object sender , EventArgs e)
        {
            Log("Main" , "Msg" , "Change TNT weight value");
            GeneralData.TNTWeight = GeneralTNTWeightTrackerSlider.Value;
            Log("Main" , "Msg" , "Currently set to " + GeneralData.TNTWeight.ToString());
            Log("Main" , "Msg" , $"Currently Mode: {(IsDisplayOnTNT ? "TNT amount" : "pearl simulate")}");

            if (IsDisplayOnTNT)
                DisplayTNTAmount(false);
            else
                Log("Main" , "Msg" , "Currently mode only set weight value");
        }

        private void OffsetXTextBox_TextChanged(object sender , EventArgs e)
        {
            if(GeneralOffsetXTextBox.Text == OffsetXTextBoxString)
            {
                return;
            }
            Surface2D offset = new Surface2D();
            if(!double.TryParse(GeneralOffsetXTextBox.Text , out offset.X) || offset.X >= 1)
            {
                GeneralOffsetXTextBox.Text = OffsetXTextBoxString;
                GeneralOffsetXTextBox.Select(GeneralOffsetXTextBox.Text.Length , 0);
            }
            else
            {
                offset.Z = GeneralData.PearlOffset.Z;
                GeneralData.PearlOffset = offset;
                DisplaySetting();
                OffsetXTextBoxString = GeneralOffsetXTextBox.Text;
            }
        }

        private void OffsetZTextBox_TextChanged(object sender , EventArgs e)
        {
            if(GeneralOffsetZTextBox.Text == OffsetZTextBoxString)
            {
                return;
            }
            Surface2D offset = new Surface2D();
            if(!double.TryParse(GeneralOffsetZTextBox.Text , out offset.Z) || offset.Z >= 1)
            {
                GeneralOffsetZTextBox.Text = OffsetZTextBoxString;
                GeneralOffsetZTextBox.Select(GeneralOffsetZTextBox.Text.Length , 0);
            }
            else
            {
                offset.X = GeneralData.PearlOffset.X;
                GeneralData.PearlOffset = offset;
                DisplaySetting();
                OffsetZTextBoxString = GeneralOffsetZTextBox.Text;
            }
        }

        #endregion

        #region General : Import/Export Settings

        private void ImportSettingButton_Click(object sender , EventArgs e)
        {
            using var fileDialog = new OpenFileDialog { Filter = Settings.FileSuffix };

            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                using var fs = File.OpenRead(fileDialog.FileName);
                if(new BinaryFormatter().Deserialize(fs) is Settings settings)
                {
                    ImportSettings(settings);
                    RefleshInput();
                }
            }
        }

        private void SaveSettingButton_Click(object sender , EventArgs e)
        {
            var bf = new BinaryFormatter();
            using var fileDialog = new SaveFileDialog();
            fileDialog.Filter = Settings.FileSuffix;
            fileDialog.AddExtension = true;

            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                var fs = File.Open(fileDialog.FileName , FileMode.OpenOrCreate);
                bf.Serialize(fs , CreateSavedSettingsData());
                fs.Close();
            }
        }

        Settings CreateSavedSettingsData() => new Settings()
        {
            NorthWestTNT = GeneralData.NorthWestTNT ,
            NorthEastTNT = GeneralData.NorthEastTNT ,
            SouthWestTNT = GeneralData.SouthWestTNT ,
            SouthEastTNT = GeneralData.SouthEastTNT ,

            Pearl = GeneralData.Pearl ,

            RedTNT = GeneralData.RedTNT ,
            BlueTNT = GeneralData.BlueTNT ,
            MaxTNT = GeneralData.MaxTNT ,

            Destination = GeneralData.Destination ,
            Offset = GeneralData.PearlOffset ,

            Direction = GeneralData.Direction
        };

        void ImportSettingFormDefaultFile()
        {
            if (File.Exists(DefaultImportSettingsPath))
            {
                using var fs = File.OpenRead(DefaultImportSettingsPath);
                try
                {
                    if (new BinaryFormatter().Deserialize(fs) is Settings settings)
                    {
                        ImportSettings(settings);
                        RefleshInput();
                    }
                }
                catch (SerializationException)
                {
                    Log("Main", "Error", "At start, found settings.pcld file, but it not is calculator's settings file");
                }
            }
        }

        void ImportSettings(Settings settings)
        {
            if(settings == null)
                return;

            GeneralData.NorthWestTNT = settings.NorthWestTNT;
            GeneralData.NorthEastTNT = settings.NorthEastTNT;
            GeneralData.SouthWestTNT = settings.SouthWestTNT;
            GeneralData.SouthEastTNT = settings.SouthEastTNT;

            GeneralData.Pearl = settings.Pearl;

            GeneralData.RedTNT = settings.RedTNT;
            GeneralData.BlueTNT = settings.BlueTNT;
            GeneralData.MaxTNT = settings.MaxTNT;

            GeneralData.Destination = settings.Destination;
            GeneralData.PearlOffset = settings.Offset;

            GeneralData.Direction = settings.Direction;
        }

        void RefleshInput()
        {
            GeneralPearlXTextBox.Text = GeneralData.Pearl.Position.X.ToString();
            GeneralPearlZTextBox.Text = GeneralData.Pearl.Position.Z.ToString();

            GeneralDestinationXTextBox.Text = GeneralData.Destination.X.ToString();
            GeneralDestinationZTextBox.Text = GeneralData.Destination.Z.ToString();

            GeneralMaxTNTTextBox.Text = GeneralData.MaxTNT.ToString();

            switch(GeneralData.Direction)
            {
                case Direction.North:
                    GeneralNorthRadioButton.Checked = true;
                    break;
                case Direction.South:
                    GeneralSouthRadioButton.Checked = true;
                    break;
                case Direction.East:
                    GeneralEastRadioButton.Checked = true;
                    break;
                case Direction.West:
                    GeneralWestRadioButton.Checked = true;
                    break;
            }

            GeneralRedTNTTextBox.Text = GeneralData.RedTNT.ToString();
            GeneralBlueTNTTextBox.Text = GeneralData.BlueTNT.ToString();

            GeneralOffsetXTextBox.Text = GeneralData.PearlOffset.X.ToString();
            GeneralOffsetZTextBox.Text = GeneralData.PearlOffset.Z.ToString();

            PearlSimulate();
            GeneralPearlSimulateButton.Focus();
        }

        #endregion

        #region General : Settings
        private void DisplaySetting()
        {
            Log("Main" , "Msg" , "Clear setting display");
            GeneralSettingListView.Items.Clear();

            Log("Main" , "Msg" , "Output settings");
            ListViewItem NorthWestTNTCoorX = new ListViewItem("North West TNT(X Axis)");
            NorthWestTNTCoorX.SubItems.Add(GeneralData.NorthWestTNT.X.ToString());
            GeneralSettingListView.Items.Add(NorthWestTNTCoorX);

            ListViewItem NorthWestTNTCoorY = new ListViewItem("North West TNT(Y Axis)");
            NorthWestTNTCoorY.SubItems.Add(GeneralData.NorthWestTNT.Y.ToString());
            GeneralSettingListView.Items.Add(NorthWestTNTCoorY);

            ListViewItem NorthWestTNTCoorZ = new ListViewItem("North West TNT(Z Axis)");
            NorthWestTNTCoorZ.SubItems.Add(GeneralData.NorthWestTNT.Z.ToString());
            GeneralSettingListView.Items.Add(NorthWestTNTCoorZ);


            ListViewItem NorthEastTNTCoorX = new ListViewItem("North East TNT(X Axis)");
            NorthEastTNTCoorX.SubItems.Add(GeneralData.NorthEastTNT.X.ToString());
            GeneralSettingListView.Items.Add(NorthEastTNTCoorX);

            ListViewItem NorthEastTNTCoorY = new ListViewItem("North East TNT(Y Axis)");
            NorthEastTNTCoorY.SubItems.Add(GeneralData.NorthEastTNT.Y.ToString());
            GeneralSettingListView.Items.Add(NorthEastTNTCoorY);

            ListViewItem NorthEastTNTCoorZ = new ListViewItem("North East TNT(Z Axis)");
            NorthEastTNTCoorZ.SubItems.Add(GeneralData.NorthEastTNT.Z.ToString());
            GeneralSettingListView.Items.Add(NorthEastTNTCoorZ);


            ListViewItem SouthWestTNTCoorX = new ListViewItem("South West TNT(X Axis)");
            SouthWestTNTCoorX.SubItems.Add(GeneralData.SouthWestTNT.X.ToString());
            GeneralSettingListView.Items.Add(SouthWestTNTCoorX);

            ListViewItem SouthWestTNTCoorY = new ListViewItem("South West TNT(Y Axis)");
            SouthWestTNTCoorY.SubItems.Add(GeneralData.SouthWestTNT.Y.ToString());
            GeneralSettingListView.Items.Add(SouthWestTNTCoorY);

            ListViewItem SouthWestTNTCoorZ = new ListViewItem("South West TNT(Z Axis)");
            SouthWestTNTCoorZ.SubItems.Add(GeneralData.SouthWestTNT.Z.ToString());
            GeneralSettingListView.Items.Add(SouthWestTNTCoorZ);


            ListViewItem SouthEastTNTCoorX = new ListViewItem("South East TNT(X Axis)");
            SouthEastTNTCoorX.SubItems.Add(GeneralData.SouthEastTNT.X.ToString());
            GeneralSettingListView.Items.Add(SouthEastTNTCoorX);

            ListViewItem SouthEastTNTCoorY = new ListViewItem("South East TNT(Y Axis)");
            SouthEastTNTCoorY.SubItems.Add(GeneralData.SouthEastTNT.Y.ToString());
            GeneralSettingListView.Items.Add(SouthEastTNTCoorY);

            ListViewItem SouthEastTNTCoorZ = new ListViewItem("South East TNT(Z Axis)");
            SouthEastTNTCoorZ.SubItems.Add(GeneralData.SouthEastTNT.Z.ToString());
            GeneralSettingListView.Items.Add(SouthEastTNTCoorZ);


            ListViewItem PearlOffsetX = new ListViewItem("Pearl Offset(X Axis)");
            PearlOffsetX.SubItems.Add(GeneralData.PearlOffset.X.ToString());
            GeneralSettingListView.Items.Add(PearlOffsetX);

            ListViewItem PearlOffsetZ = new ListViewItem("Pearl Offset(Z Axis)");
            PearlOffsetZ.SubItems.Add(GeneralData.PearlOffset.Z.ToString());
            GeneralSettingListView.Items.Add(PearlOffsetZ);


            ListViewItem PearlYCoordinate = new ListViewItem("Pearl Y Coordinate");
            PearlYCoordinate.SubItems.Add(GeneralData.Pearl.Position.Y.ToString());
            GeneralSettingListView.Items.Add(PearlYCoordinate);

            ListViewItem PearlYMomentum = new ListViewItem("Pearl Y Momemtum");
            PearlYMomentum.SubItems.Add(GeneralData.Pearl.Motion.Y.ToString());
            GeneralSettingListView.Items.Add(PearlYMomentum);

            Log("Main" , "Msg" , "Settings output finished");
        }

        private void ChangeSettingButton_Click(object sender , EventArgs e)
        {
            int j;
            int k;
            if(GeneralSettingListView.FocusedItem == null)
            {
                Log("Main" , "Warn" , "============================");
                Log("Main" , "Warn" , "Settings did not change");
                Log("Main" , "Warn" , "Please check for your selection");
                Log("Main" , "Warn" , "You had to select the before changed");
                Log("Main" , "Warn" , "============================");
                return;
            }
            if(GeneralSettingListView.FocusedItem.Index < 12 && GeneralSettingListView.FocusedItem.Index >= 0)
            {
                Log("Main" , "Msg" , "Update TNT settings");
                j = GeneralSettingListView.FocusedItem.Index / 3;
                k = GeneralSettingListView.FocusedItem.Index % 3;
                Space3D setting;
                double.TryParse(GeneralSettingListView.Items[j * 3].SubItems[1].Text , out setting.X);
                double.TryParse(GeneralSettingListView.Items[j * 3 + 1].SubItems[1].Text , out setting.Y);
                double.TryParse(GeneralSettingListView.Items[j * 3 + 2].SubItems[1].Text , out setting.Z);
                switch(k)
                {
                    case 0:
                        double.TryParse(GeneralSettingInputTextBox.Text , out setting.X);
                        break;
                    case 1:
                        double.TryParse(GeneralSettingInputTextBox.Text , out setting.Y);
                        break;
                    case 2:
                        double.TryParse(GeneralSettingInputTextBox.Text , out setting.Z);
                        break;
                    default:
                        break;
                }
                switch(j)
                {
                    case 0:
                        GeneralData.NorthWestTNT = setting;
                        break;
                    case 1:
                        GeneralData.NorthEastTNT = setting;
                        break;
                    case 2:
                        GeneralData.SouthWestTNT = setting;
                        break;
                    case 3:
                        GeneralData.SouthEastTNT = setting;
                        break;
                    default:
                        break;
                }
            }
            else if(GeneralSettingListView.FocusedItem.Index == 12)
            {
                Log("Main" , "Msg" , "Update pearl offset");
                Surface2D setting = new Surface2D();
                double.TryParse(GeneralSettingInputTextBox.Text , out setting.X);
                double.TryParse(GeneralSettingListView.Items[13].SubItems[1].Text , out setting.Z);
                GeneralData.PearlOffset = setting;
            }
            else if(GeneralSettingListView.FocusedItem.Index == 13)
            {
                Log("Main" , "Msg" , "Update pearl offset");
                Surface2D setting = new Surface2D();
                double.TryParse(GeneralSettingListView.Items[12].SubItems[1].Text , out setting.X);
                double.TryParse(GeneralSettingInputTextBox.Text , out setting.Z);
                GeneralData.PearlOffset = setting;
            }
            else if(GeneralSettingListView.FocusedItem.Index == 14)
            {
                Log("Main" , "Msg" , "Update pearl Y axis position");
                double.TryParse(GeneralSettingInputTextBox.Text , out GeneralData.Pearl.Position.Y);
            }
            else if(GeneralSettingListView.FocusedItem.Index == 15)
            {
                Log("Main" , "Msg" , "Update pearl Y axis momentum");
                double.TryParse(GeneralSettingInputTextBox.Text , out GeneralData.Pearl.Motion.Y);
            }
            DisplaySetting();
        }

        private void ResetSettingButton_Click(object sender , EventArgs e)
        {
            Log("Main" , "Msg" , "Reset settings");
            GeneralData.NorthWestTNT = new Space3D(-0.884999990463257 , 170.5 , -0.884999990463257);
            GeneralData.NorthEastTNT = new Space3D(+0.884999990463257 , 170.5 , -0.884999990463257);
            GeneralData.SouthWestTNT = new Space3D(-0.884999990463257 , 170.5 , +0.884999990463257);
            GeneralData.SouthEastTNT = new Space3D(+0.884999990463257 , 170.5 , +0.884999990463257);
            GeneralData.Pearl.Position = new Space3D(0 , 170.34722638929408 , 0);
            GeneralData.Pearl.Motion= new Space3D(0 , 0.2716278719434352 , 0);
            GeneralData.PearlOffset = new Surface2D();
            DisplaySetting();
        }

        #endregion

        #region Manually : Input

        private void ManuallyPearlXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyPearlXTextBox.Text , out ManuallyData.Pearl.Position.X);
        }

        private void ManuallyMomemtumXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyMomemtumXTextBox.Text , out ManuallyData.Pearl.Motion.X);
        }

        private void ManuallyPearlYTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyPearlYTextBox.Text , out ManuallyData.Pearl.Position.Y);
        }

        private void ManuallyMomemtumYTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyMomemtumYTextBox.Text , out ManuallyData.Pearl.Motion.Y);
        }

        private void ManuallyPearlZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyPearlZTextBox.Text , out ManuallyData.Pearl.Position.Z);
        }

        private void ManuallyMomemtumZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyMomemtumZTextBox.Text , out ManuallyData.Pearl.Motion.Z);
        }

        private void ManuallyATNTXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyATNTXTextBox.Text , out ManuallyData.ATNT.X);
        }

        private void ManuallyBTNTXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyBTNTXTextBox.Text , out ManuallyData.BTNT.X);
        }

        private void ManuallyATNTYTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyATNTYTextBox.Text , out ManuallyData.ATNT.Y);
        }

        private void ManuallyBTNTYTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyBTNTYTextBox.Text , out ManuallyData.BTNT.Y);
        }

        private void ManuallyATNTZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyATNTZTextBox.Text , out ManuallyData.ATNT.Z);   
        }

        private void ManuallyBTNTZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyBTNTZTextBox.Text , out ManuallyData.BTNT.Z);
        }

        private void ManuallyATNTAmountTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(ManuallyATNTAmountTextBox.Text , out ManuallyAtntAmount);
        }

        private void ManuallyBTNTAmountTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(ManuallyBTNTAmountTextBox.Text ,  out ManuallyBtntAmount);
        }

        private void ManuallyDestinationXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyDestinationXTextBox.Text , out ManuallyData.Destination.X);
        }

        private void ManuallyDestinationZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(ManuallyDestinationZTextBox.Text , out ManuallyData.Destination.Z);
        }
        #endregion

        #region Manually : Calculation

        private void ManuallyCalculateTNTAmountButton_Click(object sender , EventArgs e)
        {
            ManuallyCalculateTNTAmount();
        }

        private void ManuallyCalculateTNTAmount()
        {
            List<TNTCalculationResult> tntResult;
            if(ManuallyCalculation.CalculateTNTAmount(ManuallyData.Destination , MaxTicks , out tntResult))
            {
                Log("Main" , "Msg" , "Reset display");
                BasicOutputSystem.Items.Clear();
                BasicOutputSystem.Columns.Clear();
                BasicOutputSystem.Columns.Add("Distance" , 120 , HorizontalAlignment.Left);
                BasicOutputSystem.Columns.Add("Ticks" , 40 , HorizontalAlignment.Left);
                BasicOutputSystem.Columns.Add("A TNT" , 60 , HorizontalAlignment.Left);
                BasicOutputSystem.Columns.Add("B TNT" , 60 , HorizontalAlignment.Left);
                BasicOutputSystem.Columns.Add("Total TNT" , 70 , HorizontalAlignment.Left);
                Log("Main" , "Msg" , "Start outputing TNT result");
                for(int i = 0; i < tntResult.Count; i++)
                {
                    if(tntResult[i].Red >= 0 && tntResult[i].Blue >= 0)
                    {
                        ListViewItem result = new ListViewItem(tntResult[i].Distance.ToString());
                        result.SubItems.Add(tntResult[i].Tick.ToString());
                        result.SubItems.Add(tntResult[i].Red.ToString());
                        result.SubItems.Add(tntResult[i].Blue.ToString());
                        result.SubItems.Add(tntResult[i].TotalTNT.ToString());
                        BasicOutputSystem.Items.Add(result);
                    }
                }
                Log("Main" , "Msg" , "TNT result output finished");
            }
        }

        private void ManuallyCalculatePearlTraceButton_Click(object sender , EventArgs e)
        {
            ManuallyCalculatePearlTrace();
        }

        private void ManuallyCalculatePearlTrace()
        {
            List<Entity> pearlTrace = ManuallyCalculation.CalculatePearl(ManuallyAtntAmount , ManuallyBtntAmount , MaxTicks);
            Log("Main" , "Msg" , "Display pearl trace");
            Log("Main" , "Msg" , "Clear display");
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Ticks" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("X Coordinate" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Y Coordinate" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Z Coordinate" , 100 , HorizontalAlignment.Left);
            Log("Main" , "Msg" , "Start outputing pearl trace");
            for(int i = 0; i < pearlTrace.Count; i++)
            {
                ListViewItem result = new ListViewItem(i.ToString());
                result.SubItems.Add(pearlTrace[i].Position.X.ToString());
                result.SubItems.Add(pearlTrace[i].Position.Y.ToString());
                result.SubItems.Add(pearlTrace[i].Position.Z.ToString());
                BasicOutputSystem.Items.Add(result);
            }
            Log("Main" , "Msg" , "Pearl trace output finished");
        }

        private void ManuallyCalculatePearlMomemtumButton_Click(object sender , EventArgs e)
        {
            ManuallyCalculatePearlMomemtum();
        }

        private void ManuallyCalculatePearlMomemtum()
        {
            List<Entity> pearlTrace = ManuallyCalculation.CalculatePearl(ManuallyAtntAmount , ManuallyBtntAmount , MaxTicks);
            Log("Main" , "Msg" , "Display pearl momemtum");
            Log("Main" , "Msg" , "Clear display");
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Ticks" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("X Motion" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Y Motion" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Z Motion" , 100 , HorizontalAlignment.Left);
            Log("Main" , "Msg" , "Start outputing pearl trace");
            for(int i = 0; i < pearlTrace.Count; i++)
            {
                ListViewItem result = new ListViewItem(i.ToString());
                result.SubItems.Add(pearlTrace[i].Motion.X.ToString());
                result.SubItems.Add(pearlTrace[i].Motion.Y.ToString());
                result.SubItems.Add(pearlTrace[i].Motion.Z.ToString());
                BasicOutputSystem.Items.Add(result);
            }
            Log("Main" , "Msg" , "Pearl momemtum output finished");
        }
        #endregion

        #region Console
        private void Log(string thread , string type , string message)
        {
            Log(thread, type, message, Color.Black);
        }

        private void Log(string thread , string type , string message , Color messageColor)
        {
            ListViewItem log = new ListViewItem("[" + thread + "/" + type + "]");
            log.UseItemStyleForSubItems = false;

            log.SubItems[0].ForeColor = Color.Gray;
            log.SubItems.Add(message).ForeColor = messageColor;

            ConsoleListView.Items.Add(log);
        }

        private void ConsoleEnterButton_Click(object sender , EventArgs e)
        {
            Space3D space3D;
            string cmd = "";
            string parameter1 = "";
            string parameter2 = "";
            string parameter3 = "";
            bool isParameter1Correct, isParameter2Correct , isParameter3Correct;
            int j = 0;
            for(int i = 0; i < ConsoleInputTextBox.TextLength; i++)
            {
                if(ConsoleInputTextBox.Text[i] == ' ')
                {
                    j = i + 1;
                    break;
                }
                cmd += ConsoleInputTextBox.Text[i];
            }
            for(int i = j; i < ConsoleInputTextBox.TextLength; i++)
            {
                if(ConsoleInputTextBox.Text[i] == ' ')
                {
                    j = i + 1;
                    break;
                }
                parameter1 += ConsoleInputTextBox.Text[i];
            }
            for(int i = j; i < ConsoleInputTextBox.TextLength; i++)
            {
                if(ConsoleInputTextBox.Text[i] == ' ')
                {
                    j = i + 1;
                    break;
                }
                parameter2 += ConsoleInputTextBox.Text[i];
            }
            for(int i = j; i < ConsoleInputTextBox.TextLength; i++)
            {
                if(ConsoleInputTextBox.Text[i] == ' ')
                {
                    j = i + 1;
                    break;
                }
                parameter3 += ConsoleInputTextBox.Text[i];
            }
            switch(cmd)
            {
                case "cmd.help":
                    Log("CMD" , "Msg" , "==========================" , Color.Red);
                    Log("CMD" , "Msg" , "cmd.help");
                    Log("CMD" , "Msg" , "Show all cmd command and help");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.all.change.maxticks");
                    Log("CMD" , "Msg" , "Parameter : <interger>");
                    Log("CMD" , "Msg" , "Changes max ticks");
                    Log("CMD" , "Msg" , "Note : value should be more than 0");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.tntweight");
                    Log("CMD" , "Msg" , "Parameter : <interger>");
                    Log("CMD" , "Msg" , "Changes TNT weight");
                    Log("CMD" , "Msg" , "Note : Value should between 0 ~ 100");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general,change.tnt.red");
                    Log("CMD" , "Msg" , "Parameter : <interger>");
                    Log("CMD" , "Msg" , "Changes Red TNT amount");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general,change.tnt.blue");
                    Log("CMD" , "Msg" , "Parameter : <interger>");
                    Log("CMD" , "Msg" , "Changes Blue TNT amount");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.pearl.position.x");
                    Log("CMD" , "Msg" , "Parameter : <floating point number>");
                    Log("CMD" , "Msg" , "Change the value of Pearl X axix position");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.pearl.position.y");
                    Log("CMD" , "Msg" , "Parameter : <floating point number>");
                    Log("CMD" , "Msg" , "Change the value of Pearl Y axix position");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.pearl.position.z");
                    Log("CMD" , "Msg" , "Parameter : <floating point number>");
                    Log("CMD" , "Msg" , "Change the value of Pearl Z axix position");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.pearl.position.2D");
                    Log("CMD" , "Msg" , "Parameter : <X> <Z>");
                    Log("CMD" , "Msg" , "All Parameters are in floating point number");
                    Log("CMD" , "Msg" , "Change the X and Z position of the pearl");
                    Log("CMD" , "Msg" , "Note : Please input the coordinate correctly");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.pearl.position.3D");
                    Log("CMD" , "Msg" , "Parameter : <X> <Y> <Z>");
                    Log("CMD" , "Msg" , "All Parameters are in floating point number");
                    Log("CMD" , "Msg" , "Change the X , Y and Z position of the pearl");
                    Log("CMD" , "Msg" , "Note : Please input the coordinate correctly");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.change.destination");
                    Log("CMD" , "Msg" , "Parameter : <X> <Z> (Both in floating point number)");
                    Log("CMD" , "Msg" , "Change the X and Z position of your destination");
                    Log("CMD" , "Msg" , "Note : Please input the coordinate correctly");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.calculate.tnt");
                    Log("CMD" , "Msg" , "Calculate the suitable TNT setup");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.calculate.pearl,trace");
                    Log("CMD" , "Msg" , "Calculate the trace of pearl");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.general.calculate.pearl.momemtum");
                    Log("CMD" , "Msg" , "Calculate the momemtum of pearl");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.manually.calculate.tnt");
                    Log("CMD" , "Msg" , "Calculate the suitable TNT setup");
                    Log("CMD" , "Warn" , "Make sure for the TNT Coordinate");
                    Log("CMD" , "Warn" , "It might crash if you enter it incorrectly");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.manually.calculate.pearl.trace");
                    Log("CMD" , "Msg" , "Calculate the trace of pearl");
                    Log("CMD" , "Msg" , "----------------------------" , Color.Gray);
                    Log("CMD" , "Msg" , "cmd.manually.calculate.pearl.momemtum");
                    Log("CMD" , "Msg" , "Calculate the momemtum of pearl");
                    Log("CMD" , "Msg" , "==========================" , Color.Red);
                    break;
                case "cmd.all.change.maxticks":
                    if(int.TryParse(parameter1 , out MaxTicks))
                    {
                        Log("CMD" , "Msg" , "Max ticks changed");
                        Log("CMD" , "Msg" , "Currentlt set to " + MaxTicks.ToString());
                    }
                    else
                    {
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                        Log("CMD" , "Warn" , "Unknow parameter" , Color.Red);
                        Log("CMD" , "Warn" , "Please check for your input" , Color.Red);
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                    }
                    break;
                case "cmd.general.change.tntweight":
                    Log("CMD" , "Msg" , "Changing TNT weight value");
                    GeneralData.TNTWeight = GeneralTNTWeightTrackerSlider.Value;
                    Log("CMD" , "Msg" , "Currently set to " + GeneralData.TNTWeight.ToString());
                    DisplayTNTAmount(false);
                    break;
                case "cmd.general.change.pearl.position.x":
                    Log("CMD" , "Msg" , "Changing Pearl X coordinate");
                    if(double.TryParse(parameter1 , out GeneralData.Pearl.Position.X))
                    {
                        GeneralPearlXTextBox.Text = parameter1;
                        Log("CMD" , "Msg" , "Pearl X coordinate changed");
                        Log("CMD" , "Msg" , "Currently set to " + GeneralData.Pearl.Position.X);
                    }
                    else
                    {
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                        Log("CMD" , "Warn" , "Pearl X does not change" , Color.Red);
                        Log("CMD" , "Warn" , "Check for your parameter" , Color.Red);
                        Log("CMD" , "Warn" , "It should be a floating point number" , Color.Red);
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                    }
                    break;
                case "cmd.general.change.pearl.position.y":
                    Log("CMD" , "Msg" , "Changing Pearl Y coordinate");
                    if(double.TryParse(parameter1 , out GeneralData.Pearl.Position.Y))
                    {
                        DisplaySetting();
                        Log("CMD" , "Msg" , "Pearl Y height changed");
                        Log("CMD" , "Msg" , "Currently set to " + GeneralData.Pearl.Position.Y);   
                    }
                    else
                    {
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                        Log("CMD" , "Warn" , "Pearl Y does not change" , Color.Red);
                        Log("CMD" , "Warn" , "Check for your parameter" , Color.Red);
                        Log("CMD" , "Warn" , "It should be a floating point number" , Color.Red);
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                    }
                    break;
                case "cmd.general.change.pearl.position.z":
                    Log("CMD" , "Msg" , "Changing Pearl Z coordinate");
                    if(double.TryParse(parameter1 , out GeneralData.Pearl.Position.Z))
                    {
                        GeneralPearlZTextBox.Text = parameter1;
                        Log("CMD" , "Msg" , "Pearl Z coordinate changed");
                        Log("CMD" , "Msg" , "Currently set to " + GeneralData.Pearl.Position.Z);
                    }
                    else
                    {
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                        Log("CMD" , "Warn" , "Pearl Z does not change" , Color.Red);
                        Log("CMD" , "Warn" , "Check for your parameter" , Color.Red);
                        Log("CMD" , "Warn" , "It should be a floating point number" , Color.Red);
                        Log("CMD" , "Warn" , "==========================" , Color.Red);
                    }
                    break;
                case "cmd.general.change.pearl.position.2D":
                    Log("CMD" , "Msg" , "Changing Pearl Position");
                    isParameter1Correct = double.TryParse(parameter1 , out space3D.X);
                    isParameter2Correct = double.TryParse(parameter2 , out space3D.Z);
                    if(isParameter1Correct && isParameter2Correct)
                    {
                        GeneralData.Pearl.Position.X = space3D.X;
                        GeneralData.Pearl.Position.Z = space3D.Z;
                        GeneralPearlXTextBox.Text = space3D.X.ToString();
                        GeneralPearlZTextBox.Text = space3D.Z.ToString();
                        Log("CMD" , "Msg" , "Pearl position changed");
                        Log("CMD" , "Msg" , "Currecntly set to");
                        Log("CMD" , "Msg" , "X Axis : " + GeneralData.Pearl.Position.X);
                        Log("CMD" , "Msg" , "Z Axis : " + GeneralData.Pearl.Position.Z);
                    }
                    else
                        DoubleParameterUncorrectLog(isParameter1Correct , isParameter2Correct);
                    break;
                case "cmd.general.change.pearl.position.3D":
                    Log("CMD" , "Msg" , "Changing Pearl Position");
                    isParameter1Correct = double.TryParse(parameter1 , out space3D.X);
                    isParameter2Correct = double.TryParse(parameter2 , out space3D.Y);
                    isParameter3Correct = double.TryParse(parameter3 , out space3D.Z);
                    if(isParameter1Correct && isParameter2Correct)
                    {
                        GeneralData.Pearl.Position = space3D;
                        GeneralPearlXTextBox.Text = space3D.X.ToString();
                        GeneralPearlZTextBox.Text = space3D.Z.ToString();
                        DisplaySetting();
                        Log("CMD" , "Msg" , "Pearl position changed");
                        Log("CMD" , "Msg" , "Currecntly set to");
                        Log("CMD" , "Msg" , "X Axis : " + GeneralData.Pearl.Position.X);
                        Log("CMD" , "Msg" , "Y Axis : " + GeneralData.Pearl.Position.Y);
                        Log("CMD" , "Msg" , "Z Axis : " + GeneralData.Pearl.Position.Z);
                    }
                    else
                        TribleParameterUncorectLog(isParameter1Correct , isParameter2Correct , isParameter3Correct);
                    break;
                case "cmd.general,change.tnt.red":
                    Log("CMD" , "Msg" , "Changing Red TNT amount");
                    if(int.TryParse(parameter1 , out GeneralData.RedTNT))
                    {
                        Log("CMD" , "Msg" , "Red TNT changed");
                        Log("CMD" , "Msg" , "Currentlt set to " + GeneralData.RedTNT);
                    }
                    break;
                case "cmd.general,change.tnt.blue":
                    Log("CMD" , "Msg" , "Changing Blue TNT amount");
                    if(int.TryParse(parameter1 , out GeneralData.BlueTNT))
                    {
                        Log("CMD" , "Msg" , "Blue TNT changed");
                        Log("CMD" , "Msg" , "Currentlt set to " + GeneralData.BlueTNT);
                    }
                    break;
                case "cmd.general.change.destination":
                    Log("CMD" , "Warn" , "Changing Destination" , Color.Red);
                    isParameter1Correct = double.TryParse(parameter1 , out space3D.X);
                    isParameter2Correct = double.TryParse(parameter2 , out space3D.Z);
                    if(isParameter1Correct && isParameter2Correct)
                    {
                        GeneralData.Destination.X = space3D.X;
                        GeneralData.Destination.Z = space3D.Z;
                        GeneralDestinationXTextBox.Text = space3D.X.ToString();
                        GeneralDestinationZTextBox.Text = space3D.Z.ToString();
                        Log("CMD" , "Msg" , "Destination changed");
                        Log("CMD" , "Msg" , "Currecntly set to");
                        Log("CMD" , "Msg" , GeneralData.Destination.X + " , " + GeneralData.Destination.Z);
                    }
                    else
                        DoubleParameterUncorrectLog(isParameter1Correct , isParameter2Correct);
                    break;
                case "cmd.general.calculate.tnt":
                    Log("Main" , "Msg" , "Calculate TNT");
                    if(GeneralCalculation.CalculateTNTAmount(MaxTicks , 10))
                    {
                        Log("Main" , "Msg" , "TNT calculated");
                        DisplayTNTAmount(false);
                        DisplayDirection();
                    }
                    else
                    {
                        Log("Main" , "Warn" , "==========================" , Color.Red);
                        Log("Main" , "Warn" , "TNT did not calculated" , Color.Red);
                        Log("Main" , "Warn" , "Please check for your input" , Color.Red);
                        Log("Main" , "Warn" , "==========================" , Color.Red);
                    }
                    break;
                case "cmd.general.calculate.pearl.trace":
                    Log("Main" , "Msg" , "Caluting pearl trace");
                    DisplayPearTrace(GeneralCalculation.CalculatePearlTrace(GeneralData.RedTNT , GeneralData.BlueTNT , MaxTicks , GeneralData.Direction));
                    IsDisplayOnTNT = false;
                    break;
                case "cmd.general.calculate.pearl.momemtum":
                    Log("Main" , "Msg" , "Calculating pearl momemtum");
                    DisplayPearlMomemtum(GeneralCalculation.CalculatePearlTrace(GeneralData.RedTNT , GeneralData.BlueTNT , MaxTicks , GeneralData.Direction));
                    IsDisplayOnTNT = false;
                    break;
                case "cmd.manually.calculate.tnt":
                    Log("CMD" , "Msg" , "Calculating TNT");
                    ManuallyCalculateTNTAmount();
                    break;
                case "cmd.manually.cauculate.pearl.trace":
                    Log("CMD" , "Msg" , "Calculating pearl trace");
                    ManuallyCalculatePearlTrace();
                    break;
                case "cmd.manually.calculate.pearl.momemtum":
                    Log("CMD" , "Msg" , "Calculating pearl momemtum");
                    ManuallyCalculatePearlMomemtum();
                    break;
                default:
                    Log("CMD" , "Warn" , "==========================" , Color.Red);
                    Log("CMD" , "Warn" , "Unknow instruction" , Color.Red);
                    Log("CMD" , "Warn" , "Please check for your input" , Color.Red);
                    Log("CMD" , "Warn" , "==========================" , Color.Red);
                    break;
            }
        }

        private void DoubleParameterUncorrectLog(bool isParameter1Correct , bool isParameter2Correct)
        {
            Log("CMD" , "Warn" , "==========================" , Color.Red);
            if(!isParameter1Correct && !isParameter1Correct)
                Log("CMD" , "Warn" , "Both parameter is uncorrect" , Color.Red);
            else if(!isParameter1Correct)
                Log("CMD" , "Warn" , "Parameter 1 is uncorrect" , Color.Red);
            else
                Log("CMD" , "Warn" , "Parameter 2 is uncorrect" , Color.Red);
            Log("CMD" , "Warn" , "Please check it again" , Color.Red);
            Log("CMD" , "Warn" , "==========================" , Color.Red);
        }

        private void TribleParameterUncorectLog(bool isParameter1Correct , bool isParameter2Correct , bool isParameter3Correct)
        {
            Log("CMD" , "Warn" , "==========================" , Color.Red);
            if(!isParameter1Correct && !isParameter2Correct && !isParameter3Correct)
                Log("CMD" , "Warn" , "All Parameter is uncorrect" , Color.Red);
            else if(!isParameter1Correct && !isParameter2Correct)
                Log("CMD" , "Warn" , "Parameter 1 & 2 is uncorrect" , Color.Red);
            else if(!isParameter2Correct && !isParameter3Correct)
                Log("CMD" , "Warn" , "Parameter 2 & 3 is uncorrect" , Color.Red);
            else if(!isParameter3Correct && !isParameter1Correct)
                Log("CMD" , "Warn" , "Parameter 1 & 3 is uncorrect" , Color.Red);
            else if(!isParameter1Correct)
                Log("CMD" , "Warn" , "Parameter 1 is uncorrect" , Color.Red);
            else if(!isParameter2Correct)
                Log("CMD" , "Warn" , "Parameter 2 is uncorrect" , Color.Red);
            else
                Log("CMD" , "Warn" , "Paramater 3 is uncorrect" , Color.Red);
            Log("CMD" , "Warn" , "==========================" , Color.Red);
        }
        #endregion
    }
}
