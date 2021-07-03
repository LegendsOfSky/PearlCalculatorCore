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
                if (value == _weightMode) return;
                
                this.RaiseAndSetProperty(ref _weightMode, value);
                StaticWeightMode = value;
                EventManager.PublishEvent(this, "tntWeightChanged", 
                    value == TNTWeightModeEnum.Distance ? _distanceModeArgs : _totalModeArgs);

            }
        }


        public static TNTWeightModeEnum StaticWeightMode { get; private set; } = TNTWeightModeEnum.Distance;

        private static TNTWeightChangedArgs _distanceModeArgs = new TNTWeightChangedArgs("GeneralFTL_Advance", TNTWeightModeEnum.Distance);
        private static TNTWeightChangedArgs _totalModeArgs = new TNTWeightChangedArgs("GeneralFTL_Advance", TNTWeightModeEnum.Total);


        private bool _enableChunkMode;
        public bool EnableChunkMode
        {
            get => _enableChunkMode;
            set
            {
                _enableChunkMode = value;
                EventManager.PublishEvent(this, "switchChunkMode", new SwitchChunkModeArgs("OtherSettings", value));
            }
        }
    }
    
    public enum TNTWeightModeEnum
    {
        Distance, Total
    }
}