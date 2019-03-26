using GCodeParser.Interfaces;
using System;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Reflection;
using System.Collections.Generic;
using GCodeParser.Models;
using GCodeParser.Parsers;
using GCodeParser.Intepreters;

namespace GCodeParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = $"C:\\Users\\Tim\\Documents\\GitHub\\GCodeParser\\GCodeParser\\TestGCode\\spindleLeveler.nc";

            // kernel

            var kernel = new StandardKernel();

            kernel.Bind<IFileHandler>().To<FileHandler>();
            kernel.Bind<ICommandParser>().To<CommandParser>();
            kernel.Bind<IMoveParser>().To<MoveParser>();
            kernel.Bind<IMachineStorage>().To<MachineStorage>();
            kernel.Bind<ICommandInterpreter>().To<CommandInterpreter>();

            kernel.Bind<IMachine>().To<Machine>().InSingletonScope();

            kernel.Bind(x => x.FromAssemblyContaining(typeof(IGCodeInterpreter))
                .SelectAllClasses()
                .InheritedFrom<IGCodeInterpreter>()
                .BindAllInterfaces()
                );


            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            var fileHandler = kernel.Get<IFileHandler>();
            List<ParsedCommand> parsedCommands = fileHandler.ProcessFile(filePath);

            // Interpret those parsed commands
            var interpreter = kernel.Get<ICommandInterpreter>();
            interpreter.InterpretCommands(parsedCommands);


            Console.ReadLine(); // Pause at end
        }

        

    }
}
