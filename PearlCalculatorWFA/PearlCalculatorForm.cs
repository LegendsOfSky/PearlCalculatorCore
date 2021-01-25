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

        public PearlCalculatorWFA()
        {
            InitializeComponent();

            ConsoleListView.Columns.Add("Type" , 90);
            ConsoleListView.Columns.Add("Message" , 260);
            BasicDirectionOutSystem.Columns.Add("Direction" , 120);
            BasicDirectionOutSystem.Columns.Add("Angle" , 230);
            SettingListView.Columns.Add("Name" , 120);
            SettingListView.Columns.Add("Value" , 240);
            Log("Main" , "Msg" , "InitializeComponent");
            DisplaySetting();
            BasicOutputSystem.ColumnClick += BasicOutputSystem_ColumnClick;
        }

        private void BasicOutputSystem_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            /*
             * Distance 0
             * Tick     1
             * Total    4
             */
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
            if(Calculation.CalculateTNTAmount(100))
            {
                Log("Main" , "Msg" , "TNT calculated");
                DisplayTNTAmount(false);
                DisplayDirection();
            }
            else
            {
                Log("Main" , "Warn" , "=============================");
                Log("Main" , "Warn" , "TNT did not calculated");
                Log("Main" , "Warn" , "Please check for your input");
                Log("Main" , "Warn" , "=============================");
            }
        }

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
                if(TNTRadioButton.Checked)
                {
                    Log("Main" , "Msg" , "Sort TNT result by weighted total TNT");
                    Data.TNTResult.SortByWeightedTotal();
                }
                else if(DistanceRadioButton.Checked)
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

        #region Import

        private void PearlXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(PearlXTextBox.Text , out Data.Pearl.Position.X);
        }

        private void PearlZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(PearlZTextBox.Text , out Data.Pearl.Position.Z);
        }

        private void DestinationXTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(DestinationXTextBox.Text , out Data.Destination.X);
        }

        private void DestinationZTextBox_TextChanged(object sender , EventArgs e)
        {
            double.TryParse(DestinationZTextBox.Text , out Data.Destination.Z);
        }

        private void MaxTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(MaxTNTTextBox.Text , out Data.MaxTNT);
        }

        private void RedTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(RedTNTTextBox.Text , out Data.RedTNT);
        }

        private void BlueTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            int.TryParse(BlueTNTTextBox.Text , out Data.BlueTNT);
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

        private void OffsetXTextBox_TextChanged(object sender , EventArgs e)
        {
            if(OffsetXTextBox.Text == OffsetXTextBoxString)
            {
                return;
            }
            Space3D offset = new Space3D();
            if(!double.TryParse(OffsetXTextBox.Text , out offset.X) || offset.X >= 1)
            {
                OffsetXTextBox.Text = OffsetXTextBoxString;
                OffsetXTextBox.Select(OffsetXTextBox.Text.Length , 0);
            }
            else
            {
                offset.Z = Data.PearlOffset.Z;
                Data.PearlOffset = offset;
                DisplaySetting();
                OffsetXTextBoxString = OffsetXTextBox.Text;
            }
        }

        private void OffsetZTextBox_TextChanged(object sender , EventArgs e)
        {
            if(OffsetZTextBox.Text == OffsetZTextBoxString)
            {
                return;
            }
            Space3D offset = new Space3D();
            if(!double.TryParse(OffsetZTextBox.Text , out offset.Z) || offset.Z >= 1)
            {
                OffsetZTextBox.Text = OffsetZTextBoxString;
                OffsetZTextBox.Select(OffsetZTextBox.Text.Length , 0);
            }
            else
            {
                offset.X = Data.PearlOffset.X;
                Data.PearlOffset = offset;
                DisplaySetting();
                OffsetZTextBoxString = OffsetZTextBox.Text;
            }
        }

        private void GeneralFTLTabControl_SelectedIndexChanged(object sender , EventArgs e)
        {

        }

        #endregion

        private void PearlSimulateButton_Click(object sender , EventArgs e) => PearlSimulate();

        private void PearlSimulate()
        {
            Log("Main" , "Msg" , "Caluete pearl trace");
            DisplayPearTrace(Calculation.CalculatePearlTrace(Data.RedTNT , Data.BlueTNT , 100 , Data.Direction));
            IsDisplayOnTNT = false;
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

        private void Log(string thread , string type , string message)
        {
            ListViewItem log = new ListViewItem("[" + thread + "/" + type + "]");
            log.SubItems.Add(message);
            ConsoleListView.Items.Add(log);
        }

        private void TNTWeightTrackerSlider_Scroll(object sender , EventArgs e)
        {
            Log("Main" , "Msg" , "Change TNT weight value");
            Data.TNTWeight = TNTWeightTrackerSlider.Value;
            DisplayTNTAmount(false);
        }

        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {
            if(!IsDisplayOnTNT)
                return;
            Log("Main" , "Msg" , "Auto-implement");
            int index = BasicOutputSystem.FocusedItem.Index;
            var direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination));

            RedTNTTextBox.Text = Data.TNTResult[index].Red.ToString();
            BlueTNTTextBox.Text = Data.TNTResult[index].Blue.ToString();

            switch(direction)
            {
                case Direction.North:
                    NorthRadioButton.Checked = true;
                    break;
                case Direction.South:
                    SouthRadioButton.Checked = true;
                    break;
                case Direction.East:
                    EastRadioButton.Checked = true;
                    break;
                case Direction.West:
                    WestRadioButton.Checked = true;
                    break;
            }
        }

        #region Import/Export Settings

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
            PearlXTextBox.Text = Data.Pearl.Position.X.ToString();
            PearlZTextBox.Text = Data.Pearl.Position.Z.ToString();

            DestinationXTextBox.Text = Data.Destination.X.ToString();
            DestinationZTextBox.Text = Data.Destination.Z.ToString();

            MaxTNTTextBox.Text = Data.MaxTNT.ToString();

            switch(Data.Direction)
            {
                case Direction.North:
                    NorthRadioButton.Checked = true;
                    break;
                case Direction.South:
                    SouthRadioButton.Checked = true;
                    break;
                case Direction.East:
                    EastRadioButton.Checked = true;
                    break;
                case Direction.West:
                    WestRadioButton.Checked = true;
                    break;
            }

            RedTNTTextBox.Text = Data.RedTNT.ToString();
            BlueTNTTextBox.Text = Data.BlueTNT.ToString();

            OffsetXTextBox.Text = Data.PearlOffset.X.ToString();
            OffsetZTextBox.Text = Data.PearlOffset.Z.ToString();

            PearlSimulate();
        }

        #endregion


        private void DisplaySetting()
        {
            Log("Main" , "Msg" , "Clear setting display");
            SettingListView.Items.Clear();

            Log("Main" , "Msg" , "Output settings");
            ListViewItem NorthWestTNTCoorX = new ListViewItem("North West TNT(X Axis)");
            NorthWestTNTCoorX.SubItems.Add(Data.NorthWestTNT.X.ToString());
            SettingListView.Items.Add(NorthWestTNTCoorX);

            ListViewItem NorthWestTNTCoorY = new ListViewItem("North West TNT(Y Axis)");
            NorthWestTNTCoorY.SubItems.Add(Data.NorthWestTNT.Y.ToString());
            SettingListView.Items.Add(NorthWestTNTCoorY);

            ListViewItem NorthWestTNTCoorZ = new ListViewItem("North West TNT(Z Axis)");
            NorthWestTNTCoorZ.SubItems.Add(Data.NorthWestTNT.Z.ToString());
            SettingListView.Items.Add(NorthWestTNTCoorZ);


            ListViewItem NorthEastTNTCoorX = new ListViewItem("North East TNT(X Axis)");
            NorthEastTNTCoorX.SubItems.Add(Data.NorthEastTNT.X.ToString());
            SettingListView.Items.Add(NorthEastTNTCoorX);

            ListViewItem NorthEastTNTCoorY = new ListViewItem("North East TNT(Y Axis)");
            NorthEastTNTCoorY.SubItems.Add(Data.NorthEastTNT.Y.ToString());
            SettingListView.Items.Add(NorthEastTNTCoorY);

            ListViewItem NorthEastTNTCoorZ = new ListViewItem("North East TNT(Z Axis)");
            NorthEastTNTCoorZ.SubItems.Add(Data.NorthEastTNT.Z.ToString());
            SettingListView.Items.Add(NorthEastTNTCoorZ);


            ListViewItem SouthWestTNTCoorX = new ListViewItem("South West TNT(X Axis)");
            SouthWestTNTCoorX.SubItems.Add(Data.SouthWestTNT.X.ToString());
            SettingListView.Items.Add(SouthWestTNTCoorX);

            ListViewItem SouthWestTNTCoorY = new ListViewItem("South West TNT(Y Axis)");
            SouthWestTNTCoorY.SubItems.Add(Data.SouthWestTNT.Y.ToString());
            SettingListView.Items.Add(SouthWestTNTCoorY);

            ListViewItem SouthWestTNTCoorZ = new ListViewItem("South West TNT(Z Axis)");
            SouthWestTNTCoorZ.SubItems.Add(Data.SouthWestTNT.Z.ToString());
            SettingListView.Items.Add(SouthWestTNTCoorZ);


            ListViewItem SouthEastTNTCoorX = new ListViewItem("South East TNT(X Axis)");
            SouthEastTNTCoorX.SubItems.Add(Data.SouthEastTNT.X.ToString());
            SettingListView.Items.Add(SouthEastTNTCoorX);

            ListViewItem SouthEastTNTCoorY = new ListViewItem("South East TNT(Y Axis)");
            SouthEastTNTCoorY.SubItems.Add(Data.SouthEastTNT.Y.ToString());
            SettingListView.Items.Add(SouthEastTNTCoorY);

            ListViewItem SouthEastTNTCoorZ = new ListViewItem("South East TNT(Z Axis)");
            SouthEastTNTCoorZ.SubItems.Add(Data.SouthEastTNT.Z.ToString());
            SettingListView.Items.Add(SouthEastTNTCoorZ);


            ListViewItem PearlOffsetX = new ListViewItem("Pearl Offset(X Axis)");
            PearlOffsetX.SubItems.Add(Data.PearlOffset.X.ToString());
            SettingListView.Items.Add(PearlOffsetX);

            ListViewItem PearlOffsetZ = new ListViewItem("Pearl Offset(Z Axis)");
            PearlOffsetZ.SubItems.Add(Data.PearlOffset.Z.ToString());
            SettingListView.Items.Add(PearlOffsetZ);


            ListViewItem PearlYCoordinate = new ListViewItem("Pearl Y Coordinate");
            PearlYCoordinate.SubItems.Add(Data.Pearl.Position.Y.ToString());
            SettingListView.Items.Add(PearlYCoordinate);

            ListViewItem PearlYMomentum = new ListViewItem("Pearl Y Momemtum");
            PearlYMomentum.SubItems.Add(Data.Pearl.Vector.Y.ToString());
            SettingListView.Items.Add(PearlYMomentum);

            Log("Main" , "Msg" , "Settings output finished");
        }

        private void SettingListView_SelectedIndexChanged(object sender , EventArgs e)
        {

        }

        private void ChangeSettingButton_Click(object sender , EventArgs e)
        {
            int j;
            int k;
            if(SettingListView.FocusedItem == null)
            {
                Log("Main" , "Warn" , "=============================");
                Log("Main" , "Warn" , "Settings did not change");
                Log("Main" , "Warn" , "Please check for your selection");
                Log("Main" , "Warn" , "You had to select the before changed");
                Log("Main" , "Warn" , "=============================");
                return;
            }
            if(SettingListView.FocusedItem.Index < 12 && SettingListView.FocusedItem.Index >= 0)
            {
                Log("Main" , "Msg" , "Update TNT settings");
                j = SettingListView.FocusedItem.Index / 3;
                k = SettingListView.FocusedItem.Index % 3;
                Space3D setting;
                double.TryParse(SettingListView.Items[j * 3].SubItems[1].Text , out setting.X);
                double.TryParse(SettingListView.Items[j * 3 + 1].SubItems[1].Text , out setting.Y);
                double.TryParse(SettingListView.Items[j * 3 + 2].SubItems[1].Text , out setting.Z);
                switch(k)
                {
                    case 0:
                        double.TryParse(SettingInputTextBox.Text , out setting.X);
                        break;
                    case 1:
                        double.TryParse(SettingInputTextBox.Text , out setting.Y);
                        break;
                    case 2:
                        double.TryParse(SettingInputTextBox.Text , out setting.Z);
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
            else if(SettingListView.FocusedItem.Index == 12)
            {
                Log("Main" , "Msg" , "Update pearl offset");
                Space3D setting = new Space3D();
                double.TryParse(SettingInputTextBox.Text , out setting.X);
                double.TryParse(SettingListView.Items[13].SubItems[1].Text , out setting.Z);
                Data.PearlOffset = setting;
            }
            else if(SettingListView.FocusedItem.Index == 13)
            {
                Log("Main" , "Msg" , "Update pearl offset");
                Space3D setting = new Space3D();
                double.TryParse(SettingListView.Items[12].SubItems[1].Text , out setting.X);
                double.TryParse(SettingInputTextBox.Text , out setting.Z);
                Data.PearlOffset = setting;
            }
            else if(SettingListView.FocusedItem.Index == 14)
            {
                Log("Main" , "Msg" , "Update pearl Y axis position");
                double.TryParse(SettingInputTextBox.Text , out Data.Pearl.Position.Y);
            }
            else if(SettingListView.FocusedItem.Index == 15)
            {
                Log("Main" , "Msg" , "Update pearl Y axis momentum");
                double.TryParse(SettingInputTextBox.Text , out Data.Pearl.Vector.Y);
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
    }
}
