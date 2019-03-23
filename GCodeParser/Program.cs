using GCodeParser.Interfaces;
using System;
using Ninject;
using System.Reflection;
using System.Collections.Generic;
using GCodeParser.Models;

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

            List<ParsedCommand> parsedCommands = fileHandler.ProcessFile(filePath); 

            // Interpret those parsed commands


            Console.ReadLine(); // Pause at end
        }

    }
}
