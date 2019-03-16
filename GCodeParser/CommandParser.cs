using System;
using System.Collections.Generic;
using GCodeParser.Models;

namespace GCodeParser
{
    class CommandParser : ICommandParser
    {
        private HashSet<string> _validCommandPrefixes;

        public CommandParser()
        {
            var commands = new string[] { "G", "M" };
            _validCommandPrefixes = new HashSet<string>(commands);
        }

        public ParsedCommand ParseCommand(string line)
        {
            string command = "NOP";

            if (string.IsNullOrWhiteSpace(line))
            {
                return new ParsedCommand(command, line);
            }
            // Parse
            // look at first char, if invalid 
            string firstChar = line.Substring(0, 1);
            if (_validCommandPrefixes.Contains(firstChar))
            {
                command = firstChar;
                int characterCount = 1;
                bool isNumeral = true;
                while (isNumeral && characterCount < line.Length)
                {
                    string character = line.Substring(characterCount, 1);
                    int numeral;
                    isNumeral = int.TryParse(character, out numeral);
                    if (isNumeral)
                    {
                        command += character;
                        characterCount++;
                    }
                }
                line = line.Substring(characterCount).Trim();
            }

            return new ParsedCommand(command, line);
        }
    }
}
