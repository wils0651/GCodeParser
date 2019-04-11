using System;

namespace GCodeParser.DurationCalculator
{
    public static class LinearMoves
    {
        // Gets the distance to get to max velocity
        public static double GetDistanceWhileAccerlerating(double initialVelocity, double maxVelocity, double acceleration)
        {
            return (maxVelocity - initialVelocity) * (maxVelocity + initialVelocity) / 2 * acceleration;
        }

        public static double GetTimeWhileAccelerating(double initialVelocity, double maxVelocity, double distance)
        {
            return distance * 2 / (maxVelocity + initialVelocity);
        }

        public static double GetTimeConstantVelocity(double velocity, double distance)
        {
            return distance / velocity;
        }

        public static double GetMaxVelocityOverDistance(double initialVelocity, double distance, double acceleration)
        {
            double term1 = initialVelocity * initialVelocity + acceleration * 2 * distance;
            return Math.Sqrt(term1);
        }


    }
}
