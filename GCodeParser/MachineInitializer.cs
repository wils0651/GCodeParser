using GCodeParser.Interfaces;

namespace GCodeParser
{
    public class MachineInitializer : IMachineInitializer
    {
        private IMachine _machine;

        public MachineInitializer(IMachine machine)
        {
            _machine = machine;
        }

        public void InitializeMachine()
        {
            // Acceleration mm/s^2
            _machine.AccelerationX = 25.0;
            _machine.AccelerationY = 25.0;
            _machine.AccelerationZ = 25.0;

            // Stored in GRBL as mm/min
            _machine.MaxVelocityX = 500.0 / 60;
            _machine.MaxVelocityY = 500.0 / 60;
            _machine.MaxVelocityZ = 500.0 / 60;
        }
    }
}
