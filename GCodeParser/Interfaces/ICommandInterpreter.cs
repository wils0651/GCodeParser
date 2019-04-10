using GCodeParser.Models;
using System.Collections.Generic;

namespace GCodeParser
{
    public interface ICommandInterpreter
    {
        void InterpretCommands(List<ParsedCommand> commands);
    }
}
