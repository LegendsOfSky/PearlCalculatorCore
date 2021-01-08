using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.General;

namespace PearlCalculatorWFA
{
    public partial class PearlCalculatorWFA : Form
    {

        bool IsDisplayOnTNT = false;

        public PearlCalculatorWFA()
        {
            InitializeComponent();

            ConsoleListView.Columns.Clear();
            ConsoleListView.Items.Clear();
            ConsoleListView.Columns.Add("Type" , 80 , HorizontalAlignment.Left);
            ConsoleListView.Columns.Add("Message" , 360 , HorizontalAlignment.Left);

            BasicDirectionOutSystem.Items.Clear();
            BasicDirectionOutSystem.Columns.Clear();
            BasicDirectionOutSystem.Columns.Add("Direction" , 80 , HorizontalAlignment.Left);
            BasicDirectionOutSystem.Columns.Add("Angle" , 240 , HorizontalAlignment.Left);
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

        #endregion


        private void textBox1_TextChanged(object sender , EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender , EventArgs e)
        {

        }

        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {

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
    }
}
