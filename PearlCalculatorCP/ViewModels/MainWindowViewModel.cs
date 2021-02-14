using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using PearlCalculatorLib.General;
using PearlCalculatorLib.Result;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public enum TNTWeightModeEnum
        {
            DistanceVSTNT, OnlyTNT, OnlyDistance
        }

        public event Func<string, string, bool> OnPearlOffsetXTextChanged;
        public event Func<string, string, bool> OnPearlOffsetZTextChanged;

        private bool _isSupressX = false;
        private bool _isSupressZ = false;


        public int TNTWeight
        {
            get => Data.TNTWeight;
            set
            {
                if(this.RaiseAndSetIfChanged(ref Data.TNTWeight, value) != value)
                {
                    
                }
            }
        }

        public double PearlPosX
        {
            get => Data.Pearl.Position.X;
            set => this.RaiseAndSetIfChanged(ref Data.Pearl.Position.X, value);
        }

        public double PearlPosZ
        {
            get => Data.Pearl.Position.Z;
            set => this.RaiseAndSetIfChanged(ref Data.Pearl.Position.Z, value);
        }

        public double DestinationX
        {
            get => Data.Destination.X;
            set => this.RaiseAndSetIfChanged(ref Data.Destination.X, value);
        }
        
        public double DestinationZ
        {
            get => Data.Destination.Z;
            set => this.RaiseAndSetIfChanged(ref Data.Destination.Z, value);
        }

        public uint MaxTNT
        {
            get => (uint)Data.MaxTNT;
            set => this.RaiseAndSetIfChanged(ref Data.MaxTNT, (int)value);
        }

        public uint RedTNT
        {
            get => (uint)Data.RedTNT;
            set => this.RaiseAndSetIfChanged(ref Data.RedTNT, (int)value);
        }

        public uint BlueTNT
        {
            get => (uint)Data.BlueTNT;
            set => this.RaiseAndSetIfChanged(ref Data.BlueTNT, (int)value);
        }

        private string _pearlOffsetX = "0.";
        public string PearlOffsetX
        {
            get => _pearlOffsetX;
            set
            {
                if (!_isSupressX && OnPearlOffsetXTextChanged.Invoke(_pearlOffsetX, value))
                    this.RaiseAndSetIfChanged(ref _pearlOffsetX, value);
                if (_isSupressX) _isSupressX = false;
            }   
        }

        private string _pearlOffsetZ = "0.";
        public string PearlOffsetZ
        {
            get => _pearlOffsetZ;
            set
            {
                if (!_isSupressZ && OnPearlOffsetZTextChanged.Invoke(_pearlOffsetZ, value))
                    this.RaiseAndSetIfChanged(ref _pearlOffsetZ, value);
                if (_isSupressZ) _isSupressZ = false;
            }
        }
        
        private TNTWeightModeEnum _tntWeight;
        public TNTWeightModeEnum TNTWeightMode
        {
            get => _tntWeight;
            set => this.RaiseAndSetIfChanged(ref _tntWeight, value);
        }

        public List<TNTCalculationResult> TNTResult
        {
            get => Data.TNTResult;
            set => this.RaiseAndSetIfChanged(ref Data.TNTResult, value);
        }

        private string _direction = string.Empty;
        public string Direction
        {
            get => _direction;
            set => this.RaiseAndSetIfChanged(ref _direction, value);
        }

        private string _angle = string.Empty;
        public string Angle
        {
            get => _angle;
            set => this.RaiseAndSetIfChanged(ref _angle, value);
        }

        private bool _isDisplayOnTNT = true;
        public bool IsDisplayOnTNT
        {
            get => _isDisplayOnTNT;
            set => this.RaiseAndSetIfChanged(ref _isDisplayOnTNT, value);
        }

        public void SupressNextOffsetXUpdate() => _isSupressX = true;
        public void SupressNextOffsetZUpdate() => _isSupressZ = true;
    }
}
