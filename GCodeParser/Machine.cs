namespace GCodeParser
{

    public class Machine : IMachine
    {
        private const double zero = 0.0;

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double VelocityZ { get; set; }

        public double MaxVelocityX { get; set; }
        public double MaxVelocityY { get; set; }
        public double MaxVelocityZ { get; set; }

        public double AccelerationX { get; set; }
        public double AccelerationY { get; set; }
        public double AccelerationZ { get; set; }

        public double DurationSeconds { get; set; }

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
