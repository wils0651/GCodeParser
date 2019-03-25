using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeParser.DurationCalculator
{
    public static class LinearRapid
    {
        public static double MoveTime(Machine machine, double xFinal, double yFinal, double zFinal)
        {
            double time = 0;
            // Get accel, v0 settings from the machine
            // check if current and final positions are the same

            return time;
        }

        // Gets the distance to get to max velocity
        private static double GetAccelerationDistance(double initialVelocity, double maxVelocity, double acceleration)
        {
            return (maxVelocity - initialVelocity) * (maxVelocity + initialVelocity) / 2 * acceleration;
        }

        private static double GetAcceleratingTime(double initialVelocity, double maxVelocity, double distance)
        {
            return distance * 2 / (maxVelocity + initialVelocity);
        }

        private static double GetConstantVelocityTime(double velocity, double distance)
        {
            return distance / velocity;
        }

        private static double GetMaxVelocityOverDistance(double initialVelocity, double distance, double acceleration)
        {
            double term1 = initialVelocity * initialVelocity + acceleration * 2 * distance;
            return Math.Sqrt(term1);
        }


    }
}
