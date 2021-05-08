using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using PearlCalculatorCP.Models;
using PearlCalculatorCP.Commands;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.Views;
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
        //this field for RaiseAndSetIfChanged()
        private bool _isEnableIfChanged = true;

        public static int MaxTicks { get; set; } = 100;

        #region GeneralFTL General Input Data
        
        private double _pearlPosX;
        public double PearlPosX
        {
            get => _pearlPosX;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _pearlPosX, ref value, _isEnableIfChanged);
                DataUpdate(ref _pearlPosX, ref Data.Pearl.Position.X);
            }
        }

        private double _pearlPosZ;
        public double PearlPosZ
        {
            get => _pearlPosZ;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _pearlPosZ, ref value, _isEnableIfChanged);
                DataUpdate(ref _pearlPosZ, ref Data.Pearl.Position.Z);
            }
        }

        private double _destinationX;
        public double DestinationX
        {
            get => _destinationX;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _destinationX, ref value, _isEnableIfChanged);
                DataUpdate(ref _destinationX, ref Data.Destination.X);
            }
        }

        private double _destinationZ;
        public double DestinationZ
        {
            get => _destinationZ;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _destinationZ, ref value, _isEnableIfChanged);
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

        private double _pearlOffsetX;
        public double PearlOffsetX
        {
            get => _pearlOffsetX;
            set
            {
                Data.PearlOffset = new Surface2D(value, _pearlOffsetZ);
                RaiseAndSetProperty(ref _pearlOffsetX, Data.PearlOffset.X);
            }
        }

        private double _pearlOffsetZ;
        public double PearlOffsetZ
        {
            get => _pearlOffsetZ;
            set
            {
                Data.PearlOffset = new Surface2D(_pearlOffsetX, value);
                RaiseAndSetProperty(ref _pearlOffsetZ, Data.PearlOffset.Z);
            }
        }
        
        public int TNTWeight
        {
            get => Data.TNTWeight;
            set
            {
                this.RaiseAndSetIfChanged(ref Data.TNTWeight, value);
                if (IsDisplayTNTAmount)
                {
                    SortTNTResult();
                    ShowTNTAmount(Data.TNTResult, Data.Pearl.Position, Data.Destination);
                }
            }
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
#nullable disable
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
#nullable enable
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
                this.RaiseAndSetOrIfChanged(ref _northWestTNTX, ref value, _isEnableIfChanged);
                DataUpdate(ref _northWestTNTX, ref Data.NorthWestTNT.X);
            }
        }

        private double _northWestTNTY;
        public double NorthWestTNTY
        {
            get => _northWestTNTY;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _northWestTNTY, ref value, _isEnableIfChanged);
                DataUpdate(ref _northWestTNTY, ref Data.NorthWestTNT.Y);
            }
        }

        private double _northWestTNTZ;
        public double NorthWestTNTZ
        {
            get => _northWestTNTZ;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _northWestTNTZ, ref value, _isEnableIfChanged);
                DataUpdate(ref _northWestTNTZ, ref Data.NorthWestTNT.Z);
            }
        }

        private double _northEastTNTX;
        public double NorthEastTNTX
        {
            get => _northEastTNTX;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _northEastTNTX, ref value, _isEnableIfChanged);
                DataUpdate(ref _northEastTNTX, ref Data.NorthEastTNT.X);
            }
        }

        private double _northEastTNTY;
        public double NorthEastTNTY
        {
            get => _northEastTNTY;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _northEastTNTY, ref value, _isEnableIfChanged);
                DataUpdate(ref _northEastTNTY, ref Data.NorthEastTNT.Y);
            }
        }

        private double _northEastTNTZ;
        public double NorthEastTNTZ
        {
            get => _northEastTNTZ;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _northEastTNTZ, ref value, _isEnableIfChanged);
                DataUpdate(ref _northEastTNTZ, ref Data.NorthEastTNT.Z);
            }
        }

        private double _southWestTNTX;
        public double SouthWestTNTX
        {
            get => _southWestTNTX;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _southWestTNTX, ref value, _isEnableIfChanged);
                DataUpdate(ref _southWestTNTX, ref Data.SouthWestTNT.X);
            }
        }

        private double _southWestTNTY;
        public double SouthWestTNTY
        {
            get => _southWestTNTY;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _southWestTNTY, ref value, _isEnableIfChanged);
                DataUpdate(ref _southWestTNTY, ref Data.SouthWestTNT.Y);
            }
        }

        private double _southWestTNTZ;
        public double SouthWestTNTZ
        {
            get => _southWestTNTZ;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _southWestTNTZ, ref value, _isEnableIfChanged);
                DataUpdate(ref _southWestTNTZ, ref Data.SouthWestTNT.Z);
            }
        }

        private double _southEastTNTX;
        public double SouthEastTNTX
        {
            get => _southEastTNTX;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _southEastTNTX, ref value, _isEnableIfChanged);
                DataUpdate(ref _southEastTNTX, ref Data.SouthEastTNT.X);
            }
        }

        private double _southEastTNTY;
        public double SouthEastTNTY
        {
            get => _southEastTNTY;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _southEastTNTY, ref value, _isEnableIfChanged);
                DataUpdate(ref _southEastTNTY, ref Data.SouthEastTNT.Y);
            }
        }

        private double _southEastTNTZ;
        public double SouthEastTNTZ
        {
            get => _southEastTNTZ;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _southEastTNTZ, ref value, _isEnableIfChanged);
                DataUpdate(ref _southEastTNTZ, ref Data.SouthEastTNT.Z);
            }
        }

        private double _pearlYCoor;
        public double PearlYCoor
        {
            get => _pearlYCoor;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _pearlYCoor, ref value, _isEnableIfChanged);
                DataUpdate(ref _pearlYCoor, ref Data.Pearl.Position.Y);
            }
        }

        private double _pearlYMomentum;
        public double PearlYMomentum
        {
            get => _pearlYMomentum;
            set
            {
                this.RaiseAndSetOrIfChanged(ref _pearlYMomentum, ref value, _isEnableIfChanged);
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
        
        private string _commandText = string.Empty;
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

        private IBrush _moreInfoBrush = MainWindowMoreInfoColor.DefaultMoreInfoBrush;
        public IBrush MoreInfoBrush
        {
            get => _moreInfoBrush;
            set
            {
                this.RaisePropertyChanging();
                _moreInfoBrush = value;
                this.RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            ConsoleOutputs ??= new ObservableCollection<ConsoleOutputItemModel>();
            
            CommandManager.Instance.OnMessageSend += AddConsoleMessage;
            Clear.OnExcute += ClearConsole;
            
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

            _pearlYCoor     = Data.Pearl.Position.Y;
            _pearlYMomentum = Data.Pearl.Motion.Y;
            
            DefaultRedDuperIndex  = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultRedDuper.ToString());
            DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultBlueDuper.ToString());
        }

        void ResetSettings()
        {
            Data.Reset();
            _isEnableIfChanged = false;
            
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

            
            PearlPosX      = Data.Pearl.Position.X;
            PearlYCoor     = Data.Pearl.Position.Y;
            PearlPosZ      = Data.Pearl.Position.Z;
            PearlYMomentum = Data.Pearl.Motion.Y;
            DestinationX   = Data.Destination.X;
            DestinationZ   = Data.Destination.Z;
            PearlOffsetX   = Data.PearlOffset.X;
            PearlOffsetZ   = Data.PearlOffset.Z;

            DefaultRedDuperIndex  = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultRedDuper.ToString());
            DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultBlueDuper.ToString());

            _isEnableIfChanged = true;
        }
        
        ~MainWindowViewModel()
        {
            CommandManager.Instance.OnMessageSend -= AddConsoleMessage;
            Clear.OnExcute -= ClearConsole;
        }

