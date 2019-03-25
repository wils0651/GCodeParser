using System.Collections.Generic;

namespace GCodeParser.Parsers
{
    public interface IMoveParser
    {
        Dictionary<char, double> ParseMove(string parameter);
    }
}
