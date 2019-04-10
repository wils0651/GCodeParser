using GCodeParser.Interfaces;
using GCodeParser.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace GCodeParser
{
    public class FileHandler : IFileHandler
    {
        private ICommandParser _parser;
        private ILog _log;
        private List<ParsedCommand> _commandList;
        private List<string> _unrecognizedCommands;

        public FileHandler(ICommandParser commandParser, ILog log)
        {
            _parser = commandParser;
            _log = log;
            _commandList = new List<ParsedCommand>();
            _unrecognizedCommands = new List<string>();
        }

        public List<ParsedCommand> ProcessFile(string filePath)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int lineCount = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        line.Trim();
                        lineCount++;

                        string[] stringSeparators = new string[] { ";" };   // Split on ";"
                        string[] splitLine = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var theLine in splitLine)
                        {
                            ParseString(theLine);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return _commandList;

        }

        private void ParseString(string codeString)
        {
            var parsedCommand = _parser.ParseCommand(codeString);
            CheckCommand(parsedCommand);
        }

        private void CheckCommand(ParsedCommand parsedCommand)
        {
            if (!parsedCommand.GCodeCommand.Equals("NOP"))
            {
                var parsedString = _parser.ParseCommand(parsedCommand.ParameterString);
                if (!parsedString.GCodeCommand.Equals("NOP"))
                {
                    _commandList.Add(new ParsedCommand(parsedCommand.GCodeCommand, string.Empty));
                    ParseString(parsedCommand.ParameterString);
                }
                else
                {
                    _commandList.Add(parsedCommand);
                }
            }
            else
            {
                if (!parsedCommand.ParameterString.Trim().Equals(string.Empty))
                {
                    _log.LogUnrecognizedText(parsedCommand.ParameterString);
                }
            }
        }

        //private void OutputResults()
        //{
        //    Console.WriteLine("\nProcessed Commands");
        //    _commandList.ForEach(c => PrintCommand(c));

        //    Console.WriteLine("\nUnrecognized Text");
        //    _unrecognizedCommands.ForEach(uc => Console.WriteLine(uc));
        //}

        //private void PrintCommand(ParsedCommand parsedCommand)
        //{
        //    Console.WriteLine($"Command: {parsedCommand.GCodeCommand}; Parameter String: {parsedCommand.ParameterString}");
        //}

    }
}
