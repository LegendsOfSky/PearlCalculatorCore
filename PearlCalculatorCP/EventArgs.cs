using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System.Collections.Generic;

namespace PearlCalculatorCP
{
    public abstract class PCEventArgs
    {
        public readonly string PublishKey;

        protected PCEventArgs(string publishKey)
        {
            this.PublishKey = publishKey;
        }
    }

    public class ButtonClickArgs : PCEventArgs
    {
        public ButtonClickArgs(string publishKey) : base(publishKey)
        {
        }
    }

    public class PearlSimulateArgs : PCEventArgs
    {
        public readonly List<Entity> Trace;

        public PearlSimulateArgs(string publishKey, List<Entity> trace) : base(publishKey)
        {
            this.Trace = trace;
        }
    }

    public class SetRTCountArgs : PCEventArgs
    {
        public readonly int Red;
        public readonly int Blue;

        public SetRTCountArgs(string publishKey, int red, int blue) : base(publishKey)
        {
            this.Red = red;
            this.Blue = blue;
        }
    }

    public class CalculateTNTAmountArgs : PCEventArgs
    {
        public readonly List<TNTCalculationResult> Results;

        public CalculateTNTAmountArgs(string publishKey, List<TNTCalculationResult> results) : base(publishKey)
        {
            this.Results = results;
        }
    }
}
