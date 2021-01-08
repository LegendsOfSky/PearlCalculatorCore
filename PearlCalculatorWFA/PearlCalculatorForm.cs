using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.General;

namespace PearlCalculatorWFA
{
    public partial class PearlCalculatorWFA : Form
    {

        const string FileSuffix = "pcld file|*.pcld";

        bool IsDisplayOnTNT = false;


        public PearlCalculatorWFA()
        {
            InitializeComponent();

            ConsoleListView.Columns.Add("Type" , 80 , HorizontalAlignment.Left);
            ConsoleListView.Columns.Add("Message" , 360 , HorizontalAlignment.Left);

            BasicDirectionOutSystem.Columns.Add("Direction" , 80 , HorizontalAlignment.Left);
            BasicDirectionOutSystem.Columns.Add("Angle" , 240 , HorizontalAlignment.Left);

            SettingListView.Columns.Add("Name" , 120 , HorizontalAlignment.Left);
            SettingListView.Columns.Add("Value" , 360 , HorizontalAlignment.Left);

            DisplaySetting();
        }

        public void DisplaySetting()
        {
            ListViewItem NorthWestVectorX = new ListViewItem("North West TNT Vector X");
            NorthWestVectorX.SubItems.Add(Data.NorthWest.InducedVector.X.ToString());
            ListViewItem NorthWestVectorY = new ListViewItem("North West TNT Vector Y");
            NorthWestVectorY.SubItems.Add(Data.NorthWest.InducedVector.Y.ToString());
            ListViewItem NorthWestVectorZ = new ListViewItem("North West TNT Vector Z");
            NorthWestVectorZ.SubItems.Add(Data.NorthWest.InducedVector.Z.ToString());

            ListViewItem NorthEastVectorX = new ListViewItem("North East TNT Vector X");
            NorthEastVectorX.SubItems.Add(Data.NorthEast.InducedVector.X.ToString());
            ListViewItem NorthEastVectorY = new ListViewItem("North East TNT Vector Y");
            NorthEastVectorY.SubItems.Add(Data.NorthEast.InducedVector.Y.ToString());
            ListViewItem NorthEastVectorZ = new ListViewItem("North East TNT Vector Z");
            NorthEastVectorZ.SubItems.Add(Data.NorthEast.InducedVector.Z.ToString());

            ListViewItem SouthWestVectorX = new ListViewItem("South West TNT Vector X");
            SouthWestVectorX.SubItems.Add(Data.SouthWest.InducedVector.X.ToString());
            ListViewItem SouthWestVectorY = new ListViewItem("South West TNT Vector Y");
            SouthWestVectorY.SubItems.Add(Data.SouthWest.InducedVector.Y.ToString());
            ListViewItem SouthWestVectorZ = new ListViewItem("South West TNT Vector Z");
            SouthWestVectorZ.SubItems.Add(Data.SouthWest.InducedVector.Z.ToString());

            ListViewItem SouthEastVectorX = new ListViewItem("South East TNT Vector X");
            SouthEastVectorX.SubItems.Add(Data.SouthEast.InducedVector.X.ToString());
            ListViewItem SouthEastVectorY = new ListViewItem("South East TNT Vector Y");
            SouthEastVectorY.SubItems.Add(Data.SouthEast.InducedVector.Y.ToString());
            ListViewItem SouthEastVectorZ = new ListViewItem("South East TNT Vector Z");
            SouthEastVectorZ.SubItems.Add(Data.SouthEast.InducedVector.Z.ToString());

            ListViewItem PearlPositionY = new ListViewItem("Pearl Position Y");
            PearlPositionY.SubItems.Add(Data.Pearl.Position.Y.ToString());
            ListViewItem PearlVectorY = new ListViewItem("Pearl Vector Y");
            PearlVectorY.SubItems.Add(Data.Pearl.Vector.Y.ToString());

            ListViewItem RedSouthArray = new ListViewItem("South Array For Red");
            RedSouthArray.SubItems.Add(Data.SouthArray.Red);
            ListViewItem BlueSouthArray = new ListViewItem("South Array For Blue");
            BlueSouthArray.SubItems.Add(Data.SouthArray.Blue);

            ListViewItem RedWestArray = new ListViewItem("West Array For Red");
            RedWestArray.SubItems.Add(Data.SouthArray.Red);
            ListViewItem BlueWestArray = new ListViewItem("West Array For Blue");
            BlueWestArray.SubItems.Add(Data.SouthArray.Blue);
            
            ListViewItem RedNorthArray = new ListViewItem("North Array For Red");
            RedNorthArray.SubItems.Add(Data.SouthArray.Red);
            ListViewItem BlueNorthArray = new ListViewItem("North Array For Blue");
            BlueNorthArray.SubItems.Add(Data.SouthArray.Blue);

            ListViewItem RedEastArray = new ListViewItem("East Array For Red");
            RedEastArray.SubItems.Add(Data.SouthArray.Red);
            ListViewItem BlueEastArray = new ListViewItem("East Array For Blue");
            BlueEastArray.SubItems.Add(Data.SouthArray.Blue);

            ListViewItem OffsetX = new ListViewItem("Offset On X Axis");
            OffsetX.SubItems.Add(Data.PearlOffset.X.ToString());
            ListViewItem OffsetZ = new ListViewItem("Offset On Z Axis");
            OffsetZ.SubItems.Add(Data.PearlOffset.Z.ToString());

            SettingListView.Items.Add(NorthWestVectorX);
            SettingListView.Items.Add(NorthWestVectorY);
            SettingListView.Items.Add(NorthWestVectorZ);

            SettingListView.Items.Add(NorthEastVectorX);
            SettingListView.Items.Add(NorthEastVectorY);
            SettingListView.Items.Add(NorthEastVectorZ);

            SettingListView.Items.Add(SouthWestVectorX);
            SettingListView.Items.Add(SouthWestVectorY);
            SettingListView.Items.Add(SouthWestVectorZ);

            SettingListView.Items.Add(SouthEastVectorX);
            SettingListView.Items.Add(SouthEastVectorY);
            SettingListView.Items.Add(SouthEastVectorZ);

            SettingListView.Items.Add(PearlPositionY);
            SettingListView.Items.Add(PearlVectorY);

            SettingListView.Items.Add(RedSouthArray);
            SettingListView.Items.Add(BlueSouthArray);

            SettingListView.Items.Add(RedWestArray);
            SettingListView.Items.Add(BlueWestArray);

            SettingListView.Items.Add(RedNorthArray);
            SettingListView.Items.Add(BlueNorthArray);

            SettingListView.Items.Add(RedEastArray);
            SettingListView.Items.Add(BlueEastArray);
        }

