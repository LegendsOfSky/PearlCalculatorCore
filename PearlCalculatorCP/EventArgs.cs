﻿using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System.Collections.Generic;
using JetBrains.Annotations;
using PearlCalculatorCP.Models;
using PearlCalculatorCP.ViewModels;
using PearlCalculatorLib.General;

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

    public class NotificationArgs : PCEventArgs
    {
        public NotificationArgs(string publishKey) : base(publishKey)
        {
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
        public readonly List<PearlTraceModel> Trace;
        public readonly List<PearlTraceChunkModel>? Chunks;

        public PearlSimulateArgs(string publishKey, List<PearlTraceModel> trace, List<PearlTraceChunkModel>? chunks) : base(publishKey)
        {
            this.Trace = trace;
            this.Chunks = chunks;
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

    public class ShowDirectionResultArgs : PCEventArgs
    {
        public readonly string Direction;
        public readonly string Angle;
        
        public ShowDirectionResultArgs(string publishKey, string direction, string angle) : base(publishKey)
        {
            this.Direction = direction;
            this.Angle = angle;
        }
    }

    public class TNTWeightChangedArgs : PCEventArgs
    {
        public readonly TNTWeightModeEnum WeightMode;
        public TNTWeightChangedArgs(string publishKey, TNTWeightModeEnum weightMode) : base(publishKey)
        {
            this.WeightMode = weightMode;
        }
    }

    public class LoadSettingsArgs : PCEventArgs
    {
        public readonly Settings Settings;

        public LoadSettingsArgs(string publishKey, Settings settings) : base(publishKey)
        {
            this.Settings = settings;
        }
    }

    public class SwitchChunkModeArgs : PCEventArgs
    {
        public readonly bool EnableChunkMode;

        public SwitchChunkModeArgs(string publishKey, bool enable) : base(publishKey)
        {
            this.EnableChunkMode = enable;
        }
    }
}
