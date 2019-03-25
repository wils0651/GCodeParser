using System;
using System.Collections.Generic;
using System.Linq;
using GCodeParser.Intepreters;
using GCodeParser.Models;

namespace GCodeParser
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly Dictionary<string, IGCodeInterpreter> _interpreters;
        private Machine _machine;

        public CommandInterpreter(IGCodeInterpreter[] gCodeInterpreters, Machine machine)
        {
            _interpreters = gCodeInterpreters.ToDictionary(gci => gci.GetInterpreterType(), gci => gci);
            _machine = machine;
        }


        public void InterpretCommands(List<ParsedCommand> commands)
        {
            foreach (var command in commands)
            {
                IGCodeInterpreter interpreter;
                if(!_interpreters.TryGetValue(command.GCodeCommand, out interpreter))
                {
                    // TODO: Don't have this interpreter
                }
                //interpreter.InterpretGCode(command, )
            }
        }
    }
}
