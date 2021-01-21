﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public PearlCalculatorWFA()
        {
            InitializeComponent();

            ConsoleListView.Columns.Add("Type" , 60);
            ConsoleListView.Columns.Add("Message" , 480);

            BasicOutputSystem.ColumnClick += BasicOutputSystem_ColumnClick;
        }

        private void BasicOutputSystem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            /*
             * Distance 0
             * Tick     1
             * Total    4
             */
            if (!IsDisplayOnTNT) return;

            bool isSorted = true;

            switch (e.Column)
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
                    isSorted = false;
                    break;
            }

            if (isSorted)
                DisplayTNTAmount(true);

        }

        private void CalculateTNTButton_Click(object sender , EventArgs e)
        {
            if(Calculation.CalculateTNTAmount(100))
            {
                DisplayDirection();
                DisplayTNTAmount(false);
            }
        }

        private void DisplayTNTAmount(bool isRadioOverriden)
        {
            BasicOutputSystem.Items.Clear();
            BasicOutputSystem.Columns.Clear();
            BasicOutputSystem.Columns.Add("Distance" , 120 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Ticks" , 40 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Blue" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Red" , 50 , HorizontalAlignment.Left);
            BasicOutputSystem.Columns.Add("Total TNT" , 60 , HorizontalAlignment.Left);

            IsDisplayOnTNT = true;

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

        private void DisplayDirection()
        {
            BasicDirectionOutSystem.Items.Clear();
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

        }

        private void GeneralFTLTabControl_SelectedIndexChanged(object sender , EventArgs e)
        {

        }

        #endregion

        private void PearlSimulateButton_Click(object sender , EventArgs e)
        {
            DisplayPearTrace(Calculation.CalculatePearlTrace(Data.RedTNT , Data.BlueTNT , 40 , Data.Direction));
            IsDisplayOnTNT = false;
        }
        
        private void DisplayPearTrace(List<Pearl> pearlTrace)
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

        private void Log(string type , string message)
        {
            ListViewItem log = new ListViewItem(type);
            log.SubItems.Add(message);
            ConsoleListView.Items.Add(log);
        }

        private void TNTWeightTrackerSlider_Scroll(object sender , EventArgs e)
        {
            Data.TNTWeight = TNTWeightTrackerSlider.Value;
            DisplayTNTAmount(false);
        }

        private void BasicOutputSystem_SelectedIndexChanged(object sender , EventArgs e)
        {
            if(!IsDisplayOnTNT)
                return;
            int index = BasicOutputSystem.FocusedItem.Index;
            string direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination)).ToString();

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

        Settings CreateSavedSettingsData()
        {
            return new Settings()
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
        }
    }
}
