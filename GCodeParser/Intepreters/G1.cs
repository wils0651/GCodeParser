using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCodeParser.DurationCalculator;
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
        public static double TIME_THRESHOLD = 0.1;

        public G1(IMoveParser parser)
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

            // Update machine state
            if(feedRate > 0)
            {
                machine.FeedRate = feedRate;
            }

            double moveTime = MoveTime(machine, x, y, z);
            machine.AddTime(moveTime);

            machine.UpdatePosition(x, y, z);
        }

        private double MoveTime(IMachine machine, double x, double y, double z)
        {
            var moveThreshold = LinearMoves.GetMoveThreshold();
            var zeroTolerance = 0.0001;

            double xMoveDistance = (Math.Abs(x) > zeroTolerance) ? x - machine.X : 0;
            double yMoveDistance = (Math.Abs(y) > zeroTolerance) ? y - machine.Y : 0;
            double zMoveDistance = (Math.Abs(z) > zeroTolerance) ? z - machine.Z : 0;

            double distanceMagnitude = Math.Sqrt(xMoveDistance * xMoveDistance + yMoveDistance * yMoveDistance + zMoveDistance * zMoveDistance);

            HashSet<double> times = new HashSet<double>();

            if(Math.Abs(xMoveDistance) > moveThreshold)
            {
                times.Add(ComponentMoveTime(distanceMagnitude, machine.FeedRate, machine.X, machine.VelocityX, machine.MaxVelocityX, machine.AccelerationX, x));
            }

            if (Math.Abs(yMoveDistance) > moveThreshold)
            {
                times.Add(ComponentMoveTime(distanceMagnitude, machine.FeedRate, machine.Y, machine.VelocityY, machine.MaxVelocityY, machine.AccelerationY, y));
            }

            if (Math.Abs(zMoveDistance) > moveThreshold)
            {
                times.Add(ComponentMoveTime(distanceMagnitude, machine.FeedRate, machine.Z, machine.VelocityZ, machine.MaxVelocityZ, machine.AccelerationZ, z));
            }

            return CheckTimesAndReturnAverage(times);
        }

        private double CheckTimesAndReturnAverage(HashSet<double> times)
        {
            if(times == null || times.Count() == 0)
            {
                return 0.0;
            }

            var max = times.Max();
            var min = times.Min();

            if ((max - min) > TIME_THRESHOLD)
            {
                Console.WriteLine($"G1 times don't match. Max {max}; Min {min}");
            }
            return times.Average();
        }

        private double ComponentMoveTime(
            double vectorMagnitude,
            double feedRate,
            double currentLocation,
            double currentVelocity, 
            double maxVelocity, 
            double acceleration, 
            double endLocation)
        {
            double moveDistance = endLocation - currentLocation;

            if (Math.Abs(moveDistance) < LinearMoves.GetMoveThreshold())
            {
                return 0.0;
            }

            double vectorComponentFraction = moveDistance / vectorMagnitude;

            double componentMaxVelocity = feedRate * vectorComponentFraction;
            double componentAcceleration = acceleration * vectorComponentFraction;

            var velocityDirection = Math.Round(vectorComponentFraction / Math.Abs(vectorComponentFraction));

            // component speed at feed rate will need to be checked against max velocity
            if (Math.Abs(componentMaxVelocity) > maxVelocity)
            {
                //Console.WriteLine($"Too fast !!! Feed Rate: {feedRate}; Max Velocity: {maxVelocity}; Component Max Velocity {componentMaxVelocity}");
                componentMaxVelocity = velocityDirection * maxVelocity;
            }

            return LinearMoves.LinearMoveTime(
                currentLocation,
                currentVelocity,
                componentMaxVelocity,
                componentAcceleration,
                endLocation);
        }
    }
}
