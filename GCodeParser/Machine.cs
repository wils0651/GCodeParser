namespace GCodeParser
{

    public class Machine : IMachine
    {
        private const double zero = 0.0;

        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }

        double VelocityX { get; set; }
        double VelocityY { get; set; }
        double VelocityZ { get; set; }

        double MaxVelocityX { get; set; }
        double MaxVelocityY { get; set; }
        double MaxVelocityZ { get; set; }

        double AccelerationX { get; set; }
        double AccelerationY { get; set; }
        double AccelerationZ { get; set; }

        double DurationSeconds { get; set; }

        public Machine()
        {
            X = zero;
            Y = zero;
            Z = zero;

            VelocityX = zero;
            VelocityY = zero;
            VelocityZ = zero;

            DurationSeconds = zero;
        }

        public void AddTime(double timeInSeconds)
        {
            DurationSeconds += timeInSeconds;
        }

        public void UpdatePosition(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double GetDuration()
        {
            return DurationSeconds;
        }


    }
}
