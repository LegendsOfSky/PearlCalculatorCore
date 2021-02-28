using System.Linq;
using System;
using System.Collections.Generic;
using Avalonia.Media;
using PearlCalculatorCP.Commands;
using PearlCalculatorCP.Models;

namespace PearlCalculatorCP
{
    public class CommandManager
    {
        public static readonly IBrush HelpMsgTextColor = Brushes.Gray;
        public static readonly IBrush HelpCmdTextColor = Brushes.DarkBlue;

        public const string HelpType = "Help";
        public const string MsgType = "Msg";
        public const string ErrorType = "Error";

        static readonly List<string> EmptyList = new List<string>(0);
        
        internal class CommandRegistration
        {
            public readonly string Command;
            public readonly ICommand Handler;
            public readonly List<ConsoleOutputItemModel> Help;

            public CommandRegistration(string command, ICommand handler, List<string> help)
            {
                Command = command;
                Handler = handler;
                Help = new List<ConsoleOutputItemModel>(help.Count);
                foreach (var message in help)
                {
                    Help.Add(new ConsoleOutputItemModel
                    {
                        Type = HelpType,
                        Message = message,
                        TextColor = HelpMsgTextColor
                    });
                }
            }
        }
        
        
        private static CommandManager? _instance;
        public static CommandManager Instance => _instance ??= new CommandManager();

        
        private event Action<ConsoleOutputItemModel>? _onMessageSend;
        public event Action<ConsoleOutputItemModel>? OnMessageSend
        {
            add
            {
                var b = _onMessageSend is null;
                _onMessageSend += value;
                if (b)
                    LogInit();
            }
            remove => _onMessageSend -= value;
        }

        internal List<CommandRegistration> CommandList { get; private set; } = new List<CommandRegistration>(1000);

        private readonly Action<ConsoleOutputItemModel> _messageSender;

        private CommandManager()
        {
            _messageSender = SendMessage;
        }

        private void SendMessage(ConsoleOutputItemModel? message)
        {
            if (message is { })
                _onMessageSend?.Invoke(message);
        }

        public void Help()
        {
            foreach (var cmd in CommandList)
            {
                SendMessage(new ConsoleOutputItemModel($"{HelpType}/Cmd", $"/{cmd.Command}", HelpCmdTextColor));
                foreach (var help in cmd.Help)
                {
                    SendMessage(help);
                }
            }
        }

        public static void Register(string command, ICommand handler, List<string>? help)
        {
            Instance.CommandList.Add(new CommandRegistration(command, handler, help ?? EmptyList));
        }

        public bool TryGetCommandHandler<T>(string command, out T handler) where T : class, ICommand
        {
            var registration = CommandList.FirstOrDefault(registration => command == registration.Command);
            handler = registration is null ? null : (T)registration.Handler;
            return handler is null;
        }

        public void ExcuteCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command) || string.IsNullOrEmpty(command))
            {
                LogSyntaxError(null);
                return;
            }
            
            if (command.Equals("help") || command.Equals("?"))
            {
                Help();
                return;
            }

            var paras = command.TrimEnd().TrimStart().Split(" ");
            var cmdName = paras[0];
            string[]? cmdParas = paras.Length > 1 ? paras[1..] : null;

            bool isFindCmd = false;
            
            foreach (var cmd in CommandList)
            {
                if (cmdName.Equals(cmd.Command))
                {
                    cmd.Handler.Excute(cmdParas, cmdName, _messageSender);
                    isFindCmd = true;
                    break;
                }
            }
            
            if (!isFindCmd)
                LogSyntaxError(cmdName);
        }

        void LogSyntaxError(string? cmd)
        {
            if (cmd is null)
                SendMessage(DefineCmdOutput.NullCmd);
            else
                SendMessage(DefineCmdOutput.ErrorTemplate($"command \"{cmd}\" not found"));
        }

        void LogInit()
        {
            SendMessage(new ConsoleOutputItemModel{Type = MsgType, Message = "CommandManager Linked"});
            SendMessage(new ConsoleOutputItemModel{Type = MsgType, Message = "Input \"/help\" or \"/?\" to check instructions"});

            foreach (var reg in CommandList)
                reg.Handler.OnLinkOutput(_messageSender);
        }
    }

    static class DefineCmdOutput
    {
        public static readonly ConsoleOutputItemModel NullCmd = new ConsoleOutputItemModel(CommandManager.ErrorType, "Command can not be empty or null", Brushes.Red);
        
        public static ConsoleOutputItemModel ErrorTemplate(string msg) => new ConsoleOutputItemModel(CommandManager.ErrorType, msg, Brushes.Red);

        public static ConsoleOutputItemModel MsgTemplate(string msg) => new ConsoleOutputItemModel(CommandManager.MsgType, msg);
        
    }
}