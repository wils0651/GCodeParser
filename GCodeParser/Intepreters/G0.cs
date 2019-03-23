﻿using System;
using GCodeParser.Models;
using GCodeParser.Parsers;

namespace GCodeParser.Intepreters
{
    class G0 : IGCodeInterpreter
    {
        private readonly IMoveParser _parser;
        public static string GCODE_TYPE = "G0";

        // G0: rapid move
        public G0(IMoveParser parser)
        {
            _parser = parser;
        }
        public string GetInterpreterType()
        {
            return GCODE_TYPE;
        }

        public void InterpretGCode(ParsedCommand parsedCommand, Machine machine)
        {
            var parsedMoveDict = _parser.ParseMove(parsedCommand.ParameterString);

            float x = parsedMoveDict.TryGetValue('X', out float xResult) ? xResult : 0.0F;
            float y = parsedMoveDict.TryGetValue('Y', out float yResult) ? yResult : 0.0F;
            float z = parsedMoveDict.TryGetValue('Z', out float zResult) ? zResult : 0.0F;
            float speed = parsedMoveDict.TryGetValue('F', out float fResult) ? fResult : 0.0F;

            // TODO: Calculate duration

            // Update machine state
            machine.UpdatePosition(x, y, z);
        }
    }
}
