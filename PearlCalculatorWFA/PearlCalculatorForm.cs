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
        string OffsetXTextBoxString = "";
        string OffsetZTextBoxString = "";
        Dictionary<string , string> SettingsLVNameToDataName = new Dictionary<string , string>()
        {
            { "North West TNT Vector X", nameof(Data.NorthWest) } ,
            { "North West TNT Vector Y", nameof(Data.NorthWest) } ,
            { "North West TNT Vector Z", nameof(Data.NorthWest) } ,

            { "North East TNT Vector X", nameof(Data.NorthEast) } ,
            { "North East TNT Vector Y", nameof(Data.NorthEast) } ,
            { "North East TNT Vector Z", nameof(Data.NorthEast) } ,

            { "South West TNT Vector X", nameof(Data.SouthWest) } ,
            { "South West TNT Vector Y", nameof(Data.SouthWest) } ,
            { "South West TNT Vector Z", nameof(Data.SouthWest) } ,

            { "South East TNT Vector X", nameof(Data.SouthEast) } ,
            { "South East TNT Vector Y", nameof(Data.SouthEast) } ,
            { "South East TNT Vector Z", nameof(Data.SouthEast) } ,

            { "Pearl Position Y", nameof(Data.Pearl) },
            { "Pearl Vector Y", nameof(Data.Pearl) },

            { "South Array For Red", nameof(Data.SouthArray) },
            { "South Array For Blue", nameof(Data.SouthArray) },

            { "North Array For Red", nameof(Data.NorthArray) },
            { "North Array For Blue", nameof(Data.NorthArray) },

            { "East Array For Red", nameof(Data.EastArray) },
            { "East Array For Blue", nameof(Data.EastArray) },

            { "West Array For Red", nameof(Data.WestArray) },
            { "West Array For Blue", nameof(Data.WestArray) },

            { "Offset On X Axis", nameof(Data.PearlOffset) },
            { "Offset On Z Axis", nameof(Data.PearlOffset) },

        };

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
            SettingListView.Items.Clear();

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
            RedWestArray.SubItems.Add(Data.WestArray.Red);
            ListViewItem BlueWestArray = new ListViewItem("West Array For Blue");
            BlueWestArray.SubItems.Add(Data.WestArray.Blue);
            
            ListViewItem RedNorthArray = new ListViewItem("North Array For Red");
            RedNorthArray.SubItems.Add(Data.NorthArray.Red);
            ListViewItem BlueNorthArray = new ListViewItem("North Array For Blue");
            BlueNorthArray.SubItems.Add(Data.NorthArray.Blue);

            ListViewItem RedEastArray = new ListViewItem("East Array For Red");
            RedEastArray.SubItems.Add(Data.EastArray.Red);
            ListViewItem BlueEastArray = new ListViewItem("East Array For Blue");
            BlueEastArray.SubItems.Add(Data.EastArray.Blue);

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

            SettingListView.Items.Add(OffsetX);
            SettingListView.Items.Add(OffsetZ);

            OffsetXTextBox.Text = Data.PearlOffset.X.ToString();
            OffsetZTextBox.Text = Data.PearlOffset.Z.ToString();
        }


        private void ChangeSettingButton_Click(object sender , EventArgs e)
        {
            if(SettingListView.FocusedItem == null)
                return;
            int index = SettingListView.FocusedItem.Index;
            string dataName = SettingsLVNameToDataName[SettingListView.FocusedItem.Text];
            Log("Debug" , dataName);
            Log("Debug" , SettingListView.FocusedItem.SubItems[1].Text);

            if(index >= 0 && index < 12)  //TNT Vector
            {
                TNT tnt = new TNT();
                int rank = index / 3;
                int j = index % 3;
                double.TryParse(SettingListView.Items[rank * 3].SubItems[1].Text , out tnt.InducedVector.X);
                double.TryParse(SettingListView.Items[rank * 3 + 1].SubItems[1].Text , out tnt.InducedVector.Y);
                double.TryParse(SettingListView.Items[rank * 3 + 2].SubItems[1].Text , out tnt.InducedVector.Z);
                switch(j)
                {
                    case 0:
                        double.TryParse(SettingInputTextBox.Text , out tnt.InducedVector.X);
                        break;
                    case 1:
                        double.TryParse(SettingInputTextBox.Text , out tnt.InducedVector.Y);
                        break;
                    case 2:
                        double.TryParse(SettingInputTextBox.Text , out tnt.InducedVector.Z);
                        break;
                }
                Data.UpdateData(dataName , tnt);
            }
            else if(index == 12 || index == 13)  // Pearl
            {
                Pearl pearl = new Pearl();
                double.TryParse(SettingListView.Items[12].SubItems[1].Text , out pearl.Position.Y);
                double.TryParse(SettingListView.Items[13].SubItems[1].Text , out pearl.Vector.Y);
                if(index == 12)
                    double.TryParse(SettingInputTextBox.Text , out pearl.Position.Y);
                else
                    double.TryParse(SettingInputTextBox.Text , out pearl.Vector.Y);
                Data.UpdateData(dataName , pearl);
            }
            else if(index >= 14 && index < 22)  // TNTArray
            {
                TNTArray tntArray = new TNTArray();
                int rank = (index - 14) / 2;
                int j = (index - 14) % 2;
                tntArray.Red = SettingListView.Items[rank * 2 + 14].SubItems[1].Text;
                tntArray.Blue = SettingListView.Items[rank * 2 + 15].SubItems[1].Text;
                if(j == 0)
                    tntArray.Red = SettingInputTextBox.Text;
                else
                    tntArray.Blue = SettingInputTextBox.Text;
                Data.UpdateData(dataName , tntArray);
            }
            else    // Offset
            {
                Space3D offset = new Space3D();
                double.TryParse(SettingListView.Items[22].SubItems[1].Text , out offset.X);
                double.TryParse(SettingListView.Items[23].SubItems[1].Text , out offset.Z);
                if(index == 22)
                    double.TryParse(SettingInputTextBox.Text , out offset.X);
                else
                    double.TryParse(SettingInputTextBox.Text , out offset.Z);
                Data.UpdateData(dataName , offset);
            }
            SettingListView.FocusedItem.Focused = false;
            DisplaySetting();
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
            ListViewItem result = new ListViewItem(Data.Pearl.Position.Direction(Data.Pearl.Position.Angle(Data.Destination)).ToString());
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
                    Data.TNTResult.SortByWeightedTotal();
                else if(DistanceRadioButton.Checked)
                    Data.TNTResult.SortByDistance();
                else
                    Data.TNTResult.SortByWeightedDistance();
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
                Data.UpdateData(nameof(Data.PearlOffset) , offset);
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
                Data.UpdateData(nameof(Data.PearlOffset) , offset);
                DisplaySetting();
                OffsetZTextBoxString = OffsetZTextBox.Text;
            }
        }

        #endregion

        #endregion

        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {
            if(!IsDisplayOnTNT)
                return;
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

        private void ResetSettingButton_Click(object sender , EventArgs e)
        {
            Data.ResetParameter();
        }

    }
}
