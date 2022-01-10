using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class AdvanceViewModel : ViewModelBase
    {
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
                if (Data.TNTWeight == value) return;
                Data.TNTWeight = value;
                EventManager.PublishEvent(this, "tntWeightChanged", 
                    WeightMode == TNTWeightModeEnum.Distance ? _distanceModeArgs : _totalModeArgs);
            }
        }

        private TNTWeightModeEnum _weightMode = TNTWeightModeEnum.Distance;
        public TNTWeightModeEnum WeightMode
        {
            get => _weightMode;
            set
            {
                if (value == _weightMode) return;
                
                RaiseAndSetProperty(ref _weightMode, value);
                StaticWeightMode = value;
                EventManager.PublishEvent(this, "tntWeightChanged", 
                    value == TNTWeightModeEnum.Distance ? _distanceModeArgs : _totalModeArgs);

            }
        }


        public static TNTWeightModeEnum StaticWeightMode { get; private set; } = TNTWeightModeEnum.Distance;

        private static TNTWeightChangedArgs _distanceModeArgs = new("GeneralFTL_Advance", TNTWeightModeEnum.Distance);
        private static TNTWeightChangedArgs _totalModeArgs = new("GeneralFTL_Advance", TNTWeightModeEnum.Total);

        public AdvanceViewModel()
        {
            EventManager.AddListener<LoadSettingsArgs>("loadSettings", (sender, args) =>
            {
                TNTWeight = args.Settings.TNTWeight;
                
                PearlOffsetX = args.Settings.CannonSettings[0].Offset.X;
                PearlOffsetZ = args.Settings.CannonSettings[0].Offset.Z;
            });
        }
    }

    public enum TNTWeightModeEnum
    {
        Distance, Total
    }
}