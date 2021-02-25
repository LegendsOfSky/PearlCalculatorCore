using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PearlCalculatorCP.Models;
using PearlCalculatorCP.Commands;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using ReactiveUI;

using ManuallyCalculation = PearlCalculatorLib.Manually.Calculation;
using ManuallyData = PearlCalculatorLib.Manually.Data;

namespace PearlCalculatorCP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        /// <summary>
        /// On pearl offset x value changed
        /// <param name="parameter1">last value</param>
        /// <param name="parameter2">next value</param>
        /// <param name="parameter3">supress callback</param>
        /// <param name="Parameter4">backing field value</param>
        /// </summary>
        /// <returns>bool: can change this value; double: target value</returns>
        public event Func<string, string, Action, double, (bool, double)>? OnPearlOffsetXTextChanged;
        
        /// <summary>
        /// On pearl offset z value changed
        /// <param name="parameter1">last value</param>
        /// <param name="parameter2">next value</param>
        /// <param name="parameter3">supress callback</param>
        /// <param name="Parameter4">backing field value</param>
        /// </summary>
        /// <returns>bool: can change this value; double: target value</returns>
        public event Func<string, string, Action, double, (bool, double)>? OnPearlOffsetZTextChanged;

        private bool _isSupressX = false;
        private bool _isSupressZ = false;

        public static int MaxTicks { get; set; } = 100;

        #region GeneralFTL General Input Data
        
        private double _pearlPosX;
        public double PearlPosX
        {
            get => _pearlPosX;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlPosX, value);
                DataUpdate(ref _pearlPosX, ref Data.Pearl.Position.X);
            }
        }

        private double _pearlPosZ;
        public double PearlPosZ
        {
            get => _pearlPosZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlPosZ, value);
                DataUpdate(ref _pearlPosZ, ref Data.Pearl.Position.Z);
            }
        }

        private double _destinationX;
        public double DestinationX
        {
            get => _destinationX;
            set
            {
                this.RaiseAndSetIfChanged(ref _destinationX, value);
                DataUpdate(ref _destinationX, ref Data.Destination.X);
            }
        }

        private double _destinationZ;
        public double DestinationZ
        {
            get => _destinationZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _destinationZ, value);
                DataUpdate (ref _destinationZ, ref Data.Destination.Z);
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

        #endregion

        #region GeneralFTL Advanced Input Data

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
        
        public int TNTWeight
        {
            get => Data.TNTWeight;
            set => this.RaiseAndSetIfChanged(ref Data.TNTWeight, value);
        }
        
        private TNTWeightModeEnum _tntWeight;
        public TNTWeightModeEnum TNTWeightMode
        {
            get => _tntWeight;
            set => this.RaiseAndSetIfChanged(ref _tntWeight, value);
        }
        
        #endregion

        #region GeneralFTL Result Data

        #region Calculation TNT Amount

        private ObservableCollection<TNTCalculationResult>? _tntResult;
        public ObservableCollection<TNTCalculationResult>? TNTResult
        {
            get => _tntResult;
            set
            {
                _tntResult = value;
                this.RaisePropertyChanged();
            }
        }

        private int _tntResultSelectedIndex = -1;
        public int TNTResultSelectedIndex
        {
            get => _tntResultSelectedIndex;
            set
            {
                if (value >= 0 && value < TNTResult.Count)
                {
                    this.RaiseAndSetIfChanged(ref _tntResultSelectedIndex, value);
                    Direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination));
                    BlueTNT = (uint)_tntResult[value].Blue;
                    RedTNT = (uint) _tntResult[value].Red;
                }
            }
        }
        
        #endregion

        #region Pearl trace

        private List<PearlTraceModel>? _pearlTraceList;
        public List<PearlTraceModel>? PearlTraceList
        {
            get => _pearlTraceList;
            set => this.RaiseAndSetIfChanged(ref _pearlTraceList, value);
        }
        
        #endregion

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

        private bool _isDisplayTNTAmount = true;
        public bool IsDisplayTNTAmount
        {
            get => _isDisplayTNTAmount;
            set => this.RaiseAndSetIfChanged(ref _isDisplayTNTAmount, value);
        }

        private bool _isDisplayMotion = false;
        public bool IsDisplayMotion
        {
            get => _isDisplayMotion;
            set => this.RaiseAndSetIfChanged(ref _isDisplayMotion, value);
        }

        #endregion

        #region Settings Data

        private double _northWestTNTX;
        public double NorthWestTNTX
        {
            get => _northWestTNTX;
            set
            {
                this.RaiseAndSetIfChanged(ref _northWestTNTX, value);
                DataUpdate(ref _northWestTNTX, ref Data.NorthWestTNT.X);
            }
        }

        private double _northWestTNTY;
        public double NorthWestTNTY
        {
            get => _northWestTNTY;
            set
            {
                this.RaiseAndSetIfChanged(ref _northWestTNTY, value);
                DataUpdate(ref _northWestTNTY, ref Data.NorthWestTNT.Y);
            }
        }

        private double _northWestTNTZ;
        public double NorthWestTNTZ
        {
            get => _northWestTNTZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _northWestTNTZ, value);
                DataUpdate(ref _northWestTNTZ, ref Data.NorthWestTNT.Z);
            }
        }

        private double _northEastTNTX;
        public double NorthEastTNTX
        {
            get => _northEastTNTX;
            set
            {
                this.RaiseAndSetIfChanged(ref _northEastTNTX, value);
                DataUpdate(ref _northEastTNTX, ref Data.NorthEastTNT.X);
            }
        }

        private double _northEastTNTY;
        public double NorthEastTNTY
        {
            get => _northEastTNTY;
            set
            {
                this.RaiseAndSetIfChanged(ref _northEastTNTY, value);
                DataUpdate(ref _northEastTNTY, ref Data.NorthEastTNT.Y);
            }
        }

        private double _northEastTNTZ;
        public double NorthEastTNTZ
        {
            get => _northEastTNTZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _northEastTNTZ, value);
                DataUpdate(ref _northEastTNTZ, ref Data.NorthEastTNT.Z);
            }
        }

        private double _southWestTNTX;
        public double SouthWestTNTX
        {
            get => _southWestTNTX;
            set
            {
                this.RaiseAndSetIfChanged(ref _southWestTNTX, value);
                DataUpdate(ref _southWestTNTX, ref Data.SouthWestTNT.X);
            }
        }

        private double _southWestTNTY;
        public double SouthWestTNTY
        {
            get => _southWestTNTY;
            set
            {
                this.RaiseAndSetIfChanged(ref _southWestTNTY, value);
                DataUpdate(ref _southWestTNTY, ref Data.SouthWestTNT.Y);
            }
        }

        private double _southWestTNTZ;
        public double SouthWestTNTZ
        {
            get => _southWestTNTZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _southWestTNTZ, value);
                DataUpdate(ref _southWestTNTZ, ref Data.SouthWestTNT.Z);
            }
        }

        private double _southEastTNTX;
        public double SouthEastTNTX
        {
            get => _southEastTNTX;
            set
            {
                this.RaiseAndSetIfChanged(ref _southEastTNTX, value);
                DataUpdate(ref _southEastTNTX, ref Data.SouthEastTNT.X);
            }
        }

        private double _southEastTNTY;
        public double SouthEastTNTY
        {
            get => _southEastTNTY;
            set
            {
                this.RaiseAndSetIfChanged(ref _southEastTNTY, value);
                DataUpdate(ref _southEastTNTY, ref Data.SouthEastTNT.Y);
            }
        }

        private double _southEastTNTZ;
        public double SouthEastTNTZ
        {
            get => _southEastTNTZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _southEastTNTZ, value);
                DataUpdate(ref _southEastTNTZ, ref Data.SouthEastTNT.Z);
            }
        }

        private double _pearlYCoor;
        public double PearlYCoor
        {
            get => _pearlYCoor;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlYCoor, value);
                DataUpdate(ref _pearlYCoor, ref Data.Pearl.Position.Y);
            }
        }

        private double _pearlYMomentum;
        public double PearlYMomentum
        {
            get => _pearlYMomentum;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlYMomentum, value);
                DataUpdate(ref _pearlYMomentum, ref Data.Pearl.Motion.Y);
            }
        }

        private int _defaultRedDuperIndex;
        public int DefaultRedDuperIndex
        {
            get => _defaultRedDuperIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _defaultRedDuperIndex, value);
                Data.DefaultRedDuper = Enum.Parse<Direction>(((ComboBoxDireEnum) value).ToString());
            }
        }

        private int _defaultBlueDuperIndex;
        public int DefaultBlueDuperIndex
        {
            get => _defaultBlueDuperIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _defaultBlueDuperIndex, value);
                Data.DefaultBlueDuper = Enum.Parse<Direction>(((ComboBoxDireEnum) value).ToString());
            }
        }

        #endregion
        
        #region Console
        
        private string _commandText;
        public string CommandText
        {
            get => _commandText;
            set => this.RaiseAndSetIfChanged(ref _commandText, value);
        }

        private ObservableCollection<ConsoleOutputItemModel>? _consoleOutputs;
        public ObservableCollection<ConsoleOutputItemModel>? ConsoleOutputs
        {
            get => _consoleOutputs;
            set => this.RaiseAndSetIfChanged(ref _consoleOutputs, value);
        }

        #endregion

        public MainWindowViewModel()
        {
            ConsoleOutputs ??= new ObservableCollection<ConsoleOutputItemModel>();
            
            CommandManager.Instance.OnMessageSend += AddConsoleMessage;
            Clear.OnExcute += OnClear;
            
            InitData();
        }

        void InitData()
        {
            _northEastTNTX = Data.NorthEastTNT.X;
            _northEastTNTY = Data.NorthEastTNT.Y;
            _northEastTNTZ = Data.NorthEastTNT.Z;

            _northWestTNTX = Data.NorthWestTNT.X;
            _northWestTNTY = Data.NorthWestTNT.Y;
            _northWestTNTZ = Data.NorthWestTNT.Z;

            _southEastTNTX = Data.SouthEastTNT.X;
            _southEastTNTY = Data.SouthEastTNT.Y;
            _southEastTNTZ = Data.SouthEastTNT.Z;

            _southWestTNTX = Data.SouthWestTNT.X;
            _southWestTNTY = Data.SouthWestTNT.Y;
            _southWestTNTZ = Data.SouthWestTNT.Z;

            _pearlYCoor = Data.Pearl.Position.Y;
            _pearlYMomentum = Data.Pearl.Motion.Y;
            
            DefaultRedDuperIndex = (int)Enum.Parse<ComboBoxDireEnum>(Data.DefaultRedDuper.ToString());
            DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultBlueDuper.ToString());
        }

        void ResetSettings()
        {
            Data.Reset();
            
            NorthWestTNTX = Data.NorthWestTNT.X;
            NorthWestTNTY = Data.NorthWestTNT.Y;
            NorthWestTNTZ = Data.NorthWestTNT.Z;

            NorthEastTNTX = Data.NorthEastTNT.X;
            NorthEastTNTY = Data.NorthEastTNT.Y;
            NorthEastTNTZ = Data.NorthEastTNT.Z;

            SouthWestTNTX = Data.SouthWestTNT.X;
            SouthWestTNTY = Data.SouthWestTNT.Y;
            SouthWestTNTZ = Data.SouthWestTNT.Z;

            SouthEastTNTX = Data.SouthEastTNT.X;
            SouthEastTNTY = Data.SouthEastTNT.Y;
            SouthEastTNTZ = Data.SouthEastTNT.Z;

            
            PearlPosX = Data.Pearl.Position.X;
            PearlYCoor = Data.Pearl.Position.Y;
            PearlPosZ = Data.Pearl.Position.Z;
            PearlYMomentum = Data.Pearl.Motion.Y;
            DestinationX = Data.Destination.X;
            DestinationZ = Data.Destination.Z;
            PearlOffsetX = Data.PearlOffset.X.ToString();
            PearlOffsetZ = Data.PearlOffset.Z.ToString();

            DefaultRedDuperIndex = (int)Enum.Parse<ComboBoxDireEnum>(Data.DefaultRedDuper.ToString());
            DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultBlueDuper.ToString());
        }
        
        ~MainWindowViewModel()
        {
            CommandManager.Instance.OnMessageSend -= AddConsoleMessage;
            Clear.OnExcute -= OnClear;
        }

        private void AddConsoleMessage(ConsoleOutputItemModel message)
        {
            if(ConsoleOutputs.Count >= 500)
                for (int i = 0; i < 50; i++)
                    ConsoleOutputs.RemoveAt(0);
            
            ConsoleOutputs.Add(message);
        }

        private void OnClear()
        {
            ConsoleOutputs.Clear();
        }

        public void LoadDataFormSettings(Settings settings)
        {
            Data.Pearl = settings.Pearl;
            
            Data.Destination = settings.Destination;

            NorthWestTNTX = settings.NorthWestTNT.X;
            NorthWestTNTY = settings.NorthWestTNT.Y;
            NorthWestTNTZ = settings.NorthWestTNT.Z;

            NorthEastTNTX = settings.NorthEastTNT.X;
            NorthEastTNTY = settings.NorthEastTNT.Y;
            NorthEastTNTZ = settings.NorthEastTNT.Z;

            SouthWestTNTX = settings.SouthWestTNT.X;
            SouthWestTNTY = settings.SouthWestTNT.Y;
            SouthWestTNTZ = settings.SouthWestTNT.Z;

            SouthEastTNTX = settings.SouthEastTNT.X;
            SouthEastTNTY = settings.SouthEastTNT.Y;
            SouthEastTNTZ = settings.SouthEastTNT.Z;

            
            PearlPosX = settings.Pearl.Position.X;
            PearlYCoor = settings.Pearl.Position.Y;
            PearlPosZ = settings.Pearl.Position.Z;
            PearlYMomentum = settings.Pearl.Motion.Y;
            DestinationX = settings.Destination.X;
            DestinationZ = settings.Destination.Z;
            PearlOffsetX = settings.Offset.X.ToString();
            PearlOffsetZ = settings.Offset.Z.ToString();
            BlueTNT = (uint)settings.BlueTNT;
            RedTNT = (uint) settings.RedTNT;
            MaxTNT = (uint)settings.MaxTNT;
            Direction = settings.Direction;
        }

        #region Calculate
        
        public void CalculateTNTAmount()
        {
            if (Calculation.CalculateTNTAmount(MaxTicks, 10))
            {
                SortTNTResult();
                ShowTNTAmount(Data.TNTResult);
            }
            IsDisplayTNTAmount = true;
        }

        public void PearlSimulate()
        {
            ShowPearlTrace(Calculation.CalculatePearlTrace((int)RedTNT, (int)BlueTNT, MaxTicks, Direction));
        }
        
        #endregion
        
        #region TNT Result Sort

        private void SortTNTResult()
        {
            switch (TNTWeightMode)
            {
                case TNTWeightModeEnum.DistanceVSTNT:
                    Data.TNTResult.SortByWeightedDistance(new TNTResultSortByWeightedArgs(TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                    break;
                case TNTWeightModeEnum.OnlyTNT:
                    Data.TNTResult.SortByWeightedTotal(new TNTResultSortByWeightedArgs(TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                    break;
                case TNTWeightModeEnum.OnlyDistance:
                    Data.TNTResult.SortByDistance();
                    break;
            }
        }

        public void SortTNTResultByDistance()
        {
            Data.TNTResult.SortByDistance();
            TNTResult = new ObservableCollection<TNTCalculationResult>(Data.TNTResult);
        }

        public void SortTNTResultByTicks()
        {
            Data.TNTResult.SortByTick();
            TNTResult = new ObservableCollection<TNTCalculationResult>(Data.TNTResult);
        }

        public void SortTNTResultByTotal()
        {
            Data.TNTResult.SortByTotal();
            TNTResult = new ObservableCollection<TNTCalculationResult>(Data.TNTResult);
        }
        
        #endregion

        #region Maunally Calcualte

        public void ManuallyCalculateTNTAmount()
        {
            if (ManuallyCalculation.CalculateTNTAmount(ManuallyData.Destination, MaxTicks, out var result))
                ShowTNTAmount(result);
            IsDisplayTNTAmount = true;
        }

        public void ManuallyCalculatePearlTrace()
        {
            ShowPearlTrace(ManuallyCalculation.CalculatePearl(ManuallyData.ATNTAmount, ManuallyData.BTNTAmount, MaxTicks));
            
            ResultDirection = string.Empty;
            ResultAngle = string.Empty;
        }

        public void ManuallyCalculatePearlMomentum()
        {
            var entities = ManuallyCalculation.CalculatePearl(ManuallyData.ATNTAmount, ManuallyData.BTNTAmount, MaxTicks);
            var traces = new List<PearlTraceModel>(entities.Count);
            traces.AddRange(entities.Select((t, i) => new PearlTraceModel {Tick = i, XCoor = t.Motion.X, YCoor = t.Motion.Y, ZCoor = t.Motion.Z}));
            PearlTraceList = traces;

            IsDisplayTNTAmount = false;
            IsDisplayMotion = true;
            ResultDirection = string.Empty;
            ResultAngle = string.Empty;

        }

        #endregion

        #region Result Show
        
        private void ShowPearlTrace(List<Entity> entities)
        {
            var traces = new List<PearlTraceModel>(entities.Count);
            traces.AddRange(entities.Select((t, i) => new PearlTraceModel {Tick = i, XCoor = t.Position.X, YCoor = t.Position.Y, ZCoor = t.Position.Z}));
            PearlTraceList = traces;

            IsDisplayTNTAmount = false;
            IsDisplayMotion = false;
        }

        private void ShowTNTAmount(List<TNTCalculationResult> result)
        {
            TNTResult = new ObservableCollection<TNTCalculationResult>(result);
            var angle = ManuallyData.Pearl.Position.WorldAngle(ManuallyData.Destination);
            ResultDirection = ManuallyData.Pearl.Position.Direction(angle).ToString();
            ResultAngle = angle.ToString();
        }
        
        #endregion
        
        public void SendCmd()
        {
            if(string.IsNullOrEmpty(CommandText) || string.IsNullOrWhiteSpace(CommandText) || CommandText[0] != '/')
                return;

            var cmd = CommandText[1..];
            CommandText = string.Empty;
            
            CommandManager.Instance.ExcuteCommand(cmd);
        }


        private void DataUpdate<T>(ref T vmBacking, ref T dataBacking) where T : IEquatable<T>
        {
            if (!vmBacking.Equals(dataBacking))
                dataBacking = vmBacking;
        }
    }
    
    public enum TNTWeightModeEnum
    {
        DistanceVSTNT, OnlyTNT, OnlyDistance
    }
    
    //I don't know why ComboBox.SelectedItem cause a issue
    //avalonia can't resolve item form string
    //may need to ComboBoxItem?
    //So, i decide use a Enum link Direction Enum to ComboBox.SelectedIndex
    //and selectedIndex form the enum
    //it's value(int) is the same as ComboBoxItem index
    internal enum ComboBoxDireEnum
    {
        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast
    }
}
