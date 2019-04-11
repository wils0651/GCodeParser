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
        private static double MOVE_THRESHOLD = 0.001;   //TODO: Centralize this

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

            // Update machine state
            var moveTime = MoveTime(machine, x, y, z);
            machine.AddTime(moveTime);

            machine.UpdatePosition(x, y, z);
        }

        private double MoveTime(IMachine machine, double x, double y, double z)
        {
            List<double> movingTimes = new List<double>();
            movingTimes.Add(LinearMoveTime(machine.X, machine.VelocityX, machine.MaxVelocityX, machine.AccelerationX, x));
            movingTimes.Add(LinearMoveTime(machine.Y, machine.VelocityY, machine.MaxVelocityY, machine.AccelerationY, y));
            movingTimes.Add(LinearMoveTime(machine.Z, machine.VelocityZ, machine.MaxVelocityZ, machine.AccelerationZ, z));

            var maxTime = movingTimes.Max(mt => mt);

            return maxTime; 
        }

        private double LinearMoveTime(
            double currentLocation, 
            double currentVelocity, 
            double maxVelocity, 
            double acceleration, 
            double endLocation)
        {
            double timeMoving = 0;
            double moveDistance = endLocation - currentLocation;

            if(Math.Abs(moveDistance) < MOVE_THRESHOLD)
            {
                return timeMoving;
            }

            int moveDirection = moveDistance > 0 ? 1 : -1;
            acceleration = moveDirection * acceleration;
            maxVelocity = moveDirection * maxVelocity;

            double distanceToFullVelocity = LinearMoves
                .GetDistanceWhileAccerlerating(currentVelocity, maxVelocity, acceleration);

            // Do we get to max speed?
            if (moveDirection * moveDistance > moveDirection * distanceToFullVelocity)
            {
                // If yes, what distance
                //  And, what remaining distance
                double remainingDistance = moveDistance - distanceToFullVelocity;
                //  And total time
                timeMoving += LinearMoves.GetTimeWhileAccelerating(currentVelocity, maxVelocity, distanceToFullVelocity);
                timeMoving += LinearMoves.GetTimeConstantVelocity(maxVelocity, remainingDistance);
            }
            else
            {
                // If no, what time
                timeMoving += LinearMoves.GetTimeWhileAccelerating(currentVelocity, maxVelocity, moveDistance);
            }

            return timeMoving;
        }
    }
}
