using System;

namespace GCodeParser.DurationCalculator
{
    public static class LinearMoves
    {
        private static double MOVE_THRESHOLD = 0.001;

        public static double GetMoveThreshold()
        {
            return MOVE_THRESHOLD;
        }

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

        public static double LinearMoveTime(
            double currentLocation,
            double currentVelocity,
            double maxVelocity,
            double acceleration,
            double endLocation)
        {
            double timeMoving = 0;
            double moveDistance = endLocation - currentLocation;

            if (Math.Abs(moveDistance) < MOVE_THRESHOLD)
            {
                return timeMoving;
            }

            int moveDirection = moveDistance > 0 ? 1 : -1;
            acceleration = moveDirection * acceleration;
            maxVelocity = moveDirection * maxVelocity;

            double distanceToFullVelocity = GetDistanceWhileAccerlerating(currentVelocity, maxVelocity, acceleration);

            // Do we get to max speed?
            if (moveDirection * moveDistance > moveDirection * distanceToFullVelocity)
            {
                // If yes, what distance
                //  And, what remaining distance
                double remainingDistance = moveDistance - distanceToFullVelocity;
                //  And total time
                timeMoving += GetTimeWhileAccelerating(currentVelocity, maxVelocity, distanceToFullVelocity);
                timeMoving += GetTimeConstantVelocity(maxVelocity, remainingDistance);
            }
            else
            {
                // If no, what time
                timeMoving += GetTimeWhileAccelerating(currentVelocity, maxVelocity, moveDistance);
            }

            return timeMoving;
        }


    }
}
