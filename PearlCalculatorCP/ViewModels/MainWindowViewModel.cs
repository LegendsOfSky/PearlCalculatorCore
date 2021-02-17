using System;
using System.Collections.Generic;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
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

        /// <summary>
        /// On pearl offset x value changed
        /// <param name="parameter1">last value</param>
        /// <param name="parameter2">next value</param>
        /// <param name="parameter3">supress callback</param>
        /// <param name="Parameter4">backing field value</param>
        /// </summary>
        /// <returns>can change, if true, please change backing value, else rollback text to last value and return false</returns>
        public event Func<string, string, Action, double, (bool, double)>? OnPearlOffsetXTextChanged;
        
        /// <summary>
        /// On pearl offset z value changed
        /// <param name="parameter1">last value</param>
        /// <param name="parameter2">next value</param>
        /// <param name="parameter3">supress callback</param>
        /// <param name="Parameter4">backing field value</param>
        /// </summary>
        /// <returns>can change, if true, please change backing value, else rollback text to last value and return false</returns>

        public event Func<string, string, Action, double, (bool, double)>? OnPearlOffsetZTextChanged;

        private bool _isSupressX = false;
        private bool _isSupressZ = false;


        public int TNTWeight
        {
            get => Data.TNTWeight;
            set => this.RaiseAndSetIfChanged(ref Data.TNTWeight, value);
        }

        private double _pearlPosX;
        public double PearlPosX
        {
            get => _pearlPosX;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlPosX, value);
                if (_pearlPosX != Data.Pearl.Position.X)
                    Data.Pearl.Position.X = _pearlPosX;
            }
        }

        private double _pearlPosZ;
        public double PearlPosZ
        {
            get => _pearlPosZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlPosZ, value);
                if (_pearlPosZ != Data.Pearl.Position.Z)
                    Data.Pearl.Position.Z = _pearlPosZ;
            }
        }

        private double _destinationX;
        public double DestinationX
        {
            get => _destinationX;
            set
            {
                this.RaiseAndSetIfChanged(ref _destinationX, value);
                if (_destinationX != Data.Destination.X)
                    Data.Destination.X = _destinationX;
            }
        }

        private double _destinationZ;
        public double DestinationZ
        {
            get => _destinationZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _destinationZ, value);
                if (_destinationZ != Data.Destination.Z)
                    Data.Destination.Z = _destinationZ;
            }
        }

        public uint MaxTNT
        {
            get => (uint)Data.MaxTNT;
            set => this.RaiseAndSetIfChanged(ref Data.MaxTNT, (int)value);
        }

        public Direction Direction
        {
            get => Data.Direction;
            set => this.RaiseAndSetIfChanged(ref Data.Direction, value);
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
                (bool, double) callbackResult;
                if (!_isSupressX && OnPearlOffsetXTextChanged != null &&
                    (callbackResult = OnPearlOffsetXTextChanged.Invoke(_pearlOffsetX, value, () => _isSupressX = true, Data.PearlOffset.Z)).Item1)
                {
                    this.RaiseAndSetIfChanged(ref _pearlOffsetX, value);
                    Data.PearlOffset.X = callbackResult.Item2;
                }
                else _isSupressX = false;
            }   
        }

        private string _pearlOffsetZ = "0.";
        public string PearlOffsetZ
        {
            get => _pearlOffsetZ;
            set
            {
                (bool, double) callbackResult;
                if (!_isSupressZ && OnPearlOffsetZTextChanged != null &&
                    (callbackResult = OnPearlOffsetZTextChanged.Invoke(_pearlOffsetZ, value, () => _isSupressZ = true, Data.PearlOffset.Z)).Item1)
                {
                    this.RaiseAndSetIfChanged(ref _pearlOffsetZ, value);
                    Data.PearlOffset.Z = callbackResult.Item2;
                }
                else _isSupressZ = false;
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

        private string _resultDirection = string.Empty;
        public string ResultDirection
        {
            get => _resultDirection;
            set => this.RaiseAndSetIfChanged(ref _resultDirection, value);
        }

        private string _resultAngle = string.Empty;
        public string ResultAngle
        {
            get => _resultAngle;
            set => this.RaiseAndSetIfChanged(ref _resultAngle, value);
        }

        private bool _isDisplayOnTNT = true;
        public bool IsDisplayOnTNT
        {
            get => _isDisplayOnTNT;
            set => this.RaiseAndSetIfChanged(ref _isDisplayOnTNT, value);
        }

    }
}
