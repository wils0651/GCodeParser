using Ninject.Modules;
using Ninject;
using GCodeParser.Interfaces;

namespace GCodeParser
{

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileHandler>().To<FileHandler>();
            Bind<ICommandParser>().To<CommandParser>();
        }
    }
}
