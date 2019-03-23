using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeParser
{

    public class Machine
    {
        private const float zero = 0.0F;

        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }

        float VelocityX { get; set; }
        float VelocityY { get; set; }
        float VelocityZ { get; set; }

        float MaxVelocityX { get; set; }
        float MaxVelocityY { get; set; }
        float MaxVelocityZ { get; set; }

        float AccelerationX { get; set; }
        float AccelerationY { get; set; }
        float AccelerationZ { get; set; }

        float DurationSeconds { get; set; }

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

        public void AddTime(float timeInSeconds)
        {
            DurationSeconds += timeInSeconds;
        }

        public void UpdatePosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }


    }
}
