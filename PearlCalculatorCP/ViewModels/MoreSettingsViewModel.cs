using System;
using PearlCalculatorCP.Utils;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class MoreSettingsViewModel : ViewModelBase
    {
        private bool _isEnableIfChanged = true;

        private double _northWestTNTX;
        public double NorthWestTNTX
        {
            get => _northWestTNTX;
            set
            {
                RaiseAndSetOrIfChanged(ref _northWestTNTX, ref value, _isEnableIfChanged);
                UpdateNorthWestTNT();
            }
        }

        private double _northWestTNTY;
        public double NorthWestTNTY
        {
            get => _northWestTNTY;
            set
            {
                RaiseAndSetOrIfChanged(ref _northWestTNTY, ref value, _isEnableIfChanged);
                UpdateNorthWestTNT();
            }
        }

        private double _northWestTNTZ;
        public double NorthWestTNTZ
        {
            get => _northWestTNTZ;
            set
            {
                RaiseAndSetOrIfChanged(ref _northWestTNTZ, ref value, _isEnableIfChanged);
                UpdateNorthWestTNT();
            }
        }

        private double _northEastTNTX;
        public double NorthEastTNTX
        {
            get => _northEastTNTX;
            set
            {
                RaiseAndSetOrIfChanged(ref _northEastTNTX, ref value, _isEnableIfChanged);
                UpdateNorthEastTNT();
            }
        }

        private double _northEastTNTY;
        public double NorthEastTNTY
        {
            get => _northEastTNTY;
            set
            {
                RaiseAndSetOrIfChanged(ref _northEastTNTY, ref value, _isEnableIfChanged);
                UpdateNorthEastTNT();
            }
        }

        private double _northEastTNTZ;
        public double NorthEastTNTZ
        {
            get => _northEastTNTZ;
            set
            {
                RaiseAndSetOrIfChanged(ref _northEastTNTZ, ref value, _isEnableIfChanged);
                UpdateNorthEastTNT();
            }
        }

        private double _southWestTNTX;
        public double SouthWestTNTX
        {
            get => _southWestTNTX;
            set
            {
                RaiseAndSetOrIfChanged(ref _southWestTNTX, ref value, _isEnableIfChanged);
                UpdateSouthWestTNT();
            }
        }

        private double _southWestTNTY;
        public double SouthWestTNTY
        {
            get => _southWestTNTY;
            set
            {
                RaiseAndSetOrIfChanged(ref _southWestTNTY, ref value, _isEnableIfChanged);
                UpdateSouthWestTNT();
            }
        }

        private double _southWestTNTZ;
        public double SouthWestTNTZ
        {
            get => _southWestTNTZ;
            set
            {
                RaiseAndSetOrIfChanged(ref _southWestTNTZ, ref value, _isEnableIfChanged);
                UpdateSouthWestTNT();
            }
        }

        private double _southEastTNTX;
        public double SouthEastTNTX
        {
            get => _southEastTNTX;
            set
            {
                RaiseAndSetOrIfChanged(ref _southEastTNTX, ref value, _isEnableIfChanged);
                UpdateSouthEastTNT();
            }
        }

        private double _southEastTNTY;
        public double SouthEastTNTY
        {
            get => _southEastTNTY;
            set
            {
                RaiseAndSetOrIfChanged(ref _southEastTNTY, ref value, _isEnableIfChanged);
                UpdateSouthEastTNT();
            }
        }

        private double _southEastTNTZ;
        public double SouthEastTNTZ
        {
            get => _southEastTNTZ;
            set
            {
                RaiseAndSetOrIfChanged(ref _southEastTNTZ, ref value, _isEnableIfChanged);
                UpdateSouthEastTNT();
            }
        }

        private double _pearlYCoor;
        public double PearlYCoor
        {
            get => _pearlYCoor;
            set
            {
                RaiseAndSetOrIfChanged(ref _pearlYCoor, ref value, _isEnableIfChanged);
                Data.Pearl.Position.Y = _pearlYCoor;
            }
        }

        private double _pearlYMomentum;
        public double PearlYMomentum
        {
            get => _pearlYMomentum;
            set
            {
                RaiseAndSetOrIfChanged(ref _pearlYMomentum, ref value, _isEnableIfChanged);
                Data.Pearl.Motion.Y = _pearlYMomentum;
            }
        }

        public Direction[] CannonDuperDirections { get; } = { Direction.NorthWest, Direction.NorthEast, Direction.SouthWest, Direction.SouthEast };

        private Direction _defaultRedDuper;
        public Direction DefaultRedDuper
        {
            get => _defaultRedDuper;
            set
            {
                Data.DefaultRedDuper = value;
                RaiseAndSetProperty(ref _defaultRedDuper, Data.DefaultRedDuper);
            }
        }

        private Direction _defaultBlueDuper;
        public Direction DefaultBlueDuper
        {
            get => _defaultBlueDuper;
            set
            {
                Data.DefaultBlueDuper = value;
                RaiseAndSetProperty(ref _defaultBlueDuper, Data.DefaultBlueDuper);
            }
        }

        public MoreSettingsViewModel()
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
            
            DefaultRedDuper  = Data.DefaultRedDuper;
            DefaultBlueDuper = Data.DefaultBlueDuper;
            
            EventManager.AddListener<LoadSettingsArgs>("loadSettings", (_, args) =>
            {
                var settings = args.Settings.CannonSettings[0];

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
                
                PearlYCoor     = settings.Pearl.Position.Y;
                PearlYMomentum = settings.Pearl.Motion.Y;
                
                DefaultRedDuper  = settings.DefaultRedDirection;
                DefaultBlueDuper = settings.DefaultBlueDirection;
            });
            
            EventManager.AddListener<NotificationArgs>("changeDefaultBlueDuper", (_, _) =>
            {
                var red = Data.DefaultRedDuper;
                DefaultBlueDuper = (red.IsNorth() ? Direction.South : Direction.North) |
                                   (red.IsEast() ? Direction.West : Direction.East);
                LogUtils.Log($"change DefaultBlueDuper to {DefaultBlueDuper.ToString()}");
            });
        }

        public void ResetSettings()
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
            
            PearlYCoor     = Data.Pearl.Position.Y;
            PearlYMomentum = Data.Pearl.Motion.Y;
            
            DefaultRedDuper  = Data.DefaultRedDuper;
            DefaultBlueDuper = Data.DefaultBlueDuper;
            
            _isEnableIfChanged = true;
            
            EventManager.PublishEvent(this, "resetSettings", new NotificationArgs("GeneralData"));
        }
        
        private void UpdateNorthWestTNT()
        {
            Data.NorthWestTNT = new(NorthWestTNTX, NorthWestTNTY, NorthWestTNTZ);
        }
        
        private void UpdateNorthEastTNT()
        {
            Data.NorthEastTNT = new(NorthEastTNTX, NorthEastTNTY, NorthEastTNTZ);
        }
        
        private void UpdateSouthWestTNT()
        {
            Data.SouthWestTNT = new(SouthWestTNTX, SouthWestTNTY, SouthWestTNTZ);
        }
        
        private void UpdateSouthEastTNT()
        {
            Data.SouthEastTNT = new(SouthEastTNTX, SouthEastTNTY, SouthEastTNTZ);
        }
    }
}