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

            float x = parsedMoveDict.TryGetValue('X', out float xResult) ? xResult : 0.0F;
            float y = parsedMoveDict.TryGetValue('Y', out float yResult) ? yResult : 0.0F;
            float z = parsedMoveDict.TryGetValue('Z', out float zResult) ? zResult : 0.0F;
            float speed = parsedMoveDict.TryGetValue('F', out float fResult) ? fResult : 0.0F;

            // TODO: Calculate duration

            // Update machine state
            machine.UpdatePosition(x, y, z);
        }
    }
}
