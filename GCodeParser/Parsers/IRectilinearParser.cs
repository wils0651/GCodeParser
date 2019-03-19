using GCodeParser.Models;

namespace GCodeParser.Parsers
{
    public interface IRectilinearParser
    {
        RectilinearMove ParseMove(string parameter);
    }
}
