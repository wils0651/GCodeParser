using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCodeParser.Models;
using GCodeParser.Parsers;

namespace GCodeParser.Intepreters
{
    public class G1 : IGCodeInterpreter
    {
        /*
         * Switch to linear motion at the current feed rate. 
         * Used to cut a straight line --- the interpreter 
         * will determine the acceleration needed along each 
         * axis to ensure direct movement from the original to 
         * the destination point at no more than the current 
         * Feed rate.
         */ 

        private IMoveParser _parser;
        public static string GCODE_TYPE = "G1";

        public G1(IMoveParser parser)
        {
            _parser = parser;
        }

        public string GetInterpreterType()
        {
            return GCODE_TYPE;
        }

        public void InterpretGCode(ParsedCommand parsedCommand, Machine machine)
        {
            var parsedMoveDict = _parser.ParseMove(parsedCommand.ParameterString);

            double x = parsedMoveDict.TryGetValue('X', out double xResult) ? xResult : 0.0;
            double y = parsedMoveDict.TryGetValue('Y', out double yResult) ? yResult : 0.0;
            double z = parsedMoveDict.TryGetValue('Z', out double zResult) ? zResult : 0.0;
            double speed = parsedMoveDict.TryGetValue('F', out double fResult) ? fResult : 0.0;

            // TODO: Calculate duration

            // Update machine state
            machine.UpdatePosition(x, y, z);
        }
    }
}
