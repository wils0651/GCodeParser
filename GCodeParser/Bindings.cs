using Ninject.Modules;
using Ninject;
using Ninject.Extensions.Conventions;
using GCodeParser.Interfaces;
using GCodeParser.Parsers;
using GCodeParser.Intepreters;

namespace GCodeParser
{

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IFileHandler>().To<FileHandler>();
            kernel.Bind<ICommandParser>().To<CommandParser>();
            kernel.Bind<IMoveParser>().To<MoveParser>();

            kernel.Bind(x => x.FromAssemblyContaining(typeof(IGCodeInterpreter))
                .SelectAllClasses()
                .InheritedFrom<IGCodeInterpreter>()
                .BindAllInterfaces()
                );
        }
    }
}
