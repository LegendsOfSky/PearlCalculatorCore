using System.Collections.Generic;
using System.Linq;
using PearlCalculatorCP.Models;
using ReactiveUI;
using PearlCalculatorLib.Manually;
using PearlCalculatorLib.PearlCalculationLib.Utility;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorCP.ViewModels
{
    public class CustomFTLViewModel : ViewModelBase
    {
        public const string PublishKey = "CustomFTL";
        
        private double _pearlX;
        public double PearlX
        {
            get => _pearlX;
            set => this.RaiseAndSetIfChanged(ref _pearlX , value);
        }

        private double _pearlY;
        public double PearlY
        {
            get => _pearlY;
            set => this.RaiseAndSetIfChanged(ref _pearlY , value);
        }

        private double _pearlZ;
        public double PearlZ
        {
            get => _pearlZ;
            set => this.RaiseAndSetIfChanged(ref _pearlZ , value);
        }

        public Space3D PearlPos => new(_pearlX, _pearlY, _pearlZ);

        private double _momentumX;
        public double MomentumX
        {
            get => _momentumX;
            set => this.RaiseAndSetIfChanged(ref _momentumX , value);
        }

        private double _momentumY;
        public double MomentumY
        {
            get => _momentumY;
            set => this.RaiseAndSetIfChanged(ref _momentumY , value);
        }

        private double _momentumZ;
        public double MomentumZ
        {
            get => _momentumZ;
            set => this.RaiseAndSetIfChanged(ref _momentumZ , value);
        }

        public Space3D PearlMomentum => new(_momentumX, _momentumY, _momentumZ);

        public PearlEntity Pearl => new(PearlMomentum, PearlPos);


        private double _aTX;
        public double ATX
        {
            get => _aTX;
            set => this.RaiseAndSetIfChanged(ref _aTX , value);
        }

        private double _aTY;
        public double ATY
        {
            get => _aTY;
            set => this.RaiseAndSetIfChanged(ref _aTY , value);
        }

        private double _aTZ;
        public double ATZ
        {
            get => _aTZ;
            set => this.RaiseAndSetIfChanged(ref _aTZ , value);
        }

        public Space3D ATNT => new(_aTX, _aTY, _aTZ);

        private double _bTX;
        public double BTX
        {
            get => _bTX;
            set => this.RaiseAndSetIfChanged(ref _bTX , value);
        }

        private double _bTY;
        public double BTY
        {
            get => _bTY;
            set => this.RaiseAndSetIfChanged(ref _bTY , value);
        }

        private double _bTZ;
        public double BTZ
        {
            get => _bTZ;
            set => this.RaiseAndSetIfChanged(ref _bTZ , value);
        }
        
        public Space3D BTNT => new(_bTX, _bTY, _bTZ);



        private int _aTNTAmount;
        public int ATNTAmount
        {
            get => _aTNTAmount;
            set => this.RaiseAndSetIfChanged(ref _aTNTAmount , value);
        }

        private int _bTNTAmount;
        public int BTNTAmount
        {
            get => _bTNTAmount;
            set => this.RaiseAndSetIfChanged(ref _bTNTAmount , value);
        }

        private double _destinationX;
        public double DestinationX
        {
            get => _destinationX;
            set => this.RaiseAndSetIfChanged(ref _destinationX , value);
        }

        private double _destinationZ;
        public double DestinationZ
        {
            get => _destinationZ;
            set => this.RaiseAndSetIfChanged(ref _destinationZ , value);
        }

        public Surface2D Destination => new(_destinationX, _destinationZ);

        public ManuallyData CreateManuallyData() => new(ATNTAmount, BTNTAmount, ATNT, BTNT, Destination, Pearl);


        public void CalculateAmount()
        {
            var data = CreateManuallyData();
            if (Calculation.CalculateTNTAmount(data, MainWindowViewModel.MaxTicks, MainWindowViewModel.MaxDistance, out var result))
            {
                EventManager.PublishEvent(this, "calculate", new CalculateTNTAmountArgs(PublishKey, result));
                var angle = data.Pearl.Position.WorldAngle(data.Destination.ToSpace3D());
                var direction = DirectionUtils.GetDirection(angle).ToString();
                EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs(PublishKey, direction, angle.ToString()));
            }
            else
            {
                EventManager.PublishEvent(this, "calculate", new CalculateTNTAmountArgs(PublishKey, null));
                EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs(PublishKey, string.Empty, string.Empty));
            }
        }

        public void CalculateTrace()
        {
            var data = CreateManuallyData();
            var entities = Calculation.CalculatePearlTrace(data, MainWindowViewModel.MaxTicks);
            var chunks = ListCoverterUtility.ToChunk(entities);

            var traces = new List<PearlTraceModel>(entities.Count);
            var chunkModels = new List<PearlTraceChunkModel>(chunks.Count);

            traces.AddRange(entities.Select((t, i) => new PearlTraceModel 
            {
                Tick = i, 
                XCoor = t.Position.X, 
                YCoor = t.Position.Y, 
                ZCoor = t.Position.Z
            }));
            chunkModels.AddRange(chunks.Select((c, i) => new PearlTraceChunkModel{Tick = i, XCoor = c.X, ZCoor = c.Z}));

            EventManager.PublishEvent(this, "pearlTrace", new PearlSimulateArgs(PublishKey, traces, chunkModels));
            EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs(PublishKey, string.Empty, string.Empty));
        }

        public void CalculateMomentum()
        {
            var data = CreateManuallyData();
            var entities = Calculation.CalculatePearlTrace(data, MainWindowViewModel.MaxTicks);
            var traces = new List<PearlTraceModel>(entities.Count);
            traces.AddRange(entities.Select((t, i) => new PearlTraceModel
            {
                Tick = i, 
                XCoor = t.Motion.X, 
                YCoor = t.Motion.Y, 
                ZCoor = t.Motion.Z
            }));
            EventManager.PublishEvent(this, "pearlMotion", new PearlSimulateArgs(PublishKey, traces, null));
            EventManager.PublishEvent(this, "showDirectionResult", new ShowDirectionResultArgs(PublishKey, string.Empty, string.Empty));
        }
    }
}