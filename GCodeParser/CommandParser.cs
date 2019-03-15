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
            // Parse
            // split
            // look at first char, if invalid 

            return new ParsedCommand(command, line);
        }
    }
}
