using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.General;
using PearlCalculatorLib.General.Result;
using PearlCalculatorLib.PearlCalculationLib;

namespace PearlCalculatorWFA
{
    public partial class PearlCalculatorWFA : Form
    {
        private bool IsDisplayOnTNT = false;
        private string OffsetXTextBoxString = "0.";
        private string OffsetZTextBoxString = "0.";
        private int MaxTicks = 100;

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
                    Data.TNTResult.SortByDistance();
                    break;
                case 1:
                    Log("Main" , "Msg" , "Sort TNT result by Ticks");
                    Data.TNTResult.SortByTick();
                    break;
                case 4:
                    Log("Main" , "Msg" , "Sort TNT result by toal TNT");
                    Data.TNTResult.SortByTotal();
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
            if(Calculation.CalculateTNTAmount(MaxTicks))
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
            DisplayPearTrace(Calculation.CalculatePearlTrace(Data.RedTNT , Data.BlueTNT , MaxTicks , Data.Direction));
            IsDisplayOnTNT = false;
        }


        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {
            if(!IsDisplayOnTNT)
                return;
            Log("Main" , "Msg" , "Auto-implement");
            int index = BasicOutputSystem.FocusedItem.Index;
            var direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination));

            GeneralRedTNTTextBox.Text = Data.TNTResult[index].Red.ToString();
            GeneralBlueTNTTextBox.Text = Data.TNTResult[index].Blue.ToString();

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
                    Data.TNTResult.SortByWeightedTotal();
                }
                else if(GeneralDistanceRadioButton.Checked)
                {
                    Log("Main" , "Msg" , "Sort TNT result by distance deviation");
                    Data.TNTResult.SortByDistance();
                }
                else
                {
                    Log("Main" , "Msg" , "Sort TNT result ...");
                    Log("Main" , "Msg" , "by weighted distance deviation");
                    Data.TNTResult.SortByWeightedDistance();
                }
            }
            Log("Main" , "Msg" , "Start outputing TNT result");
            for(int i = 0; i < Data.TNTResult.Count; i++)
            {
                if(Data.TNTResult[i].Red >= 0 && Data.TNTResult[i].Blue >= 0)
                {
                    ListViewItem result = new ListViewItem(Data.TNTResult[i].Distance.ToString());
                    result.SubItems.Add(Data.TNTResult[i].Tick.ToString());
                    result.SubItems.Add(Data.TNTResult[i].Blue.ToString());
                    result.SubItems.Add(Data.TNTResult[i].Red.ToString());
                    result.SubItems.Add(Data.TNTResult[i].TotalTNT.ToString());
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
            ListViewItem result = new ListViewItem(Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination)).ToString());
            result.SubItems.Add(Data.Pearl.Position.WorldAngle(Data.Destination).ToString());
            BasicDirectionOutSystem.Items.Add(result);
        }

        private void DisplayPearTrace(List<Pearl> pearlTrace)
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

        private void DisplayPearlMomemtum(List<Pearl> pearlMomemtum)
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
                result.SubItems.Add(pearlMomemtum[i].Vector.X.ToString());
                result.SubItems.Add(pearlMomemtum[i].Vector.Y.ToString());
                result.SubItems.Add(pearlMomemtum[i].Vector.Z.ToString());
                BasicOutputSystem.Items.Add(result);
            }
            Log("Main" , "Msg" , "Pearl Momemtum output finished");
        }

        #endregion

        #region General : Input

        private void PearlXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralPearlXTextBox.Text , out Data.Pearl.Position.X);
        }

        private void PearlZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralPearlZTextBox.Text , out Data.Pearl.Position.Z);
        }

        private void DestinationXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralDestinationXTextBox.Text , out Data.Destination.X);
        }

        private void DestinationZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(GeneralDestinationZTextBox.Text , out Data.Destination.Z);
        }

        private void MaxTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(GeneralMaxTNTTextBox.Text , out Data.MaxTNT);
        }

        private void RedTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(GeneralRedTNTTextBox.Text , out Data.RedTNT);
        }

        private void BlueTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(GeneralBlueTNTTextBox.Text , out Data.BlueTNT);
        }

        private void NorthRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = Direction.North;
        }

        private void SouthRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = Direction.South;
        }

        private void EastRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = Direction.East;
        }

        private void WestRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = Direction.West;
        }

        private void TNTWeightTrackerSlider_Scroll(object sender , EventArgs e)
        {
            Log("Main" , "Msg" , "Change TNT weight value");
            Data.TNTWeight = GeneralTNTWeightTrackerSlider.Value;
            Log("Main" , "Msg" , "Currently set to " + Data.TNTWeight.ToString());
            DisplayTNTAmount(false);
        }

        private void OffsetXTextBox_TextChanged(object sender , EventArgs e)
        {
            if(GeneralOffsetXTextBox.Text == OffsetXTextBoxString)
            {
                return;
            }
            Space3D offset = new Space3D();
            if(!double.TryParse(GeneralOffsetXTextBox.Text , out offset.X) || offset.X >= 1)
            {
                GeneralOffsetXTextBox.Text = OffsetXTextBoxString;
                GeneralOffsetXTextBox.Select(GeneralOffsetXTextBox.Text.Length , 0);
            }
            else
            {
                offset.Z = Data.PearlOffset.Z;
                Data.PearlOffset = offset;
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
            Space3D offset = new Space3D();
            if(!double.TryParse(GeneralOffsetZTextBox.Text , out offset.Z) || offset.Z >= 1)
            {
                GeneralOffsetZTextBox.Text = OffsetZTextBoxString;
                GeneralOffsetZTextBox.Select(GeneralOffsetZTextBox.Text.Length , 0);
            }
            else
            {
                offset.X = Data.PearlOffset.X;
                Data.PearlOffset = offset;
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
            NorthWestTNT = Data.NorthWestTNT ,
            NorthEastTNT = Data.NorthEastTNT ,
            SouthWestTNT = Data.SouthWestTNT ,
            SouthEastTNT = Data.SouthEastTNT ,

            Pearl = Data.Pearl ,

            RedTNT = Data.RedTNT ,
            BlueTNT = Data.BlueTNT ,
            MaxTNT = Data.MaxTNT ,

            Destination = Data.Destination ,
            Offset = Data.PearlOffset ,

            Direction = Data.Direction
        };

        void ImportSettings(Settings settings)
        {
            if(settings == null)
                return;

            Data.NorthWestTNT = settings.NorthWestTNT;
            Data.NorthEastTNT = settings.NorthEastTNT;
            Data.SouthWestTNT = settings.SouthWestTNT;
            Data.SouthEastTNT = settings.SouthEastTNT;

            Data.Pearl = settings.Pearl;

            Data.RedTNT = settings.RedTNT;
            Data.BlueTNT = settings.BlueTNT;
            Data.MaxTNT = settings.MaxTNT;

            Data.Destination = settings.Destination;
            Data.PearlOffset = settings.Offset;

            Data.Direction = settings.Direction;
        }

        void RefleshInput()
        {
            GeneralPearlXTextBox.Text = Data.Pearl.Position.X.ToString();
            GeneralPearlZTextBox.Text = Data.Pearl.Position.Z.ToString();

            GeneralDestinationXTextBox.Text = Data.Destination.X.ToString();
            GeneralDestinationZTextBox.Text = Data.Destination.Z.ToString();

            GeneralMaxTNTTextBox.Text = Data.MaxTNT.ToString();

            switch(Data.Direction)
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

            GeneralRedTNTTextBox.Text = Data.RedTNT.ToString();
            GeneralBlueTNTTextBox.Text = Data.BlueTNT.ToString();

            GeneralOffsetXTextBox.Text = Data.PearlOffset.X.ToString();
            GeneralOffsetZTextBox.Text = Data.PearlOffset.Z.ToString();

            PearlSimulate();
        }

        #endregion

        #region General : Settings
        private void DisplaySetting()
        {
            Log("Main" , "Msg" , "Clear setting display");
            GeneralSettingListView.Items.Clear();

            Log("Main" , "Msg" , "Output settings");
            ListViewItem NorthWestTNTCoorX = new ListViewItem("North West TNT(X Axis)");
            NorthWestTNTCoorX.SubItems.Add(Data.NorthWestTNT.X.ToString());
            GeneralSettingListView.Items.Add(NorthWestTNTCoorX);

            ListViewItem NorthWestTNTCoorY = new ListViewItem("North West TNT(Y Axis)");
            NorthWestTNTCoorY.SubItems.Add(Data.NorthWestTNT.Y.ToString());
            GeneralSettingListView.Items.Add(NorthWestTNTCoorY);

            ListViewItem NorthWestTNTCoorZ = new ListViewItem("North West TNT(Z Axis)");
            NorthWestTNTCoorZ.SubItems.Add(Data.NorthWestTNT.Z.ToString());
            GeneralSettingListView.Items.Add(NorthWestTNTCoorZ);


            ListViewItem NorthEastTNTCoorX = new ListViewItem("North East TNT(X Axis)");
            NorthEastTNTCoorX.SubItems.Add(Data.NorthEastTNT.X.ToString());
            GeneralSettingListView.Items.Add(NorthEastTNTCoorX);

            ListViewItem NorthEastTNTCoorY = new ListViewItem("North East TNT(Y Axis)");
            NorthEastTNTCoorY.SubItems.Add(Data.NorthEastTNT.Y.ToString());
            GeneralSettingListView.Items.Add(NorthEastTNTCoorY);

            ListViewItem NorthEastTNTCoorZ = new ListViewItem("North East TNT(Z Axis)");
            NorthEastTNTCoorZ.SubItems.Add(Data.NorthEastTNT.Z.ToString());
            GeneralSettingListView.Items.Add(NorthEastTNTCoorZ);


            ListViewItem SouthWestTNTCoorX = new ListViewItem("South West TNT(X Axis)");
            SouthWestTNTCoorX.SubItems.Add(Data.SouthWestTNT.X.ToString());
            GeneralSettingListView.Items.Add(SouthWestTNTCoorX);

            ListViewItem SouthWestTNTCoorY = new ListViewItem("South West TNT(Y Axis)");
            SouthWestTNTCoorY.SubItems.Add(Data.SouthWestTNT.Y.ToString());
            GeneralSettingListView.Items.Add(SouthWestTNTCoorY);

            ListViewItem SouthWestTNTCoorZ = new ListViewItem("South West TNT(Z Axis)");
            SouthWestTNTCoorZ.SubItems.Add(Data.SouthWestTNT.Z.ToString());
            GeneralSettingListView.Items.Add(SouthWestTNTCoorZ);


            ListViewItem SouthEastTNTCoorX = new ListViewItem("South East TNT(X Axis)");
            SouthEastTNTCoorX.SubItems.Add(Data.SouthEastTNT.X.ToString());
            GeneralSettingListView.Items.Add(SouthEastTNTCoorX);

            ListViewItem SouthEastTNTCoorY = new ListViewItem("South East TNT(Y Axis)");
            SouthEastTNTCoorY.SubItems.Add(Data.SouthEastTNT.Y.ToString());
            GeneralSettingListView.Items.Add(SouthEastTNTCoorY);

            ListViewItem SouthEastTNTCoorZ = new ListViewItem("South East TNT(Z Axis)");
            SouthEastTNTCoorZ.SubItems.Add(Data.SouthEastTNT.Z.ToString());
            GeneralSettingListView.Items.Add(SouthEastTNTCoorZ);


            ListViewItem PearlOffsetX = new ListViewItem("Pearl Offset(X Axis)");
            PearlOffsetX.SubItems.Add(Data.PearlOffset.X.ToString());
            GeneralSettingListView.Items.Add(PearlOffsetX);

            ListViewItem PearlOffsetZ = new ListViewItem("Pearl Offset(Z Axis)");
            PearlOffsetZ.SubItems.Add(Data.PearlOffset.Z.ToString());
            GeneralSettingListView.Items.Add(PearlOffsetZ);


            ListViewItem PearlYCoordinate = new ListViewItem("Pearl Y Coordinate");
            PearlYCoordinate.SubItems.Add(Data.Pearl.Position.Y.ToString());
            GeneralSettingListView.Items.Add(PearlYCoordinate);

            ListViewItem PearlYMomentum = new ListViewItem("Pearl Y Momemtum");
            PearlYMomentum.SubItems.Add(Data.Pearl.Vector.Y.ToString());
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
                        Data.NorthWestTNT = setting;
                        break;
                    case 1:
                        Data.NorthEastTNT = setting;
                        break;
                    case 2:
                        Data.SouthWestTNT = setting;
                        break;
                    case 3:
                        Data.SouthEastTNT = setting;
                        break;
                    default:
                        break;
                }
            }
            else if(GeneralSettingListView.FocusedItem.Index == 12)
            {
                Log("Main" , "Msg" , "Update pearl offset");
                Space3D setting = new Space3D();
                double.TryParse(GeneralSettingInputTextBox.Text , out setting.X);
                double.TryParse(GeneralSettingListView.Items[13].SubItems[1].Text , out setting.Z);
                Data.PearlOffset = setting;
            }
            else if(GeneralSettingListView.FocusedItem.Index == 13)
            {
                Log("Main" , "Msg" , "Update pearl offset");
                Space3D setting = new Space3D();
                double.TryParse(GeneralSettingListView.Items[12].SubItems[1].Text , out setting.X);
                double.TryParse(GeneralSettingInputTextBox.Text , out setting.Z);
                Data.PearlOffset = setting;
            }
            else if(GeneralSettingListView.FocusedItem.Index == 14)
            {
                Log("Main" , "Msg" , "Update pearl Y axis position");
                double.TryParse(GeneralSettingInputTextBox.Text , out Data.Pearl.Position.Y);
            }
            else if(GeneralSettingListView.FocusedItem.Index == 15)
            {
                Log("Main" , "Msg" , "Update pearl Y axis momentum");
                double.TryParse(GeneralSettingInputTextBox.Text , out Data.Pearl.Vector.Y);
            }
            DisplaySetting();
        }

        private void ResetSettingButton_Click(object sender , EventArgs e)
        {
            Log("Main" , "Msg" , "Reset settings");
            Data.NorthWestTNT = new Space3D(-0.884999990463257 , 170.5 , -0.884999990463257);
            Data.NorthEastTNT = new Space3D(+0.884999990463257 , 170.5 , -0.884999990463257);
            Data.SouthWestTNT = new Space3D(-0.884999990463257 , 170.5 , +0.884999990463257);
            Data.SouthEastTNT = new Space3D(+0.884999990463257 , 170.5 , +0.884999990463257);
            Data.Pearl.Position = new Space3D(0 , 170.34722638929408 , 0);
            Data.Pearl.Vector = new Space3D(0 , 0.2716278719434352 , 0);
            Data.PearlOffset = new Space3D(0 , 0 , 0);
            DisplaySetting();
        }

        #endregion

        #region Console
        private void Log(string thread , string type , string message)
        {
            ListViewItem log = new ListViewItem("[" + thread + "/" + type + "]");
            log.SubItems.Add(message);
            ConsoleListView.Items.Add(log);
        }

        private void ConsoleEnterButton_Click(object sender , EventArgs e)
        {
            string cmd = "";
            string parameter1 = "";
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
            switch(cmd)
            {
                case "cmd.help":
                    Log("CMD" , "Msg" , "==========================");
                    Log("CMD" , "Msg" , "cmd.general.change.maxticks <Intherger>");
                    Log("CMD" , "Msg" , "Changes the max ticks");
                    Log("CMD" , "Msg" , "----------------------------");
                    Log("CMD" , "Msg" , "cmd.general.change.tntweight <interger>");
                    Log("CMD" , "Msg" , "Changes the TNT weight");
                    Log("CMD" , "Msg" , "----------------------------");
                    Log("CMD" , "Msg" , "cmd.general.calculate.tnt");
                    Log("CMD" , "Msg" , "Calculate the suitable TNT setup");
                    Log("CMD" , "Msg" , "----------------------------");
                    Log("CMD" , "Msg" , "cmd.general.calculate.pearl,trace");
                    Log("CMD" , "Msg" , "Calculate the trace of pearl");
                    Log("CMD" , "Msg" , "----------------------------");
                    Log("CMD" , "Msg" , "cmd.general.calculate.pearl.momemtum");
                    Log("CMD" , "Msg" , "Calculate the momemtum of pearl");
                    Log("CMD" , "Msg" , "==========================");
                    break;
                case "cmd.general.change.maxticks":
                    if(int.TryParse(parameter1 , out MaxTicks))
                    {
                        Log("CMD" , "Msg" , "Max ticks changed");
                        Log("CMD" , "Msg" , "Currentlt set to " + MaxTicks.ToString());
                    }
                    else
                    {
                        Log("CMD" , "Warn" , "==========================");
                        Log("CMD" , "Warn" , "Unknow parameter");
                        Log("CMD" , "Warn" , "Please check for your input");
                        Log("CMD" , "Warn" , "==========================");
                    }
                    break;
                case "cmd.general.change.tntweight":
                    Log("CMD" , "Msg" , "Change TNT weight value");
                    Data.TNTWeight = GeneralTNTWeightTrackerSlider.Value;
                    Log("CMD" , "Msg" , "Currently set to " + Data.TNTWeight.ToString());
                    DisplayTNTAmount(false);
                    break;
                case "cmd.general.calculate.tnt":
                    Log("Main" , "Msg" , "Calculate TNT");
                    if(Calculation.CalculateTNTAmount(MaxTicks))
                    {
                        Log("Main" , "Msg" , "TNT calculated");
                        DisplayTNTAmount(false);
                        DisplayDirection();
                    }
                    else
                    {
                        Log("Main" , "Warn" , "==========================");
                        Log("Main" , "Warn" , "TNT did not calculated");
                        Log("Main" , "Warn" , "Please check for your input");
                        Log("Main" , "Warn" , "==========================");
                    }
                    break;
                case "cmd.general.calculate.pearl.trace":
                    Log("Main" , "Msg" , "Caluete pearl trace");
                    DisplayPearTrace(Calculation.CalculatePearlTrace(Data.RedTNT , Data.BlueTNT , MaxTicks , Data.Direction));
                    IsDisplayOnTNT = false;
                    break;
                case "cmd.general.calculate.pearl.momemtum":
                    Log("Main" , "Msg" , "Caluete pearl momemtum");
                    DisplayPearlMomemtum(Calculation.CalculatePearlTrace(Data.RedTNT , Data.BlueTNT , MaxTicks , Data.Direction));
                    IsDisplayOnTNT = false;
                    break;
                default:
                    Log("CMD" , "Warn" , "==========================");
                    Log("CMD" , "Warn" , "Unknow instruction");
                    Log("CMD" , "Warn" , "Please check for your input");
                    Log("CMD" , "Warn" , "==========================");
                    break;
            }
        }
        #endregion

        #region Manually : Input

        private void ManuallyPearlXTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyMomemtumXTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyPearlYTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyMomemtumYTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyPearlZTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyMomemtumZTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyATNTXTexBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyBTNTXTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyATNTYTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyBTNTYTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyATNTZTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyBTNTZTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyATNTAmountTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        private void ManuallyBTNTAmountTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        #endregion

    }
}
