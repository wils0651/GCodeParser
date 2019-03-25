namespace GCodeParser.Models
{
    public class RectilinearMove
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Speed { get; set; }

        public RectilinearMove(double x, double y, double z, double speed)
        {
            X = x;
            Y = y;
            Z = z;
            Speed = speed;
        }
    }
}
