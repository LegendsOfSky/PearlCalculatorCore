using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PearlCalculatorCP.Models;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using ReactiveUI;

namespace PearlCalculatorCP.ViewModels
{
    public class ResultPanelViewModel : ViewModelBase
    {
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
                if (value >= -1 && value < AmountResult!.Count && value != _amountResultSelectedIndex)
                {
                    this.RaiseAndSetIfChanged(ref _amountResultSelectedIndex, value);
                    var res = AmountResult[value];
                    EventManager.PublishEvent(this, "setRTCount", new SetRTCountArgs("ResultPanel", res.Red, res.Blue));
                }
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
            set => this.RaiseAndSetIfChanged(ref _showMode, value);
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
                
                _amountList = args.Results;
                AmountResult = new ObservableCollection<TNTCalculationResult>(_amountList);
                AmountResultSelectedIndex = -1;
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
            });
            
            EventManager.AddListener<PearlSimulateArgs>("pearlMotion", (sender, args) =>
            {
                _showMode = ResultShowMode.Motion;
                PearlMotionList = new ObservableCollection<PearlTraceModel>(args.Trace);
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
    }
    
    public enum ResultShowMode
    {
        Amount,
        Trace,
        Motion
    }
}