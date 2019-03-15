using GCodeParser.Models;

namespace GCodeParser
{
    interface ICommandParser
    {
        ParsedCommand ParseCommand(string line);
    }
}
