namespace GCodeParser
{
    public interface IMachine
    {
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

        double FeedRate { get; set; }

        void AddTime(double timeInSeconds);
        void UpdatePosition(double x, double y, double z);
        double GetDuration();
    }
}
