namespace GCodeParser
{
    public interface IMachine
    {
        void AddTime(double timeInSeconds);
        void UpdatePosition(double x, double y, double z);
        double GetDuration();
    }
}
