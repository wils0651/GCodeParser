using GCodeParser.Models;

namespace GCodeParser
{
    public interface ICommandParser
    {
        ParsedCommand ParseCommand(string line);
    }
}
