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

        static readonly List<string> EmptyList = new(0);
        
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

        internal Dictionary<string ,CommandRegistration> CommandList { get; private set; } = new(100);

        private readonly Action<ConsoleOutputItemModel> _messageSender;

        private CommandManager()
        {
            _messageSender = SendMessage;
        }

        private void SendMessage(ConsoleOutputItemModel? message)
        {
            if (message is not null)
            {
                Console.WriteLine($"{message.Type} : {message.Message}");
                _onMessageSend?.Invoke(message);
            }
        }

        public void Help()
        {
            foreach (var cmd in CommandList.Values)
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
            if (!Instance.CommandList.TryAdd(command, new CommandRegistration(command, handler, help ?? EmptyList)))
                throw new ArgumentException($"you are trying to import an existed command {command}");
        }


#nullable disable
        public bool TryGetCommandHandler<T>(string command, out T handler) where T : class, ICommand
        {
            var isFound = CommandList.TryGetValue(command, out var registration);
            handler = isFound ? (T)registration.Handler : null;
            return isFound;
        }

        public void ExecuteCommand(string command, bool splitParameter = true)
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

            command = command.TrimEnd().TrimStart();
            string[] paras;
            string cmdName;
            
            if (splitParameter)
            {
                paras = command.Split(" ");
                cmdName = paras[0];
                paras = paras.Length > 1 ? paras[1..] : null;
            }
            else
            {
                var index = command.IndexOf(" ", StringComparison.Ordinal);
                cmdName = index == -1 ? command : command[..index];
                paras = index == -1 ? null : new []{command[(index+1)..]};
            }

            bool isFindCmd = CommandList.TryGetValue(cmdName, out var registration);

            if (isFindCmd)
                registration.Handler.Execute(paras, cmdName, _messageSender);
            else
                LogSyntaxError(cmdName);
        }
#nullable enable

        void LogSyntaxError(string? command)
        {
            if (command is null)
                SendMessage(DefineCmdOutput.NullCmd);
            else
                SendMessage(DefineCmdOutput.ErrorTemplate($"command \"{command}\" not found"));
        }

        void LogInit()
        {
            SendMessage(new ConsoleOutputItemModel{Type = MsgType, Message = "CommandManager Linked"});
            SendMessage(new ConsoleOutputItemModel{Type = MsgType, Message = "Input \"/help\" or \"/?\" to check instructions"});

            foreach (var reg in CommandList.Values)
            {
                if (reg.Handler is ICommandOutputLink o)
                    o.OnLinkOutput(_messageSender);
            }
        }
    }

    static class DefineCmdOutput
    {
        public static readonly ConsoleOutputItemModel NullCmd = new(CommandManager.ErrorType, "Command can not be empty or null", Brushes.Red);
        
        public static ConsoleOutputItemModel ErrorTemplate(string msg) => new(CommandManager.ErrorType, msg, Brushes.Red);

        public static ConsoleOutputItemModel MsgTemplate(string msg) => new(CommandManager.MsgType, msg);
        
    }
}