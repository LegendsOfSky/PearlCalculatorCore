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
            
            DefaultRedDuperIndex  = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultRedDuper.ToString());
            DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultBlueDuper.ToString());
            
            EventManager.AddListener<LoadSettingsArgs>("loadSettings", (sender, args) =>
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
                
                DefaultRedDuperIndex  = (int) Enum.Parse<ComboBoxDireEnum>(settings.DefaultRedDirection.ToString());
                DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(settings.DefaultBlueDirection.ToString());
            });
            
            EventManager.AddListener<NotificationArgs>("changeDefaultBlueDuper", (sender, args) =>
            {
                var red = Data.DefaultRedDuper;
                var direction = (red.IsNorth() ? Direction.South : Direction.North) |
                                (red.IsEast() ? Direction.West : Direction.East);
                DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(direction.ToString());
                LogUtils.Log($"change DefaultBlueDuper to {direction.ToString()}");
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
            
            DefaultRedDuperIndex  = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultRedDuper.ToString());
            DefaultBlueDuperIndex = (int) Enum.Parse<ComboBoxDireEnum>(Data.DefaultBlueDuper.ToString());
            
            _isEnableIfChanged = true;
            
            EventManager.PublishEvent(this, "resetSettings", new NotificationArgs("GeneralData"));
        }
        
        private void DataUpdate<T>(ref T vmBacking, ref T dataBacking) where T : IEquatable<T>
        {
            if (!vmBacking.Equals(dataBacking))
                dataBacking = vmBacking;
        }
    }
}