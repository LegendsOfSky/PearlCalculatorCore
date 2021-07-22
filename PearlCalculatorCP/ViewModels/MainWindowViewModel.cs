using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using PearlCalculatorCP.Models;
using PearlCalculatorCP.Localizer;
using PearlCalculatorCP.Views;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Utility;
using PearlCalculatorLib.PearlCalculationLib.World;
using ReactiveUI;

using ManuallyCalculation = PearlCalculatorLib.Manually.Calculation;
using ManuallyData = PearlCalculatorLib.Manually.Data;

namespace PearlCalculatorCP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Window Scale

        private static readonly Size DefaultWindowSize = new Size(1000, 800);
        
        private Size _windowSize = DefaultWindowSize;
        public Size WindowSize
        {
            get => _windowSize;
            private set => this.RaiseAndSetIfChanged(ref _windowSize, value);
        }

        private double _windowScale = 1.0d;
        public double WindowScale
        {
            get => _windowScale;
            private set
            {
                this.RaiseAndSetIfChanged(ref _windowScale, value);
                WindowSize = DefaultWindowSize * value;
            }
        }

        #endregion
        
        
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
            EventManager.AddListener<SetRTCountArgs>("setRTCount", (sender, args) =>
            {
                RedTNT = (uint) args.Red;
                BlueTNT = (uint) args.Blue;
                Direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination));
            });
            
            EventManager.AddListener<NotificationArgs>("resetSettings", (sender, args) =>
            {
                _isEnableIfChanged = false;
                
                PearlPosX      = Data.Pearl.Position.X;
                PearlPosZ      = Data.Pearl.Position.Z;
                DestinationX   = Data.Destination.X;
                DestinationZ   = Data.Destination.Z;
                
                _isEnableIfChanged = true;
            });
        }

        public MainWindowViewModel(ref Action onStartupCompleted) : this()
        {
            onStartupCompleted = () =>
            {
                if (AppRuntimeSettings.Settings.TryGetValue("scale", out var s))
                    WindowScale = (double)s;
            };
        }
        

        public void LoadDataFormSettings(Settings settings)
        {
            Data.Pearl = settings.Pearl;
            
            Data.Destination = settings.Destination;

            PearlPosX      = settings.Pearl.Position.X;
            PearlPosZ      = settings.Pearl.Position.Z;
            DestinationX   = settings.Destination.X;
            DestinationZ   = settings.Destination.Z;
            BlueTNT        = (uint) settings.BlueTNT;
            RedTNT         = (uint) settings.RedTNT;
            MaxTNT         = (uint) settings.MaxTNT;
            Direction      = settings.Direction;
            
            EventManager.PublishEvent(this, "loadSettings", new LoadSettingsArgs("LoadSettings", settings));
            
            ShowDirectionResult(Data.Pearl.Position, Data.Destination);
            if (RedTNT > 0 || BlueTNT > 0)
            {
                PearlSimulate();
            }
        }

        #region Calculate
        
        public void CalculateTNTAmount()
        {
            if (Calculation.CalculateTNTAmount(MaxTicks, 10))
            {
                EventManager.PublishEvent(this, "calculate", new CalculateTNTAmountArgs("GeneralFTL", Data.TNTResult));
                ShowDirectionResult(Data.Pearl.Position, Data.Destination);
            }
        }

        public void PearlSimulate()
        {
            var entities = Calculation.CalculatePearlTrace((int)RedTNT, (int)BlueTNT, MaxTicks, Direction);
            var chunks = ListCoverterUtility.ToChunk(entities);
            
            var traces = new List<PearlTraceModel>(entities.Count);
            var chunkModels = new List<PearlTraceChunkModel>(chunks.Count);
            
            traces.AddRange(entities.Select((t, i) => new PearlTraceModel {Tick = i, XCoor = t.Position.X, YCoor = t.Position.Y, ZCoor = t.Position.Z}));
            chunkModels.AddRange(chunks.Select((c, i) => new PearlTraceChunkModel{Tick = i, XCoor = c.X, ZCoor = c.Z}));
            EventManager.PublishEvent(this, "pearlTrace", new PearlSimulateArgs("GeneralFTL", traces, chunkModels));
        }
        
        #endregion

        #region Result Show

        private void ShowDirectionResult(Space3D pearlPos, Space3D destination)
        {
            var angle = pearlPos.WorldAngle(destination);
            var direction = pearlPos.Direction(angle).ToString();
            EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs("GeneralFTL", direction, angle.ToString()));
        }
        
        #endregion

        public void ChangeLanguageOptional(string lang)
        {
            CommandManager.Instance.ExecuteCommand(
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
