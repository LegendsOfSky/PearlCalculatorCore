using System.Collections.Generic;
using System.Linq;
using PearlCalculatorCP.Models;
using ReactiveUI;
using PearlCalculatorLib.Manually;
using PearlCalculatorLib.Result;

namespace PearlCalculatorCP.ViewModels
{
    public class ManuallyViewModel : ViewModelBase
    {
        private double _pearlX;
        public double PearlX
        {
            get => _pearlX;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlX, value);
                if (Data.Pearl.Position.X != _pearlX)
                    Data.Pearl.Position.X = _pearlX;
            }
        }

        private double _pearlY;
        public double PearlY
        {
            get => _pearlY;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlY, value);
                if (Data.Pearl.Position.Y != _pearlY)
                    Data.Pearl.Position.Y = _pearlY;
            }
        }
        
        private double _pearlZ;
        public double PearlZ
        {
            get => _pearlZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _pearlZ, value);
                if (Data.Pearl.Position.Z != _pearlZ)
                    Data.Pearl.Position.Z = _pearlZ;
            }
        }

        private double _momentumX;
        public double MomentumX
        {
            get => _momentumX;
            set
            {
                this.RaiseAndSetIfChanged(ref _momentumX, value);
                if (Data.Pearl.Motion.X != _momentumX)
                    Data.Pearl.Motion.X = _momentumX;
            }
        }
        
        private double _momentumY;
        public double MomentumY
        {
            get => _momentumY;
            set
            {
                this.RaiseAndSetIfChanged(ref _momentumY, value);
                if (Data.Pearl.Motion.Y != _momentumY)
                    Data.Pearl.Motion.Y = _momentumY;
            }
        }

        private double _momentumZ;
        public double MomentumZ
        {
            get => _momentumZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _momentumZ, value);
                if (Data.Pearl.Motion.Z != _momentumZ)
                    Data.Pearl.Motion.Z = _momentumZ;
            }
        }

        private double _aTX;
        public double ATX
        {
            get => _aTX;
            set
            {
                this.RaiseAndSetIfChanged(ref _aTX, value);
                if (Data.ATNT.X != _aTX)
                    Data.ATNT.X = _aTX;
            }
        }

        private double _aTY;
        public double ATY
        {
            get => _aTY;
            set
            {
                this.RaiseAndSetIfChanged(ref _aTY, value);
                if (Data.ATNT.Y != _aTY)
                    Data.ATNT.Y = _aTY;
            }
        }

        private double _aTZ;
        public double ATZ
        {
            get => _aTZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _aTZ, value);
                if (Data.ATNT.Z != _aTZ)
                    Data.ATNT.Z = _aTZ;
            }
        }

        private double _bTX;
        public double BTX
        {
            get => _bTX;
            set
            {
                this.RaiseAndSetIfChanged(ref _bTX, value);
                if (Data.BTNT.X != _bTX)
                    Data.BTNT.X = _bTX;
            }
        }
        
        private double _bTY;
        public double BTY
        {
            get => _bTY;
            set
            {
                this.RaiseAndSetIfChanged(ref _bTY, value);
                if (Data.BTNT.Y != _bTY)
                    Data.BTNT.Y = _bTY;
            }
        }
        
        private double _bTZ;
        public double BTZ
        {
            get => _bTZ;
            set
            {
                this.RaiseAndSetIfChanged(ref _bTZ, value);
                if (Data.BTNT.Z != _bTZ)
                    Data.BTNT.Z = _bTZ;
            }
        }
        

        private int _aTNTAmount;
        public int ATNTAmount
        {
            get => _aTNTAmount;
            set
            {
                this.RaiseAndSetIfChanged(ref _aTNTAmount, value);
                if (Data.ATNTAmount != _aTNTAmount)
                    Data.ATNTAmount = _aTNTAmount;
            }
        }
        
        private int _bTNTAmount;
        public int BTNTAmount
        {
            get => _bTNTAmount;
            set
            {
                this.RaiseAndSetIfChanged(ref _bTNTAmount, value);
                if (Data.BTNTAmount != _bTNTAmount)
                    Data.BTNTAmount = _bTNTAmount;
            }
        }

        private double _destinationX;
        public double DestinationX
        {
            get => _destinationX;
            set
            {
                this.RaiseAndSetIfChanged(ref _destinationX, value);
                if (Data.Destination.X != _destinationX)
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
                if (Data.Destination.Z != _destinationZ)
                    Data.Destination.Z = _destinationZ;
            }
        }

        public void CalculateAmount()
        {
            if (Calculation.CalculateTNTAmount(Data.Destination, MainWindowViewModel.MaxTicks, out var result))
            {
                EventManager.PublishEvent(this, "calculate", new CalculateTNTAmountArgs("Manually", result));
                var angle = Data.Pearl.Position.WorldAngle(Data.Destination.ToSpace3D());
                var direction = Data.Pearl.Position.Direction(angle).ToString();
                EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs("Manually", direction, angle.ToString()));
            }
        }

        public void CalculateTrace()
        {
            var entities = Calculation.CalculatePearl(ATNTAmount, BTNTAmount, MainWindowViewModel.MaxTicks);
            var traces = new List<PearlTraceModel>(entities.Count);
            traces.AddRange(entities.Select((t, i) => new PearlTraceModel 
            {
                Tick = i, 
                XCoor = t.Position.X, 
                YCoor = t.Position.Y, 
                ZCoor = t.Position.Z
            }));
            EventManager.PublishEvent(this, "pearlTrace", new PearlSimulateArgs("Manually", traces));
            EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs("Manually", string.Empty, string.Empty));
        }

        public void CalculateMomentum()
        {
            var entities = Calculation.CalculatePearl(ATNTAmount, BTNTAmount, MainWindowViewModel.MaxTicks);
            var traces = new List<PearlTraceModel>(entities.Count);
            traces.AddRange(entities.Select((t, i) => new PearlTraceModel
            {
                Tick = i, 
                XCoor = t.Motion.X, 
                YCoor = t.Motion.Y, 
                ZCoor = t.Motion.Z
            }));
            EventManager.PublishEvent(this, "pearlMotion", new PearlSimulateArgs("Manually", traces));
            EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs("Manually", string.Empty, string.Empty));
        }
    }
}