using GCodeParser.Models;
using System;
using System.Collections.Generic;

namespace GCodeParser.Parsers
{
    public class MoveParser : IMoveParser
    {
        private Dictionary<char, double> _coordinateDictionary;

        public MoveParser()
        {
            _coordinateDictionary = new Dictionary<char, double>();
        }

        public Dictionary<char, double> ParseMove(string parameters)
        {
            parameters = parameters.Trim();

            if (string.IsNullOrWhiteSpace(parameters))
            {
                return null;
            }

            var parameterCharacterArray = parameters.ToUpper().ToCharArray();

            int position = 0;
            while(position < parameterCharacterArray.Length)
            {
                position = ParseCoordinate(parameterCharacterArray, position);
            }

            

            return _coordinateDictionary;
        }

        private int ParseCoordinate(char[] characters, int position)
        {
            int initialPosition = position;
            if(char.IsDigit(characters[initialPosition]))
            {
                throw new Exception("should be a character");
            }

            position++;
            string coordinateValue = string.Empty;
            while (position < characters.Length && IsValidNumericCharacter(characters[position]))
            {
                coordinateValue += characters[position];
                position++;
            }

            if (double.TryParse(coordinateValue, out double result))
            {
                _coordinateDictionary.Add(characters[initialPosition], result);
            }

            return position;
        }

        private bool IsValidNumericCharacter(char character)
        {
            return char.IsDigit(character) || character.Equals('.') || character.Equals('-');
        }
    }
}
