using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PearlCalculatorCP.Models;
using PearlCalculatorLib.Result;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class ResultPanelViewModel : ViewModelBase
    {
        public event Action<ResultShowMode>? OnShowModeSet;
        
        //Amount
        private List<TNTCalculationResult>? _amountList;
        
        private ObservableCollection<TNTCalculationResult>? _amountResult;
        public ObservableCollection<TNTCalculationResult>? AmountResult
        {
            get => _amountResult;
            set => this.RaiseAndSetProperty(ref _amountResult, value);
        }
        
        private int _amountResultSelectedIndex = -1;
        public int AmountResultSelectedIndex
        {
            get => _amountResultSelectedIndex;
#nullable disable
            set
            {
                if (value < -1 || value == _amountResultSelectedIndex || (AmountResult != null && value >= AmountResult.Count)) 
                    return;

                this.RaiseAndSetIfChanged(ref _amountResultSelectedIndex, value);

                if (value == -1) return;

                var res = AmountResult[value];
                EventManager.PublishEvent(this, "setRTCount", new SetRTCountArgs("ResultPanel", res.Red, res.Blue));
            }
#nullable enable
        }
        
        
        //Pearl Trace
        private ObservableCollection<PearlTraceModel>? _pearlTraceList;
        public ObservableCollection<PearlTraceModel>? PearlTraceList
        {
            get => _pearlTraceList;
            set => this.RaiseAndSetProperty(ref _pearlTraceList, value);
        }

        private ObservableCollection<PearlTraceChunkModel>? _pearlTraceChunkList;
        public ObservableCollection<PearlTraceChunkModel>? PearlTraceChunkList
        {
            get => _pearlTraceChunkList;
            set => this.RaiseAndSetProperty(ref _pearlTraceChunkList, value);
        }

        //Pearl Motion
        private ObservableCollection<PearlTraceModel>? _pearlMotionList;
        public ObservableCollection<PearlTraceModel>? PearlMotionList
        {
            get => _pearlMotionList;
            set => this.RaiseAndSetProperty(ref _pearlMotionList, value);
        }

        private ResultShowMode _showMode = ResultShowMode.Amount;
        public ResultShowMode ShowMode
        {
            get => _showMode;
            set
            {
                if (value == ResultShowMode.Trace || value == ResultShowMode.ChunkTrace)
                    value = EnableChunkMode ? ResultShowMode.ChunkTrace : ResultShowMode.Trace;

                this.RaiseAndSetIfChanged(ref _showMode, value);
                OnShowModeSet?.Invoke(value);
            }
        }

        private ResultAmountDataSource _amountDataSource = ResultAmountDataSource.General;

        private bool _enableChunkMode;
        public bool EnableChunkMode
        {
            get => _enableChunkMode;
            private set
            {
                _enableChunkMode = value;
                
                ShowMode = _enableChunkMode switch
                {
                    true when ShowMode == ResultShowMode.Trace => ResultShowMode.ChunkTrace,
                    false when ShowMode == ResultShowMode.ChunkTrace => ResultShowMode.Trace,
                    _ => ShowMode
                };
            }
        }
        
        
        private string _resultDirection = string.Empty;
        public string ResultDirection
        {
            get => _resultDirection;
            set => this.RaiseAndSetIfChanged(ref _resultDirection, value);
        }

        private string _resultAngle = string.Empty;
        public string ResultAngle
        {
            get => _resultAngle;
            set => this.RaiseAndSetIfChanged(ref _resultAngle, value);
        }

        public ResultPanelViewModel()
        {
            EventManager.AddListener<CalculateTNTAmountArgs>("calculate", (sender, args) =>
            {
                ShowMode = ResultShowMode.Amount;

                _amountDataSource = args.PublishKey == "Manually"
                    ? ResultAmountDataSource.Manually
                    : ResultAmountDataSource.General;
                
                _amountList = args.Results;
                SortAmountResultByWeight(AdvanceViewModel.StaticWeightMode);
                AmountResult = new ObservableCollection<TNTCalculationResult>(_amountList);
                AmountResultSelectedIndex = -1;

                PearlMotionList = null;
                PearlTraceList = null;
                PearlTraceChunkList = null;
            });
            
            EventManager.AddListener<ShowDirectionResultArgs>("showDirectionResult", (sender, args) =>
            {
                ResultDirection = args.Direction;
                ResultAngle = args.Angle;
            });
            
            EventManager.AddListener<PearlSimulateArgs>("pearlTrace", (sender, args) =>
            {
                ShowMode = ResultShowMode.Trace;
                PearlTraceList = new ObservableCollection<PearlTraceModel>(args.Trace);
                PearlTraceChunkList = new ObservableCollection<PearlTraceChunkModel>(args.Chunks!);
                
                AmountResultSelectedIndex = -1;
                AmountResult = null;
                _amountList = null;
                PearlMotionList = null;
            });
            
            EventManager.AddListener<PearlSimulateArgs>("pearlMotion", (sender, args) =>
            {
                ShowMode = ResultShowMode.Motion;
                PearlMotionList = new ObservableCollection<PearlTraceModel>(args.Trace);

                AmountResultSelectedIndex = -1;
                AmountResult = null;
                _amountList = null;
                PearlTraceList = null;
                PearlTraceChunkList = null;
            });
            
            EventManager.AddListener<TNTWeightChangedArgs>("tntWeightChanged", (sender, args) =>
            {
                if (ShowMode != ResultShowMode.Amount || _amountList is null || _amountList.Count == 0 ||
                    _amountDataSource != ResultAmountDataSource.General)
                    return;
                
                SortAmountResultByWeight(args.WeightMode);

                AmountResult = new ObservableCollection<TNTCalculationResult>(_amountList);
                AmountResultSelectedIndex = -1;
            });
            
            EventManager.AddListener<SwitchChunkModeArgs>("switchChunkMode", (sender, args) =>
            {
                EnableChunkMode = args.EnableChunkMode;
            });
        }
        
        public void SortAmountResultByDistance()
        {
            if (_amountList is null || _amountList.Count == 0) return;
            _amountList.SortByDistance();
            AmountResult = new ObservableCollection<TNTCalculationResult>(_amountList!);
        }

        public void SortAmountResultByTick()
        {
            if (_amountList is null || _amountList.Count == 0) return;
            _amountList.SortByTick();
            AmountResult = new ObservableCollection<TNTCalculationResult>(_amountList!);
        }

        public void SortAmountResultByTotal()
        {
            if (_amountList is null || _amountList.Count == 0) return;
            _amountList.SortByTotal();
            AmountResult = new ObservableCollection<TNTCalculationResult>(_amountList!);
        }

        private void SortAmountResultByWeight(TNTWeightModeEnum weightMode)
        {
            var args = new TNTResultSortByWeightedArgs(
                PearlCalculatorLib.General.Data.TNTWeight,
                PearlCalculatorLib.General.Data.MaxCalculateTNT, 
                PearlCalculatorLib.General.Data.MaxCalculateDistance);
            
            if (weightMode == TNTWeightModeEnum.Distance)
            {
                _amountList.SortByWeightedDistance(args);
            }
            else
            {
                _amountList.SortByWeightedTotal(args);
            }
        }
    }
    
    public enum ResultShowMode
    {
        Amount,
        Trace,
        Motion,
        ChunkTrace
    }

    public enum ResultAmountDataSource
    {
        General, Manually
    }
}