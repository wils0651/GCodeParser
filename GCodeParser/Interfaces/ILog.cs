using GCodeParser.Models;

namespace GCodeParser
{
    public interface ILog
    {
        void LogCommandWithNoInterpreter(ParsedCommand command);
        void LogUnrecognizedText(string text);

        string[] GetCommandsWithNoInterpreter();
        string[] GetNumberOfCommandsWithNoInterpreter();
        string[] GetUnrecognizedText();

        void OutputLogs();

    }
}
