using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using PearlCalculatorCP.Models;
using PearlCalculatorCP.Utils;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Utility;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Settings;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IWindowViewModelScale
    {

        private static readonly Size DefaultWindowSize = new(1000, 800);
        private const double DefaultSettingsPopupWidth = 350;

        private Size _windowSize = DefaultWindowSize;
        public Size WindowSize
        {
            get => _windowSize;
            private set => this.RaiseAndSetIfChanged(ref _windowSize, value);
        }

        private double _settingsPopupWidth = DefaultSettingsPopupWidth;
        public double SettingsPopupWidth
        {
            get => _settingsPopupWidth;
            private set => this.RaiseAndSetIfChanged(ref _settingsPopupWidth, value);
        }
        
        private double _windowScale = 1.0d;
        public double WindowScale
        {
            get => _windowScale;
            set
            {
                if (value <= 0 || _windowScale == value)
                    return;
                
                RaiseAndSetProperty(ref _windowScale, value);
                WindowSize = DefaultWindowSize * value;
                SettingsPopupWidth = DefaultSettingsPopupWidth * value;
            }
        }
        
        
        //this field for RaiseAndSetIfChanged()
        private bool _isEnableIfChanged = true;

        public static int MaxTicks { get; set; } = 100;
        public static double MaxDistance { get; set; } = 10;

#region GeneralFTL General Input Data
        
        private double _pearlPosX;
        public double PearlPosX
        {
            get => _pearlPosX;
            set
            {
                RaiseAndSetOrIfChanged(ref _pearlPosX, value, _isEnableIfChanged);
                Data.Pearl.Position.X = _pearlPosX;
            }
        }

        private double _pearlPosZ;
        public double PearlPosZ
        {
            get => _pearlPosZ;
            set
            {
                RaiseAndSetOrIfChanged(ref _pearlPosZ, value, _isEnableIfChanged);
                Data.Pearl.Position.Z = _pearlPosZ;
            }
        }

        private double _destinationX;
        public double DestinationX
        {
            get => _destinationX;
            set
            {
                RaiseAndSetOrIfChanged(ref _destinationX, value, _isEnableIfChanged);
                var des = Data.Destination;
                des.X = _destinationX;
                Data.Destination = des;
            }
        }

        private double _destinationZ;
        public double DestinationZ
        {
            get => _destinationZ;
            set
            {
                RaiseAndSetOrIfChanged(ref _destinationZ, value, _isEnableIfChanged);
                var des = Data.Destination;
                des.Z = _destinationZ;
                Data.Destination = des;
            }
        }

        private uint _maxTNT;
        public uint MaxTNT
        {
            get => _maxTNT;
            set
            {
                RaiseAndSetOrIfChanged(ref _maxTNT, value, _isEnableIfChanged);
                Data.MaxTNT = (int)_maxTNT;
            }
        }

        private Direction _direction;
        public Direction Direction
        {
            get => _direction;
            set
            {
                RaiseAndSetOrIfChanged(ref _direction, value, _isEnableIfChanged);
                Data.Direction = _direction;
            }
        }

        private uint _redTNT;
        public uint RedTNT
        {
            get => _redTNT;
            set
            {
                RaiseAndSetOrIfChanged(ref _redTNT, value, _isEnableIfChanged);
                Data.RedTNT = (int)_redTNT;
            }
        }

        private uint _blueTNT;
        public uint BlueTNT
        {
            get => _blueTNT;
            set
            {
                RaiseAndSetOrIfChanged(ref _blueTNT, value, _isEnableIfChanged);
                Data.BlueTNT = (int)_blueTNT;
            }
        }

#endregion

        private ObservableCollection<string> _configSelectList = new();

        public ObservableCollection<string> ConfigSelectList
        {
            get => _configSelectList;
            set => this.RaiseAndSetIfChanged(ref _configSelectList, value);
        }

        public MainWindowViewModel()
        {
            _direction = Data.Direction;
            
            EventManager.AddListener<SetRTCountArgs>("setRTCount", (_, args) =>
            {
                RedTNT = (uint) args.Red;
                BlueTNT = (uint) args.Blue;
                Direction = DirectionUtils.GetDirection(Data.Pearl.Position.WorldAngle(Data.Destination));
            });
            
            EventManager.AddListener<NotificationArgs>("resetSettings", (_, _) =>
            {
                _isEnableIfChanged = false;
                
                PearlPosX      = Data.Pearl.Position.X;
                PearlPosZ      = Data.Pearl.Position.Z;
                DestinationX   = Data.Destination.X;
                DestinationZ   = Data.Destination.Z;
                Direction      = Data.Direction;
                
                _isEnableIfChanged = true;
            });

            for (int i = 0; i < 10; i++)
            {
                _configSelectList.Add($"Default ({i.ToString()})");
            }
            
            this.ApplyScale();
        }

        public void LoadDataFormSettings(SettingsCollection settings)
        {
            var cannon = settings.CannonSettings[0];
            
            Data.Pearl = cannon.Pearl;
            
            Data.Destination = settings.Destination.ToSpace3D();

            PearlPosX      = cannon.Pearl.Position.X;
            PearlPosZ      = cannon.Pearl.Position.Z;
            DestinationX   = settings.Destination.X;
            DestinationZ   = settings.Destination.Z;
            BlueTNT        = (uint) settings.BlueTNT;
            RedTNT         = (uint) settings.RedTNT;
            MaxTNT         = (uint) cannon.MaxTNT;
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
            try
            {
                if (Calculation.CalculateTNTAmount(MaxTicks, MaxDistance))
                {
                    EventManager.PublishEvent(this, "calculate", new CalculateTNTAmountArgs("GeneralFTL", Data.TNTResult));
                    ShowDirectionResult(Data.Pearl.Position, Data.Destination);
                }
                else
                {
                    EventManager.PublishEvent(this, "calculate", new CalculateTNTAmountArgs("GeneralFTL", null));
                    EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs("GeneralFTL", string.Empty, string.Empty));
                }
            }
            catch (ArgumentException e)
            {
                LogUtils.Error(e.Message);
                EventManager.PublishEvent(this, "changeDefaultBlueDuper", new NotificationArgs("GeneralFTL"));
            }
            catch (Exception e)
            {
                LogUtils.Error(e.Message);
            }
        }

        public void PearlSimulate()
        {
            try
            {
                var entities = Calculation.CalculatePearlTrace((int)RedTNT, (int)BlueTNT, MaxTicks, Direction);
                var chunks = ListCoverterUtility.ToChunk(entities);
            
                var traces = new List<PearlTraceModel>(entities.Count);
                var chunkModels = new List<PearlTraceChunkModel>(chunks.Count);
            
                traces.AddRange(entities.Select((t, i) => new PearlTraceModel {Tick = i, XCoor = t.Position.X, YCoor = t.Position.Y, ZCoor = t.Position.Z}));
                chunkModels.AddRange(chunks.Select((c, i) => new PearlTraceChunkModel{Tick = i, XCoor = c.X, ZCoor = c.Z}));
                EventManager.PublishEvent(this, "pearlTrace", new PearlSimulateArgs("GeneralFTL", traces, chunkModels));
            }
            catch (ArgumentException e)
            {
                LogUtils.Error(e.Message);
                EventManager.PublishEvent(this, "changeDefaultBlueDuper", new NotificationArgs("GeneralFTL"));
            }
            catch (Exception e)
            {
                LogUtils.Error(e.Message);
            }
        }
        
        #endregion

        #region Result Show

        private void ShowDirectionResult(Space3D pearlPos, Space3D destination)
        {
            var angle = pearlPos.WorldAngle(destination);
            var direction = DirectionUtils.GetDirection(angle).ToString();
            EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs("GeneralFTL", direction, angle.ToString()));
        }
        
        #endregion
    }
}
