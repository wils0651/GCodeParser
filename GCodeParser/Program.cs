using System;

namespace GCodeParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = $"C:\\Users\\Tim\\Documents\\GitHub\\GCodeParser\\GCodeParser\\TestGCode\\spindleLeveler.nc";

            var program = new GCodeParser();
            program.InterpretFile(filePath);

            Console.ReadLine(); // Pause at end
        }
    }
}
