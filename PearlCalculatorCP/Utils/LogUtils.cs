using System;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP.Utils
{
    public static class LogUtils
    {
        public static event Action<ConsoleOutputItemModel>? OnLog;

        public static void Error(string msg)
        {
            OnLog?.Invoke(DefineCmdOutput.ErrorTemplate(msg));
        }

        public static void Log(string msg)
        {
            OnLog?.Invoke(DefineCmdOutput.MsgTemplate(msg));
        }
    }
}