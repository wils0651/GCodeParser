using System.Collections.Generic;

namespace GCodeParser.Parsers
{
    public interface IMoveParser
    {
        Dictionary<char, float> ParseMove(string parameter);
    }
}
