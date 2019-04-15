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
        public static double GetDistanceWhileAccerlerating(double initialVelocity, double finalVelocity, double acceleration)
        {
            return (finalVelocity - initialVelocity) * (finalVelocity + initialVelocity) / 2 * acceleration;
        }

        public static double GetTimeWhileAccelerating(double initialVelocity, double finalVelocity, double distance)
        {
            if(initialVelocity == 0 && finalVelocity == 0)
            {
                throw new ArgumentException("LinearMoves GetTimeWhileAccelerating zero intial and final velocity");
            }

            if (initialVelocity == -finalVelocity)
            {
                throw new ArgumentException("LinearMoves GetTimeWhileAccelerating initialVelocity == -maxVelocity");
            }

            return distance * 2 / (finalVelocity + initialVelocity);
        }

        public static double GetTimeConstantVelocity(double velocity, double distance)
        {
            if(velocity == 0 || distance == 0)
            {
                return 0;
            }

            if(Math.Sign(velocity) != Math.Sign(distance))
            {
                throw new ArgumentException("LinearMoves GetTimeConstantVelocity signs do not match");
            }

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
            double finalVelocity,
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
            finalVelocity = moveDirection * finalVelocity;

            double distanceToFullVelocity = GetDistanceWhileAccerlerating(currentVelocity, finalVelocity, acceleration);

            // Do we get to max speed?
            if (moveDirection * moveDistance > moveDirection * distanceToFullVelocity)
            {
                // If yes, what distance
                //  And, what remaining distance
                double remainingDistance = moveDistance - distanceToFullVelocity;
                //  And total time
                timeMoving += GetTimeWhileAccelerating(currentVelocity, finalVelocity, distanceToFullVelocity);
                timeMoving += GetTimeConstantVelocity(finalVelocity, remainingDistance);
            }
            else
            {
                // If no, what time
                timeMoving += GetTimeWhileAccelerating(currentVelocity, finalVelocity, moveDistance);
            }

            return timeMoving;
        }


    }
}
