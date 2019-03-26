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
        private IMachine _machine;

        public CommandInterpreter(IGCodeInterpreter[] gCodeInterpreters, IMachineStorage machineStore)
        {
            _interpreters = gCodeInterpreters.ToDictionary(gci => gci.GetInterpreterType(), gci => gci);
            _machine = machineStore.GetMachine();
        }


        public void InterpretCommands(List<ParsedCommand> commands)
        {
            foreach (var command in commands)
            {
                IGCodeInterpreter interpreter;
                if(!_interpreters.TryGetValue(command.GCodeCommand, out interpreter))
                {
                    // TODO: Don't have this interpreter
                    Console.WriteLine("No Interpreter for " + command.GCodeCommand);
                }
                else
                {
                    interpreter.InterpretGCode(command, _machine);
                    Console.WriteLine("Interpreted " + command.GCodeCommand + ", total duration: " + _machine.GetDuration());
                }
            }
        }
    }
}
