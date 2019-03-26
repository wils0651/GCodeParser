namespace GCodeParser
{
    class MachineStorage : IMachineStorage
    {
        IMachine _machine;

        public MachineStorage(IMachine machine)
        {
            _machine = machine;
        }

        public IMachine GetMachine()
        {
            return _machine;
        }
    }
}
