using System;
using System.Collections.Generic;
using System.Linq;
using GCodeParser.DurationCalculator;
using GCodeParser.Models;
using GCodeParser.Parsers;

namespace GCodeParser.Intepreters
{
    public class G0 : IGCodeInterpreter
    {
        private readonly IMoveParser _parser;
        private static string GCODE_TYPE = "G0";

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
            double feedRate = parsedMoveDict.TryGetValue('F', out double fResult) ? fResult : 0.0;

            // TODO: ? Log if feedRate > 0 

            // Update machine state
            var moveTime = MoveTime(machine, x, y, z);
            machine.AddTime(moveTime);

            machine.UpdatePosition(x, y, z);
        }

        private double MoveTime(IMachine machine, double x, double y, double z)
        {
            List<double> movingTimes = new List<double>();
            movingTimes.Add(LinearMoves.LinearMoveTime(machine.X, machine.VelocityX, machine.MaxVelocityX, machine.AccelerationX, x));
            movingTimes.Add(LinearMoves.LinearMoveTime(machine.Y, machine.VelocityY, machine.MaxVelocityY, machine.AccelerationY, y));
            movingTimes.Add(LinearMoves.LinearMoveTime(machine.Z, machine.VelocityZ, machine.MaxVelocityZ, machine.AccelerationZ, z));

            var maxTime = movingTimes.Max(mt => mt);

            return maxTime; 
        }
        
    }
}
