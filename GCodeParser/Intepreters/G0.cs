using System;
using GCodeParser.Models;
using GCodeParser.Parsers;

namespace GCodeParser.Intepreters
{
    public class G0 : IGCodeInterpreter
    {
        private readonly IMoveParser _parser;
        public static string GCODE_TYPE = "G0";

        // G0: rapid move
        public G0(IMoveParser parser)
        {
            _parser = parser;
        }
        public string GetInterpreterType()
        {
            return GCODE_TYPE;
        }

        public void InterpretGCode(ParsedCommand parsedCommand, IMachine machine)
        {
            var parsedMoveDict = _parser.ParseMove(parsedCommand.ParameterString);

            double x = parsedMoveDict.TryGetValue('X', out double xResult) ? xResult : 0.0;
            double y = parsedMoveDict.TryGetValue('Y', out double yResult) ? yResult : 0.0;
            double z = parsedMoveDict.TryGetValue('Z', out double zResult) ? zResult : 0.0;
            double speed = parsedMoveDict.TryGetValue('F', out double fResult) ? fResult : 0.0;

            // TODO: Calculate duration

            // Update machine state
            machine.UpdatePosition(x, y, z);

            //TODO:]
            machine.AddTime(1.0);
        }
    }
}
