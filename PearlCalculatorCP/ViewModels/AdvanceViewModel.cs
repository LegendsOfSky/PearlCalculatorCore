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
                this.RaiseAndSetIfChanged(ref Data.TNTWeight, value);
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
                this.RaiseAndSetIfChanged(ref _weightMode, value);
                StaticWeightMode = value;
            }
        }


        public static TNTWeightModeEnum StaticWeightMode { get; private set; } = TNTWeightModeEnum.Distance;

        private static TNTWeightChangedArgs _distanceModeArgs = new TNTWeightChangedArgs("GeneralFTL_Advance", TNTWeightModeEnum.Distance);
        private static TNTWeightChangedArgs _totalModeArgs = new TNTWeightChangedArgs("GeneralFTL_Advance", TNTWeightModeEnum.Total);
    }
    
    public enum TNTWeightModeEnum
    {
        Distance, Total
    }
}