        private void BasicOutputSystem_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            if(IsDisplayOnTNT)
            {
                switch(e.Column)
                {
                    case 0:
                        Data.TNTResult.SortByDistance();
                        break;
                    case 1:
                        Data.TNTResult.SortByTick();
                        break;
                    case 4:
                        Data.TNTResult.SortByTotal();
                        break;
                    default:
                        break;
                }
                DisplayTNTAmount(true);
            }
        }


        private void GeneralFTLTabControl_SelectedIndexChanged(object sender , EventArgs e)
        {

        }

        private void CalculateTNTButton_Click(object sender , EventArgs e)
        {
            if(Calculation.CalculateTNTAmount(20))
            {
                DisplayTNTAmount(false);
                DisplayDirection();
                IsDisplayOnTNT = true;
            }

        }


        public void DisplayDirection()
        {
            BasicDirectionOutSystem.Items.Clear();
            ListViewItem result = new ListViewItem();
            result.SubItems.Add(Data.Pearl.Position.Direction(Data.Pearl.Position.Angle(Data.Destination)).ToString());
            result.SubItems.Add(Data.Pearl.Position.Angle(Data.Destination).ToString());
            BasicDirectionOutSystem.Items.Add(result);
        }

        public void DisplayTNTAmount(bool isRadioOverriden)
        {
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Distance" , 120 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Ticks" , 40 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Blue" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Red" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Total TNT" , 60 , HorizontalAlignment.Left);

            if(!isRadioOverriden)
            {
                if(TNTRadioButton.Checked)
                    Data.TNTResult.SortByTotal();
                else if(DistanceRadioButton.Checked)
                    Data.TNTResult.SortByDistance();
                else
                    Data.TNTResult.SortByDistanceWithWeight();
            }

            for(int i = 0; i < Data.TNTResult.Count; i++)
            {
                ListViewItem result = new ListViewItem(Data.TNTResult[i].Distance.ToString());
                result.SubItems.Add(Data.TNTResult[i].Tick.ToString());
                result.SubItems.Add(Data.TNTResult[i].Blue.ToString());
                result.SubItems.Add(Data.TNTResult[i].Red.ToString());
                result.SubItems.Add(Data.TNTResult[i].TotalTNT.ToString());
                BasicOutputSystem.Items.Add(result);
            }
        }

