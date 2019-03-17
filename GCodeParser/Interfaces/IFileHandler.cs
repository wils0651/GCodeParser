using GCodeParser.Models;
using System.Collections.Generic;

namespace GCodeParser.Interfaces
{
    public interface IFileHandler
    {
        List<ParsedCommand> ProcessFile(string filePath);
    }
}
