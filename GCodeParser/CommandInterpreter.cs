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
        private ILog _log;

        public CommandInterpreter(IGCodeInterpreter[] gCodeInterpreters, IMachineStorage machineStore, ILog log)
        {
            _interpreters = gCodeInterpreters.ToDictionary(gci => gci.GetInterpreterType(), gci => gci);
            _machine = machineStore.GetMachine();
            _log = log;
        }


        public void InterpretCommands(List<ParsedCommand> commands)
        {
            foreach (var command in commands)
            {
                IGCodeInterpreter interpreter;
                if(!_interpreters.TryGetValue(command.GCodeCommand, out interpreter))
                {
                    _log.LogCommandWithNoInterpreter(command);
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
