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

        public static double GetTimeWhileAcceleratingToVelocity(double initialVelocity, double finalVelocity, double distance)
        {
            if(initialVelocity == 0 && finalVelocity == 0)
            {
                throw new ArgumentException("LinearMoves GetTimeWhileAccelerating zero intial and final velocity");
            }

            if (initialVelocity == -finalVelocity)
            {
                throw new ArgumentException("LinearMoves GetTimeWhileAccelerating initialVelocity == -finalVelocity");
            }
            
            var time = distance * 2 / (finalVelocity + initialVelocity);

            if(time < 0)
            {
                time = 0;
                // TODO: Throw an exception
            }

            return time;
        }

        public static double GetTimeWhileAcceleratingToFinalVelocity(double initialVelocity, double finalVelocity, double acceleration)
        {
            if (initialVelocity == 0 && finalVelocity == 0)
            {
                throw new ArgumentException("LinearMoves GetTimeWhileAccelerating zero intial and final velocity");
            }

            var time = (finalVelocity - initialVelocity) / acceleration;

            if (time < 0)
            {
                time = 0;
                // TODO: Throw an exception
            }

            return time;
        }

        public static double GetTimeWhileAcceleratingOverDistance(double initialVelocity, double acceleration, double distance)
        {
            // TODO: arg validation?
            var time1 = (-1.0 * initialVelocity + Math.Sqrt(initialVelocity * initialVelocity + 2 * acceleration * distance)) / acceleration;
            var time2 = (-1.0 * initialVelocity - Math.Sqrt(initialVelocity * initialVelocity + 2 * acceleration * distance)) / acceleration;

            //TODO: pick the right time

            return time1;
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

            var time = distance / velocity;

            if (time < 0)
            {
                time = 0;
            }

            return time;
        }

        public static double GetMaxVelocityOverDistance(double initialVelocity, double distance, double acceleration)
        {
            double term1 = initialVelocity * initialVelocity + acceleration * 2.0 * distance;
            return Math.Sqrt(term1);
        }

        public static double LinearMoveTime(
            double currentLocation,
            double currentVelocity,
            double maxSpeed,
            double accelerationRate,
            double endLocation)
        {
            double timeMoving = 0.0;
            double moveDistance = endLocation - currentLocation;

            if (Math.Abs(moveDistance) < MOVE_THRESHOLD)
            {
                return timeMoving;
            }

            int moveDirection = moveDistance > 0 ? 1 : -1;
            var acceleration = moveDirection * accelerationRate;
            var finalVelocity = moveDirection * maxSpeed;

            //double distanceToFullVelocity = GetDistanceWhileAccerlerating(currentVelocity, maxSpeed, accelerationRate);
            double distanceToFullVelocity = GetDistanceWhileAccerlerating(currentVelocity, finalVelocity, acceleration);

            // Do we get to max speed?
            if (Math.Abs(moveDistance) > Math.Abs(distanceToFullVelocity))
            {
                // If yes, what distance
                //  And, what remaining distance
                double remainingDistance = Math.Abs(moveDistance) - Math.Abs(distanceToFullVelocity);
                //  And total time
                timeMoving = GetTimeWhileAcceleratingToVelocity(currentVelocity, maxSpeed, distanceToFullVelocity);
                timeMoving += GetTimeConstantVelocity(maxSpeed, remainingDistance);
            }
            else
            {
                // If no, what time
                //timeMoving += GetTimeWhileAcceleratingToVelocity(currentVelocity, maxSpeed, moveDistance);
                //timeMoving = GetTimeWhileAcceleratingToFinalVelocity(currentVelocity, finalVelocity, acceleration);
                timeMoving = GetTimeWhileAcceleratingOverDistance(currentLocation, acceleration, moveDistance);
            }

            if(timeMoving < 0)
            {
                timeMoving = 0;
            }

            return timeMoving;
        }


    }
}
