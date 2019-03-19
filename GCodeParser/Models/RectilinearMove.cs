namespace GCodeParser.Models
{
    public class RectilinearMove
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Speed { get; set; }

        public RectilinearMove(float x, float y, float z, float speed)
        {
            X = x;
            Y = y;
            Z = z;
            Speed = speed;
        }
    }
}
