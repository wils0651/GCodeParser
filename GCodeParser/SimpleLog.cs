using System;
using System.Collections.Generic;
using System.Linq;
using GCodeParser.Models;

namespace GCodeParser
{
    class SimpleLog : ILog
    {
        private List<string> _unrecognizedText;
        private List<ParsedCommand> _commandsWithNoInterpreter;

        public string[] GetCommandsWithNoInterpreter()
        {
            if (_commandsWithNoInterpreter == null)
            {
                return new string[] { "No Unrecognized Text" };
            }

            var retVal = new List<string>();
            _commandsWithNoInterpreter.ForEach(c => retVal.Add(PrintCommand(c)));
            return retVal.ToArray();
        }

        public string[] GetNumberOfCommandsWithNoInterpreter()
        {
            if (_commandsWithNoInterpreter == null)
            {
                return new string[] { "No Unrecognized Text" };
            }

            Dictionary<string, int> commandAndCount = new Dictionary<string, int>();
            foreach (var command in _commandsWithNoInterpreter)
            {
                if (commandAndCount.TryGetValue(command.GCodeCommand, out int value))
                {
                    commandAndCount[command.GCodeCommand] = value + 1;
                }
                else
                {
                    commandAndCount.Add(command.GCodeCommand, 1);
                }
            }

            var retVal = new List<string>();
            return commandAndCount
                .OrderBy(c => c.Key)
                .OrderByDescending(c => c.Value)
                .Select(c => c.Key.ToString() + ": " + c.Value.ToString())
                .ToArray();
        }

        public string[] GetUnrecognizedText()
        {
            if(_unrecognizedText == null)
            {
                return new string[] { "No Unrecognized Text" };
            }
            return _unrecognizedText.ToArray();
        }

        public void LogCommandWithNoInterpreter(ParsedCommand command)
        {
            if(_commandsWithNoInterpreter == null)
            {
                _commandsWithNoInterpreter = new List<ParsedCommand>();
            }
            _commandsWithNoInterpreter.Add(command);

        }

        public void LogUnrecognizedText(string text)
        {
            if(_unrecognizedText == null)
            {
                _unrecognizedText = new List<string>();
            }
            _unrecognizedText.Add(text);
        }

        public void OutputLogs()
        {
            Console.WriteLine("=== Unrecognized Text ===");
            var unrecognized = GetUnrecognizedText();
            foreach (var item in unrecognized)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n=== Commands With No Interpreter ===");
            var noInterpreter = GetNumberOfCommandsWithNoInterpreter();
            foreach (var item in noInterpreter)
            {
                Console.WriteLine(item);
            }
        }

        private string PrintCommand(ParsedCommand parsedCommand)
        {
            return $"Command: {parsedCommand.GCodeCommand}; Parameter String: {parsedCommand.ParameterString}";
        }

    }
}
