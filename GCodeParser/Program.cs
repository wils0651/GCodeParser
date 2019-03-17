using GCodeParser.Interfaces;
using System;
using Ninject;
using System.Reflection;

namespace GCodeParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = $"C:\\Users\\Tim\\Documents\\GitHub\\GCodeParser\\GCodeParser\\TestGCode\\spindleLeveler.nc";

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var fileHandler = kernel.Get<IFileHandler>();

            fileHandler.ProcessFile(filePath);

            Console.ReadLine(); // Pause at end
        }

    }
}