        private void SettingInputTextBox_TextChanged(object sender , EventArgs e)
        {

        }

        #region Input

        private void PearlXTextBox_TextChanged(object sender , EventArgs e)
        {
            Double.TryParse(PearlXTextBox.Text , out Data.Pearl.Position.X);
        }

        private void PearlZTextBox_TextChanged(object sender , EventArgs e)
        {
            Double.TryParse(PearlZTextBox.Text , out Data.Pearl.Position.Z);
        }

        private void DestinationXTextBox_TextChanged(object sender , EventArgs e)
        {
            Double.TryParse(DestinationXTextBox.Text , out Data.Destination.X);
        }

        private void DestinationZTextBox_TextChanged(object sender , EventArgs e)
        {
            Double.TryParse(DestinationZTextBox.Text , out Data.Destination.Z);
        }

        private void MaxTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            Int32.TryParse(MaxTNTTextBox.Text , out Data.MaxTNT);
        }

        private void RedTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            Int32.TryParse(RedTNTTextBox.Text , out Data.RedTNT);
        }

        private void BlueTNTTextBox_TextChanged(object sender , EventArgs e)
        {
            Int32.TryParse(BlueTNTTextBox.Text , out Data.BlueTNT);
        }
        private void NorthRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = "North";
        }

        private void SouthRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = "South";
        }

        private void EastRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = "East";
        }

        private void WestRadioButton_CheckedChanged(object sender , EventArgs e)
        {
            Data.Direction = "West";
        }

        #region Offsets input

        private void OffsetXTextBox_TextChanged(object sender , EventArgs e)
        {
            var args = new TextBoxZeroToOneCheckerArgs()
            {
                TextBox = OffsetXTextBox,
                Value = Data.PearlOffset.X
            };

            ZeroToOneCheckModifier.CheckModifier.Check(args);

            Data.PearlOffset.X = args.Value;
        }

        private void OffsetZTextBox_TextChanged(object sender , EventArgs e)
        {
            var args = new TextBoxZeroToOneCheckerArgs()
            {
                TextBox = OffsetZTextBox,
                Value = Data.PearlOffset.Z
            };

            ZeroToOneCheckModifier.CheckModifier.Check(args);

            Data.PearlOffset.Z = args.Value;
        }

        #endregion

        #endregion

        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {
            int index = BasicOutputSystem.FocusedItem.Index;
            string direction = Data.Pearl.Position.Direction(Data.Pearl.Position.Angle(Data.Destination));

            RedTNTTextBox.Text = Data.TNTResult[index].Red.ToString();
            BlueTNTTextBox.Text = Data.TNTResult[index].Blue.ToString();

            switch(direction)
            {
                case "North":
                    NorthRadioButton.Checked = true;
                    break;
                case "South":
                    SouthRadioButton.Checked = true;
                    break;
                case "East":
                    EastRadioButton.Checked = true;
                    break;
                case "West":
                    WestRadioButton.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void TNTWeightGroupBox_Enter(object sender , EventArgs e)
        {

        }

        private void TNTWeightTrackerSlider_Scroll(object sender , EventArgs e)
        {
            Data.TNTWeight = TNTWeightTrackerSlider.Value;
            DisplayTNTAmount(false);
        }

        public void Log(string type , string message)
        {
            ListViewItem log = new ListViewItem(type);
            log.SubItems.Add(message);
            ConsoleListView.Items.Add(log);
        }

        private void PearlSimulateButton_Click(object sender , EventArgs e)
        {
            DisplayPearTrace(Calculation.CalculatePearlTrace(Data.RedTNT , Data.BlueTNT , 40 , Data.Direction));
            IsDisplayOnTNT = false;
        }

        public void DisplayPearTrace(List<Pearl> pearlTrace)
        {
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Ticks" , 40 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("X Coordinate" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Y Coordinate" , 100 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Z Coordinate" , 100 , HorizontalAlignment.Left);
            for(int i = 0; i < pearlTrace.Count; i++)
            {
                ListViewItem result = new ListViewItem(i.ToString());
                result.SubItems.Add(pearlTrace[i].Position.X.ToString());
                result.SubItems.Add(pearlTrace[i].Position.Y.ToString());
                result.SubItems.Add(pearlTrace[i].Position.Z.ToString());
                BasicOutputSystem.Items.Add(result);
            }
        }

        #region File Import/Export

        private void ImportSettingButton_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = FileSuffix;
            Settings settings = null;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var fs = File.OpenRead(fileDialog.FileName);
                var bf = new BinaryFormatter();

                settings = bf.Deserialize(fs) as Settings;
                fs.Close();
            }

            if (settings == null) return;

            ImportSettings(settings);
            RefreshInput();
        }

        private void SaveSettingButton_Click(object sender, EventArgs e)
        {
            var bf = new BinaryFormatter();
            var fileDialog = new SaveFileDialog();
            fileDialog.Filter = FileSuffix;
            fileDialog.AddExtension = true;
 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var fs = File.Open(fileDialog.FileName, FileMode.OpenOrCreate);
                bf.Serialize(fs, CreateSavedSettingsData());
                fs.Close();
            }
        }

        Settings CreateSavedSettingsData()
        {
            return new Settings()
            {
                NorthWest   = Data.NorthWest,
                NorthEast   = Data.NorthEast,
                SouthWest   = Data.SouthWest,
                SouthEast   = Data.SouthEast,

                Pearl       = Data.Pearl,

                SouthArray  = Data.SouthArray,
                WestArray   = Data.WestArray,
                NorthArray  = Data.NorthArray,
                EastArray   = Data.EastArray,

                RedTNT      = Data.RedTNT,
                BlueTNT     = Data.BlueTNT,
                MaxTNT      = Data.MaxTNT,

                Destination = Data.Destination,
                Offset      = Data.PearlOffset,

                Direction   = Data.Direction
            };
        }

        void ImportSettings(Settings settings)
        {
            Data.NorthWest = settings.NorthWest;
            Data.NorthEast = settings.NorthEast;
            Data.SouthWest = settings.SouthWest;
            Data.SouthEast = settings.SouthEast;

            Data.Pearl = settings.Pearl;

            Data.SouthArray = settings.SouthArray;
            Data.WestArray  = settings.WestArray;
            Data.NorthArray = settings.NorthArray;
            Data.EastArray  = settings.EastArray;

            Data.RedTNT     = settings.RedTNT;
            Data.BlueTNT    = settings.BlueTNT;
            Data.MaxTNT     = settings.MaxTNT;

            Data.Destination = settings.Destination;
            Data.PearlOffset = settings.Offset;

            Data.Direction   = settings.Direction;
        }

        void RefreshInput()
        {
            PearlXTextBox.Text = Data.Pearl.Position.X.ToString();
            PearlZTextBox.Text = Data.Pearl.Position.Z.ToString();

            DestinationXTextBox.Text = Data.Destination.X.ToString();
            DestinationZTextBox.Text = Data.Destination.Z.ToString();

            MaxTNTTextBox.Text = Data.MaxTNT.ToString();

            switch (Data.Direction)
            {
                case "North":
                    NorthRadioButton.Checked = true;
                    break;
                case "South":
                    SouthRadioButton.Checked = true;
                    break;
                case "East":
                    EastRadioButton.Checked = true;
                    break;
                case "West":
                    WestRadioButton.Checked = true;
                    break;
            }

            RedTNTTextBox.Text = Data.RedTNT.ToString();
            BlueTNTTextBox.Text = Data.BlueTNT.ToString();

            OffsetXTextBox.Text = Data.PearlOffset.X.ToString();
            OffsetZTextBox.Text = Data.PearlOffset.Z.ToString();
        }

        #endregion
    }
}
