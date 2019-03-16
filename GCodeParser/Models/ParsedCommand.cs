namespace GCodeParser.Models
{
    public class ParsedCommand
    {
        public string GCodeCommand { get; set; }
        public string ParameterString { get; set; }

        public ParsedCommand(string command, string parameters)
        {
            GCodeCommand = command;
            ParameterString = parameters;
        }
    }
}
