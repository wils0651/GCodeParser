using GCodeParser.Intepreters;
using GCodeParser.Interfaces;
using GCodeParser.Models;
using GCodeParser.Parsers;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Collections.Generic;

namespace GCodeParser
{
    class GCodeParser
    {
        private ICommandInterpreter _commandInterpreter;
        private IMachineInitializer _machineInitializer;
        private IFileHandler _fileHandler;
        private ILog _log;

        public GCodeParser()
        {
            var kernel = BindKernel();

            _fileHandler = kernel.Get<IFileHandler>();
            _machineInitializer = kernel.Get<IMachineInitializer>();
            _commandInterpreter = kernel.Get<ICommandInterpreter>();
            _log = kernel.Get<ILog>();
        }

        public void InterpretFile(string filePath)
        {
            List<ParsedCommand> parsedCommands = _fileHandler.ProcessFile(filePath);

            _machineInitializer.InitializeMachine();    //TODO: Pass in file

            _commandInterpreter.InterpretCommands(parsedCommands);

            _log.OutputLogs();
            

        }

        private StandardKernel BindKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IFileHandler>().To<FileHandler>();
            kernel.Bind<ICommandParser>().To<CommandParser>();
            kernel.Bind<IMoveParser>().To<MoveParser>();
            kernel.Bind<ICommandInterpreter>().To<CommandInterpreter>();
            kernel.Bind<IMachineInitializer>().To<MachineInitializer>();

            kernel.Bind<IMachine>().To<Machine>().InSingletonScope();
            kernel.Bind<ILog>().To<SimpleLog>().InSingletonScope();

            kernel.Bind(x => x.FromAssemblyContaining(typeof(IGCodeInterpreter))
                .SelectAllClasses()
                .InheritedFrom<IGCodeInterpreter>()
                .BindAllInterfaces()
                );

            return kernel;
        }
   
    }
}
