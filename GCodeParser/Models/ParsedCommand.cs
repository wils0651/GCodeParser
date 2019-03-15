namespace GCodeParser.Models
{
    public class ParsedCommand
    {
        string GCodeCommand { get; set; }
        string ParameterString { get; set; }

        public ParsedCommand(string command, string parameters)
        {
            GCodeCommand = command;
            ParameterString = parameters;
        }
    }
}
