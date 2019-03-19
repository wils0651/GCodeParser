using System;
using GCodeParser.Models;

namespace GCodeParser.Intepreters
{
    class G0 : IGCodeInterpreter
    {
        public string GetInterpreterType()
        {
            return "G0";
        }

        public void InterpretGCode(ParsedCommand parsedCommand, Machine machine)
        {
            throw new NotImplementedException();
        }
    }
}
