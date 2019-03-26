using GCodeParser.Models;

namespace GCodeParser.Intepreters
{
    public interface IGCodeInterpreter
    {
        string GetInterpreterType();
        void InterpretGCode(ParsedCommand parsedCommand, IMachine machine);
    }
}
