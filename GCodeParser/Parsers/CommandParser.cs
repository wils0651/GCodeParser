using System;
using System.Collections.Generic;
using GCodeParser.Models;

namespace GCodeParser
{
    public class CommandParser : ICommandParser
    {
        private HashSet<string> _validCommandPrefixes;

        public CommandParser()
        {
            var commands = new string[] { "G", "M", "T" };
            _validCommandPrefixes = new HashSet<string>(commands);
        }

        public ParsedCommand ParseCommand(string line)
        {
            string command = "NOP";

            if (string.IsNullOrWhiteSpace(line))
            {
                return new ParsedCommand(command, line);
            }

            // look at first char, if invalid 
            string firstChar = line.Substring(0, 1);
            firstChar.ToUpper();

            if (firstChar.Equals("("))  // Comments are in ()
            {
                return new ParsedCommand(command, line);
            }

            if (_validCommandPrefixes.Contains(firstChar))
            {
                command = firstChar;
                string commandNumberString = string.Empty;
                int characterCount = 1;
                bool isNumeral = true;
                while (isNumeral && characterCount < line.Length)
                {
                    string character = line.Substring(characterCount, 1);
                    int numeral;
                    isNumeral = int.TryParse(character, out numeral);
                    if (isNumeral)
                    {
                        commandNumberString += character;
                        characterCount++;
                    }
                }
                int commandNumber;
                var isCommandNumber = int.TryParse(commandNumberString, out commandNumber);
                command += commandNumber;

                line = line.Substring(characterCount).Trim();
            }

            return new ParsedCommand(command, line);
        }
    }
}
