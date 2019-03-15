using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeParser
{
    public class Machine
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }

        float MaxVelocityX { get; set; }
        float MaxVelocityY { get; set; }
        float MaxVelocityZ { get; set; }

        float AccelerationX { get; set; }
        float AccelerationY { get; set; }
        float AccelerationZ { get; set; }

        float DurationSeconds { get; set; }

        public Machine()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }


    }
}