#nullable disable
        private void AddConsoleMessage(ConsoleOutputItemModel message)
        {
            if(ConsoleOutputs.Count >= 500)
                for (int i = 0; i < 50; i++)
                    ConsoleOutputs.RemoveAt(0);
            
            ConsoleOutputs.Add(message);
        }

        private void ClearConsole() => ConsoleOutputs?.Clear();
#nullable enable

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

            
            PearlPosX      = settings.Pearl.Position.X;
            PearlYCoor     = settings.Pearl.Position.Y;
            PearlPosZ      = settings.Pearl.Position.Z;
            PearlYMomentum = settings.Pearl.Motion.Y;
            DestinationX   = settings.Destination.X;
            DestinationZ   = settings.Destination.Z;
            PearlOffsetX   = settings.Offset.X;
            PearlOffsetZ   = settings.Offset.Z;
            BlueTNT        = (uint) settings.BlueTNT;
            RedTNT         = (uint) settings.RedTNT;
            MaxTNT         = (uint) settings.MaxTNT;
            Direction      = settings.Direction;
            
            ShowDirectionResult(Data.Pearl.Position, Data.Destination);
            PearlSimulate();
        }

        #region Calculate
        
        public void CalculateTNTAmount()
        {
            if (Calculation.CalculateTNTAmount(MaxTicks, 10))
            {
                SortTNTResult();
                ShowTNTAmount(Data.TNTResult, Data.Pearl.Position, Data.Destination);
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
                case TNTWeightModeEnum.MixedWeight:
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
                ShowTNTAmount(result, ManuallyData.Pearl.Position, ManuallyData.Destination.ToSpace3D());
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

        private void ShowTNTAmount(List<TNTCalculationResult> result, Space3D pearlPos, Space3D destination)
        {
            TNTResult = new ObservableCollection<TNTCalculationResult>(result);
            ShowDirectionResult(pearlPos, destination);
        }

        private void ShowDirectionResult(Space3D pearlPos, Space3D destination)
        {
            var angle = pearlPos.WorldAngle(destination);
            ResultDirection = pearlPos.Direction(angle).ToString();
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

        public void ChangeLanguageOptional(string lang)
        {
            CommandManager.Instance.ExcuteCommand(
                Translator.Instance.CurrentLanguage == lang
                ? $"setDefaultLang {lang}"
                : $"changeLang {lang}");
        }

        private void DataUpdate<T>(ref T vmBacking, ref T dataBacking) where T : IEquatable<T>
        {
            if (!vmBacking.Equals(dataBacking))
                dataBacking = vmBacking;
        }
    }
    
    public enum TNTWeightModeEnum
    {
        MixedWeight, OnlyTNT, OnlyDistance
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
